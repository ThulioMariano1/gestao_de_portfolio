# Sistema de Gest�o de Portf�lio de Investimentos

Este � um projeto em C# para gest�o de portf�lio de investimentos. A seguir est�o as instru��es para configura��o e execu��o do projeto.

## Requisitos

- **IDE**: Visual Studio 2022
- **Banco de Dados**: SQL Server (� obrigat�rio ter o SQL Server instalado na m�quina)

### Passo 1: Clonar o Reposit�rio
Clone o reposit�rio do projeto para sua m�quina local:
```bash
git clone https://github.com/seu-usuario/nome-do-repositorio.git
git clone <URL-do-reposit�rio>
```
### Passo 2: Abrir o Projeto no Visual Studio
Abra o Visual Studio 2022 e carregue a solu��o do projeto.

### Passo 3: Restaurar os Pacotes NuGet
No Visual Studio, abra o Gerenciador de Pacotes NuGet e restaure os pacotes necess�rios:
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

OBS: caso n�o esteja, favor instalar

### Passo 4: Configurar o Banco de Dados

Edite o arquivo `appsettings.json` para atualizar as informa��es de conex�o com o banco de dados, se necess�rio.
Certifique-se de que o SQL Server esteja em execu��o.

### Passo 5: Aplicar Migra��es
Para criar a estrutura do banco de dados, execute os seguintes comandos no **Package Manager Console** do Visual Studio:

1. Gerar a migra��o inicial:

    ```sh
    Add-Migration InitialDB -Context PortfolioDBContext
    ```

2. Atualizar o banco de dados com a estrutura criada:

    ```sh
    Update-Database -Context PortfolioDBContext
    ```

### Passo 6: Executar a Aplica��o
Pressione F5 ou clique no bot�o de execu��o no Visual Studio para iniciar a aplica��o.



# Documenta��o de Como Utilizar a Aplica��o
**Nota**: Alguns endpoints nesta API requerem autentica��o. Para acess�-los, � necess�rio incluir um `Bearer Token` no cabe�alho da requisi��o.

**Exemplo:** 
    ```
       Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InRlc3RlQGdtYWlsLmNvbSIsImlkZW50aWZpZXIiOiI1IiwibmJmIjoxNzE4NjQyMDQyLCJleHAiOjE3MTg3Mjg0NDIsImlhdCI6MTcxODY0MjA0MiwiaXNzIjoicG9ydGZvbGlvX2lzc3VlciIsImF1ZCI6InBvcnRmb2xpb19hdWRpZW5jZSJ9.D4n4fEoNnykox8m70SPbnJaxcObDiRYU1PDHLQN_N7o
    ```

## Endpoints Dispon�veis

### User
- **POST /api/User/create**
  - **Descri��o**: Cria um novo usu�rio.
  - **Exemplo de Requisi��o**:
    ```json
    {
      "name": "string",
      "email": "string",
      "passWord": "string",
      "type": 1,
      "money": 0
    }
    ```
  - **Nota**: A chave '*type*' � usado para identificar o tipo de usu�rio, sendo 1 para administrador e 2 para client.

### Auth
- **POST /api/Auth/login**
  - **Descri��o**: Autentica um usu�rio.
  - **Exemplo de Requisi��o**:
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
  - **Nota**: Este � o token de autentica��o que deve ser inclu�do em um `Bearer Token` no cabe�alho da requisi��o.

### Deposit 
- **POST /api/Deposit/add**
  - **Descri��o**: Adiciona um dep�sito.
  - **[Autenticado]**
  - **Exemplo de Requisi��o**:
    ```json
    {
      "price": 0
    }
    ```
  - **Resposta**: Retorna um True(Sucesso) ou False(Insucesso)

- **POST /api/Deposit/decrease**
  - **Descri��o**: Reduz um dep�sito.
  - **[Autenticado]**
  - **Exemplo de Requisi��o**:
    ```json
    {
      "price": 0
    }
    ```
  - **Resposta**: Retorna um True(Sucesso) ou False(Insucesso)


### Investment
- **GET /api/Investment**
  - **[Autenticado]**
  - **Descri��o**: Obt�m todos os investimentos.
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
  - **Descri��o**: Obt�m detalhes de um investimento espec�fico.
  - **Requisi��o**: Deve passar o Id do investimento via pathParam.
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
  - **Descri��o**: Obt�m detalhes de um investimento espec�fico com o produto.
  - **Requisi��o**: Deve passar o Id do investimento via pathParam.
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
  - **Descri��o**: Obt�m todas as ordens.
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
  - **Descri��o**: Cria uma ordem de compra.
  - **[Autenticado]**
  - **Exemplo de Requisi��o**:
    ```json
    {
      "productId": 0,
      "price": 0,
      "amount": 0
    }
    ```
  - **Resposta**: Retorna um com o status que ficou

- **POST /api/Orders/sale**
  - **Descri��o**: Cria uma ordem de venda.
  - **[Autenticado]**
  - **Exemplo de Requisi��o**:
    ```json
    {
      "productId": 0,
      "price": 0,
      "amount": 0
    }
    ```
  - **Resposta**: Retorna um com o status que ficou

- **PUT /api/Orders/id/cancel**
  - **Descri��o**: Cancela uma ordem espec�fica.
  - **Requisi��o**: Deve passar o Id da ordem via pathParam.
  - **Resposta**: Retorna um True(Sucesso) ou False(Insucesso)


### Products
- **GET /api/Products**
  - **Descri��o**: Obt�m todos os produtos.
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
  - **Descri��o**: Cria um novo produto.
  - **[Autenticado]**: Apenas administrador pode executar
  - **Exemplo de Requisi��o**:
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
  - **Descri��o**: Atualiza um produto existente.
  - **[Autenticado]**: Apenas administrador pode executar
  - **Exemplo de Requisi��o**:
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
  - **Descri��o**: Obt�m detalhes de um produto espec�fico.
  - **[Autenticado]**
  - **Requisi��o**: Deve passar o Id do produto via pathParam.
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
  - **Descri��o**: Deleta um produto espec�fico.
  - **[Autenticado]**: Apenas administrador pode executar
  - **Requisi��o**: Deve passar o Id do produto via pathParam.
  - **Resposta**: Retorna um True(Sucesso) ou False(Insucesso)