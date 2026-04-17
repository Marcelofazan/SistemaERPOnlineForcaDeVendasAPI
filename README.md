# SistemaERPOnlineForcaDeVendasAPI

Exemplo de criação de WebAPI com Clean Arquitetura com autenticação e autorização utilizando JWT com banco de dados SQLite

# O que você vai encontrar neste projeto

- **JWT** - Implementação de autenticação e autorização em WebAPI
- **EF Core** - EntityFramework com utilização de Code First 
- **Injeção de Dependência** - Separação da criação de objetos e de sua reutilização, ideal para a realização de testes unitários
- **Testes Unitários** - Separação da criação de objetos e de sua reutilização, ideal para a realização de testes unitários

## Requisitos e Detalhe do uso de EntityFrameworkCore 10

No Visual Studio Abra (Ferramentas) > (Gerenciador de Pacotes NuGet) > (Console do Gerenciador de Pacotes Nuget)  
Necessário para Atualizar o Depurador com a Solução. 

OBS: Certificar em Definir o Projeto Padrão como (SistemaERPOnlineForcaDeVendasAPI.WebAPI) e o nome da pasta raiz seja (SistemaERPOnlineForcaDeVendasAPI), não pode haver (-main) no nome da pasta

* Instalar pacotes necessários (Obrigatório)
```bash
...
	Install-Package Microsoft.EntityFrameworkCore.Tools
	Install-Package Microsoft.EntityFrameworkCore.Design
...

```
* Usar o comando Add-Migration para Code First

```bash
...
  Add-Migration InitialCreate -Project "InfraEstrutura" -StartupProject "SistemaERPOnlineForcaDeVendasAPI.WebAPI"
...

```
* Como aplicar as mudanças no banco

```bash
...
	Update-Database -Project "SistemaERPOnlineForcaDeVendasAPI.InfraEstrutura" -StartupProject "SistemaERPOnlineForcaDeVendasAPI.WebAPI"
...

```

### Execução da aplicação

Após o Migrations, executa a aplicação **https://localhost:7092/Swagger/index.html** (ou na porta exibida no terminal). 

O banco SQLite (`SistemaERPOnlineForcaDeVendasAPI.db`) é criado na raiz do projeto na primeira execução.


## Execução Inicial de Endpoints (Postman)

**(Registrar usuário)**
Enviar POST / Usuario: **https://localhost:7092/api/auth/registro**, selecionar Guia Body e enviar RAW e enviar o seguinte JSON 

   ```json
	{ 
    		"idempresa": 1, 
    		"email": "email@email.com", 
    		"senha": "123456", 
    		"nome": "Usuario", 
    		"taxapercentual": 10.00
	}
   ```

**(Fazer Login)**
Enviar POST / Usuario: **https://localhost:7092/api/auth/login**, selecionar Guia Body e enviar RAW e enviar o seguinte JSON 

   ```json
	{
		"email": "email@email.com",
		"senha": "123456"
	}
   ```

3 - Clique na Aba do Arquivo ou para todos os arquivos, na pasta **Authorizathion** no Postman e cole Token (sem "Bearer") e salve 

```bash
...
	Metodo: POST /api/auth/registro            Função: Registo de novo admin                JWT: Não 
	Metodo: POST /api/auth/login               Função: Login e obter token JWT              JWT: Não
	Metodo: GET /health                        Função: Health check (API + base de dados)   JWT: Não
	Metodo: GET/POST /api/Produtos             Função: Listar / Criar produtos              JWT: Sim
	Metodo: GET/PUT/DELETE /api/Produtos/{id}  Função: Obter / Atualizar / Excluir produto  JWT: Sim
...

```

##Exemplo de EndPoint Criar produtos:

Enviar POST / Produto: https://localhost:7092/api/Produtos, selecionar Guia Body e enviar RAW e enviar o seguinte JSON 

   ```json
	{
  		"idempresa": 1,
  		"idproduto": 1,
  		"valorultimacompra": 95.6836,
  		"lucrominimo": 27.50,
  		"lucromaximo": 50.00,
  		"precovendaminimo": 121.99659,
  		"precosugerido": 143.5304
	}
   ```

###Health

5 - Health Checar o servidor – Estado da API e da base de dados (útil para monitorização e orquestração).
GET http://localhost:7092/health 


## Testes

```bash
        dotnet test SistemaERPOnlineForcaDeVendasAPI.Testes/SistemaERPOnlineForcaDeVendasAPI.Testes.csproj
```

Os testes cobrem a camada **Aplicacao** (serviços de Projeto e Tarefa), com mocks dos repositórios.



## Configuração

- **Banco:** SQLite, arquivo `SistemaERPOnlineForcaDeVendasAPI.db` na raiz do projeto (não versionado). Connection string em `appsettings.json` (`ConnectionStrings:DefaultConnection`).
- **JWT:** Em `appsettings.json`, substitua `Jwt:Key` por uma chave segura com **mínimo 32 caracteres** (ou defina a variável de ambiente `Jwt__Key`). Em produção use sempre variáveis de ambiente ou User Secrets.

