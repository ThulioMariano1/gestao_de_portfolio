# Sistema de Gestão de Portfólio de Investimentos

Este é um projeto em C# para gestão de portfólio de investimentos. A seguir estão as instruções para configuração e execução do projeto.

## Requisitos

- **IDE**: Visual Studio 2022
- **Banco de Dados**: SQL Server (é obrigatório ter o SQL Server instalado na máquina)

### Passo 1: Clonar o Repositório
Clone o repositório do projeto para sua máquina local:
```bash
git clone https://github.com/seu-usuario/nome-do-repositorio.git
git clone <URL-do-repositório>
```
### Passo 2: Abrir o Projeto no Visual Studio
Abra o Visual Studio 2022 e carregue a solução do projeto.

### Passo 3: Restaurar os Pacotes NuGet
No Visual Studio, abra o Gerenciador de Pacotes NuGet e restaure os pacotes necessários:
```bash
dotnet restore
```
Certifique-se que esta instalado os seguintes pacotes NuGet:

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `Microsoft.Extensions.Hosting`
- `Microsoft.Extensions.Logging`

OBS: caso não esteja, favor instalar

### Passo 4: Configurar o Banco de Dados

Edite o arquivo `appsettings.json` para atualizar as informações de conexão com o banco de dados, se necessário.
Certifique-se de que o SQL Server esteja em execução.

### Passo 5: Aplicar Migrações
Para criar a estrutura do banco de dados, execute os seguintes comandos no **Package Manager Console** do Visual Studio:

1. Gerar a migração inicial:

    ```sh
    Add-Migration InitialDB -Context PortfolioDBContext
    ```

2. Atualizar o banco de dados com a estrutura criada:

    ```sh
    Update-Database -Context PortfolioDBContext
    ```

### Passo 6: Executar a Aplicação
Pressione F5 ou clique no botão de execução no Visual Studio para iniciar a aplicação.



# Documentação de Como Utilizar a Aplicação
**Nota**: Alguns endpoints nesta API requerem autenticação. Para acessá-los, é necessário incluir um `Bearer Token` no cabeçalho da requisição.

**Exemplo:** 
    ```
       Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InRlc3RlQGdtYWlsLmNvbSIsImlkZW50aWZpZXIiOiI1IiwibmJmIjoxNzE4NjQyMDQyLCJleHAiOjE3MTg3Mjg0NDIsImlhdCI6MTcxODY0MjA0MiwiaXNzIjoicG9ydGZvbGlvX2lzc3VlciIsImF1ZCI6InBvcnRmb2xpb19hdWRpZW5jZSJ9.D4n4fEoNnykox8m70SPbnJaxcObDiRYU1PDHLQN_N7o
    ```

## Endpoints Disponíveis

### User
- **POST /api/User/create**
  - **Descrição**: Cria um novo usuário.
  - **Exemplo de Requisição**:
    ```json
    {
      "name": "string",
      "email": "string",
      "passWord": "string",
      "type": 1,
      "money": 0
    }
    ```
  - **Nota**: A chave '*type*' é usado para identificar o tipo de usuário, sendo 1 para administrador e 2 para client.

### Auth
- **POST /api/Auth/login**
  - **Descrição**: Autentica um usuário.
  - **Exemplo de Requisição**:
    ```json
    {
      "email": "string",
      "password": "string"
    }
    ```
  - **Exemplo de Resposta**:
    ```json
    {
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InRodWxpb21hcmlhbm9AZ21haWwuY29tIiwiaWRlbnRpZmllciI6IjIiLCJuYmYiOjE3MTg2NDcxMTUsImV4cCI6MTcxODczMzUxNSwiaWF0IjoxNzE4NjQ3MTE1LCJpc3MiOiJwb3J0Zm9saW9faXNzdWVyIiwiYXVkIjoicG9ydGZvbGlvX2F1ZGllbmNlIn0.0t7gO1H5-N-PkCVv5iTOTHePX8J32Cb_yUe2BzMnFUg"
    }
    ```
  - **Nota**: Este é o token de autenticação que deve ser incluído em um `Bearer Token` no cabeçalho da requisição.

### Deposit 
- **POST /api/Deposit/add**
  - **Descrição**: Adiciona um depósito.
  - **[Autenticado]**
  - **Exemplo de Requisição**:
    ```json
    {
      "price": 0
    }
    ```
  - **Resposta**: Retorna um True(Sucesso) ou False(Insucesso)

- **POST /api/Deposit/decrease**
  - **Descrição**: Reduz um depósito.
  - **[Autenticado]**
  - **Exemplo de Requisição**:
    ```json
    {
      "price": 0
    }
    ```
  - **Resposta**: Retorna um True(Sucesso) ou False(Insucesso)


### Investment
- **GET /api/Investment**
  - **[Autenticado]**
  - **Descrição**: Obtém todos os investimentos.
  - **Exemplo de Resposta**:
    ```json
    [
      {
        "id": 0,
        "userId": 0,
        "productId": 0,
        "amount": 0,
        "price": 0,
        "dateAcquisition": "2024-06-17T19:18:30.179Z",
        "product": {
          "id": 0,
          "name": "string",
          "description": "string",
          "type": "string",
          "price": 0,
          "otherDetails": "string"
        }
      }
    ]
    ```

- **GET /api/Investment/id**
  - **[Autenticado]**
  - **Descrição**: Obtém detalhes de um investimento específico.
  - **Requisição**: Deve passar o Id do investimento via pathParam.
  - **Exemplo de Resposta**:
    ```json
    {
      "id": 0,
      "userId": 0,
      "productId": 0,
      "amount": 0,
      "price": 0,
      "dateAcquisition": "2024-06-17T19:43:01.936Z"
    }
    ```

- **GET /api/Investment/id/context**
  - **[Autenticado]**
  - **Descrição**: Obtém detalhes de um investimento específico com o produto.
  - **Requisição**: Deve passar o Id do investimento via pathParam.
  - **Exemplo de Resposta**:
    ```json
    {
      "id": 0,
      "userId": 0,
      "productId": 0,
      "amount": 0,
      "price": 0,
      "dateAcquisition": "2024-06-17T19:43:19.223Z",
      "product": {
        "id": 0,
        "name": "string",
        "description": "string",
        "type": "string",
        "price": 0,
        "otherDetails": "string"
      }
    }
    ```

### Orders
- **GET /api/Orders**
  - **Descrição**: Obtém todas as ordens.
  - **[Autenticado]**
  - **Exemplo de Resposta**:
    ```json
    {
      "id": 0,
      "buyDate": "2024-06-17T19:43:57.740Z",
      "saleDate": "2024-06-17T19:43:57.740Z",
      "price": 0,
      "amount": 0,
      "status": 1,
      "userId": 0,
      "productId": 0,
      "product": {
        "id": 0,
        "name": "string",
        "description": "string",
        "type": "string",
        "price": 0,
        "otherDetails": "string"
      }
    }
    ```
- **POST /api/Orders/buy**
  - **Descrição**: Cria uma ordem de compra.
  - **[Autenticado]**
  - **Exemplo de Requisição**:
    ```json
    {
      "productId": 0,
      "price": 0,
      "amount": 0
    }
    ```
  - **Resposta**: Retorna um com o status que ficou

- **POST /api/Orders/sale**
  - **Descrição**: Cria uma ordem de venda.
  - **[Autenticado]**
  - **Exemplo de Requisição**:
    ```json
    {
      "productId": 0,
      "price": 0,
      "amount": 0
    }
    ```
  - **Resposta**: Retorna um com o status que ficou

- **PUT /api/Orders/id/cancel**
  - **Descrição**: Cancela uma ordem específica.
  - **Requisição**: Deve passar o Id da ordem via pathParam.
  - **Resposta**: Retorna um True(Sucesso) ou False(Insucesso)


### Products
- **GET /api/Products**
  - **Descrição**: Obtém todos os produtos.
  - **[Autenticado]**
  - **Exemplo de Resposta**:
    ```json
    [
      {
        "id": 0,
        "name": "string",
        "description": "string",
        "type": "string",
        "price": 0,
        "otherDetails": "string"
      }
    ]
    ```

- **POST /api/Products**
  - **Descrição**: Cria um novo produto.
  - **[Autenticado]**: Apenas administrador pode executar
  - **Exemplo de Requisição**:
    ```json
    {
      "id": 0,
      "name": "string",
      "description": "string",
      "type": "string",
      "price": 0,
      "otherDetails": "string"
    }
    ```
  - **Resposta**: Retorna um True(Sucesso) ou False(Insucesso)

- **PUT /api/Products**
  - **Descrição**: Atualiza um produto existente.
  - **[Autenticado]**: Apenas administrador pode executar
  - **Exemplo de Requisição**:
    ```json
    {
      "id": 0,
      "name": "string",
      "description": "string",
      "type": "string",
      "price": 0,
      "otherDetails": "string"
    }
    ```
  - **Resposta**: Retorna um True(Sucesso) ou False(Insucesso)

- **GET /api/Products/id**
  - **Descrição**: Obtém detalhes de um produto específico.
  - **[Autenticado]**
  - **Requisição**: Deve passar o Id do produto via pathParam.
  - **Exemplo de Resposta**:
    ```json
    {
      "id": 0,
      "name": "string",
      "description": "string",
      "type": "string",
      "price": 0,
      "otherDetails": "string"
    }
    ```

- **DELETE /api/Products/id**
  - **Descrição**: Deleta um produto específico.
  - **[Autenticado]**: Apenas administrador pode executar
  - **Requisição**: Deve passar o Id do produto via pathParam.
  - **Resposta**: Retorna um True(Sucesso) ou False(Insucesso)