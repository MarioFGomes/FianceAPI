# FinanceAPI

API para gestão de usuários, carteiras digitais e transações financeiras.

## Funcionalidades
- Cadastro e autenticação de usuários (SignUp/SignIn)
- Criação e gerenciamento de carteiras digitais
- Depósito, saque, transferência entre carteiras
- Consulta de saldo e extrato de movimentações
- Habilitar/desabilitar carteiras

## Como rodar o projeto

Para facilitar os testes da API foi feita a hospedagem da mesma no render estando assim On-line e pronta para uso
para ir fazendo as suas requisições na API basta acessar o endereço logo abaixo 
pode ter acesso a exemplos de requisições no arquivo `FinanceAPI.postman_collection.json` que se encontra na raiz do projeto
ou no final deste README.

`https://fianceapi.onrender.com/api`

#### Nota: No plano free do render a API é posta em estado de Sleep depois de 5 minutos em receber requesições e volta a estar up depois de receber alguma requesição

### Usando Docker Compose

1. Clone o repositório e acesse a pasta do projeto.
2. Execute:

```bash
docker-compose up --build
```
or 
Cria um arquivo com o nome .env e passa todas as variaveis de ambiente solicitadas no arquivo docker-compose de produção
e depois é só rodar

```bash
docker compose -f docker-compose.prod.yml up -d
```

- A API estará disponível em: `http://localhost:5000` (ajuste a porta conforme necessário)
- O banco de dados PostgreSQL estará disponível na porta 5432.

### Nota:O banco de dados só ficara disponível na primeira requisição

### Usando Docker manualmente

Suba o banco de dados:
```bash
docker run -d --name PostgresSQL -e POSTGRESQL_USERNAME=mariogomes -e POSTGRESQL_PASSWORD=1qaz2wsx -e POSTGRESQL_DATABASE=finance -p 5432:5432 bitnami/postgresql:latest
```

Construa e rode a API:
```bash
docker build -f src/Bckend/Finance.API/Dockerfile -t finance_api .
docker run -d --name finance_api --link PostgresSQL:db -p 5000:80 finance_api
```

### Rodando localmente (sem Docker)

1. Instale o .NET 7.0 SDK e o PostgreSQL.
2. Configure a string de conexão no `appsettings.Development.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=finance;Username=mariogomes;Password=1qaz2wsx"
   }
   ```
3. Restaure os pacotes e rode a aplicação:
   ```bash
   dotnet restore
   dotnet run --project src/Bckend/Finance.API/Finance.API.csproj
   ```

## Exemplos de requisições (API)

### Autenticação

#### Cadastro (SignUp)
```http
POST /api/Auth/SingUp
Content-Type: application/json

{
  "name": "Mário Gomes",
  "email": "marioferreiragomes333@gmail.com",
  "password": "!qaz2wsxQAZ"
}
```

#### Login (SignIn)
```http
POST /api/Auth/SingIn
Content-Type: application/json

{
  "email": "marioferreiragomes333@gmail.com",
  "password": "!qaz2wsxQAZ"
}
```

### Usuário

#### Buscar usuário
```http
GET /api/User/fetch?search=Mário
Authorization: Bearer <token>
```

### Carteira

#### Criar carteira
```http
POST /api/Wallet/Create
Authorization: Bearer <token>
Content-Type: application/json

{
  "balance": 0,
  "currency": "AOA"
}
```

#### Depositar
```http
POST /api/Wallet/deposit
Authorization: Bearer <token>
Content-Type: application/json

{
  "amount": 20000,
  "description": "Deposito inicial",
  "currency": "AOA"
}
```

#### Sacar
```http
POST /api/Wallet/withdraw
Authorization: Bearer <token>
Content-Type: application/json

{
  "amount": 2000,
  "description": "Levantamento em caixa eletronico",
  "currency": "BRL"
}
```

#### Transferir
```http
POST /api/Wallet/Transfer
Authorization: Bearer <token>
Content-Type: application/json

{
  "receiverWalletId": "<id-da-carteira-destino>",
  "amount": 3000,
  "description": "Tranferencia para elisa",
  "currency": "AOA"
}
```

#### Consultar movimentações
```http
GET /api/Wallet/Movements?currency=AOA&startDate=2025-07-06&endDate=2025-07-10&page=1&pageSize=10
Authorization: Bearer <token>
```

#### Consultar saldo
```http
GET /api/Wallet/CheckBalance?currency=AOA
Authorization: Bearer <token>
```

#### Desabilitar carteira
```http
PUT /api/Wallet/Disabled?currency=AOA
Authorization: Bearer <token>
```

#### Habilitar carteira
```http
PUT /api/Wallet/Enabled?currency=AOA
Authorization: Bearer <token>
```

---

> Para mais exemplos, consulte o arquivo `FinanceAPI.postman_collection.json`.

