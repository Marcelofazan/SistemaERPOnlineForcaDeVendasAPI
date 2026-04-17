using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaERPOnlineForcaDeVendasAPI.Configuracao;
using SistemaERPOnlineForcaDeVendasAPI.Dominio.Entidades;
using SistemaERPOnlineForcaDeVendasAPI.InfraEstrutura.Data;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaskManagerAPI.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _db;

    public AuthController(IConfiguration configuration, AppDbContext db)
    {
        _configuration = configuration;
        _db = db;
    }

    [HttpPost("registro")]
    [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Registro([FromBody] RegistroRequest request, CancellationToken cancellationToken)
    {
        var emailNormalizado = request.Email.Trim().ToLowerInvariant();
        if (await _db.Usuarios.AnyAsync(a => a.Email == emailNormalizado, cancellationToken))
            return BadRequest(new { message = "Já existe um usuário com este e-mail." });

        var usuario = new Usuario
        {
            IdEmpresa = request.IdEmpresa,
            Email = emailNormalizado,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
            Nome = request.Nome.Trim(),
            TaxaPercentual = request.TaxaPercentual
        };
        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(Registro), new { Id = usuario.Id }, new RegistroResponse(usuario.Id, usuario.IdEmpresa, usuario.Email, usuario.Nome, usuario.TaxaPercentual));
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var emailNormalizado = request.Email?.Trim().ToLowerInvariant();
        var usuario = await _db.Usuarios.FirstOrDefaultAsync(a => a.Email == emailNormalizado, cancellationToken);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
            return Unauthorized(new { message = "E-mail ou senha inválidos." });

        var jwt = _configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
        if (string.IsNullOrEmpty(jwt?.Key)) return StatusCode(500, "JWT não configurado.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome)
        };
        var token = new JwtSecurityToken(
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(jwt.ExpirationHours),
            signingCredentials: creds);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(new LoginResponse(tokenString));
    }
}

public record LoginRequest(
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "O e-mail é obrigatório.")]
    [System.ComponentModel.DataAnnotations.EmailAddress(ErrorMessage = "E-mail inválido.")]
    string Email,
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "A senha é obrigatória.")]
    string Senha);

public record LoginResponse(string Token);

public record RegistroRequest(
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "O e-mail é obrigatório.")]
    [System.ComponentModel.DataAnnotations.EmailAddress(ErrorMessage = "E-mail inválido.")]
    [System.ComponentModel.DataAnnotations.MaxLength(256)]
    string Email,
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "A senha é obrigatória.")]
    [System.ComponentModel.DataAnnotations.MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
    [System.ComponentModel.DataAnnotations.MaxLength(100)]
    string Senha,
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "O nome é obrigatório.")]
    [System.ComponentModel.DataAnnotations.MinLength(1)]
    [System.ComponentModel.DataAnnotations.MaxLength(200)]
    string Nome,
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "A empresa é obrigatório.")]
    int IdEmpresa,
    [System.ComponentModel.DataAnnotations.Range(0.0001, 9999999999.9999, ErrorMessage = "Taxa Percentual é Inválida")]
    double TaxaPercentual);

public record RegistroResponse(int Id, int IdEmpresa, string Email, string Nome, double TaxaPercentual);
