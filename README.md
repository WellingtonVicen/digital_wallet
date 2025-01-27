# Digital Wallet API

## Visão Geral do Projeto

O Digital Wallet API é uma aplicação de carteira digital projetada para gerenciar contas de usuários, realizar transferências financeiras e manter um histórico de transações. O projeto é desenvolvido utilizando .NET 8.0 e segue os princípios de arquitetura limpa (Clean Architecture), incluindo práticas de CQRS e SOLID.

### Funcionalidades Principais

- Gerenciamento de usuários e carteiras.
- Realização de transferências financeiras entre usuários.
- Consulta de transações com filtros opcionais.

## Tecnologias Utilizadas

- **.NET 8.0**
- **Entity Framework Core** para interações com o banco de dados.
- **PostgreSQL** como banco de dados relacional.
- **Docker** para conteinerização.
- **MediatR** para implementação do padrão CQRS.

## Estrutura do Projeto

- **API**: Controladores para expor endpoints REST.
- **Application**: Contém casos de uso, validações e lógica de aplicação.
- **Domain**: Regras de negócio e entidades principais.
- **Infrastructure**: Configuração de banco de dados e repositórios.

---

## Como Executar o Projeto

### Pré-requisitos

1. **Docker** e **Docker Compose** instalados.
2. **Visual Studio 2022** (ou IDE equivalente com suporte a .NET 8.0).

### Passo a Passo

1. **Clone o repositório**
   ```bash
   git clone <URL_DO_REPOSITORIO>
   cd digital_wallet
   ```

2. **Configuração do Docker**
   Certifique-se de que os arquivos `Dockerfile` e `docker-compose.yml` estejam no diretório raiz do projeto.

3. **Suba os contêineres**
   ```bash
   docker-compose up --build
   ```

4. **Acesse os serviços**
   - API: `http://localhost:8080`
   - Swagger: `http://localhost:8080/swagger`
   - pgAdmin: `http://localhost:5050`
     - Login: `admin@admin.com`
     - Senha: `admin`
     - Host: `database`

5. **Popule o banco de dados**

   Utilize o script SQL abaixo para popular as tabelas do banco de dados com 50 registros:

   ```sql
   INSERT INTO "Users" ("Name", "Email", "PasswordHash")
   SELECT md5(random()::text), md5(random()::text) || '@example.com', md5(random()::text)
   FROM generate_series(1, 50);

   INSERT INTO "Wallets" ("UserId", "Balance")
   SELECT "Id", round(random() * 10000, 2)
   FROM "Users"
   LIMIT 50;

   INSERT INTO "Transactions" ("WalletId", "Amount", "Timestamp")
   SELECT "Id", round(random() * 1000 - 500, 2), NOW()
   FROM "Wallets"
   LIMIT 50;
   ```

   Você pode executar este script diretamente no pgAdmin ou utilizando qualquer ferramenta de SQL conectada ao banco PostgreSQL.

6. **Testando a API**

   Exemplos de endpoints disponíveis:

   - **Criar Usuário**: `POST /api/users`
   - **Criar Carteira**: `POST /api/wallets`
   - **Transferências**: `POST /api/transactions`
   - **Listar Transações**: `GET /api/transactions?userId=<id>&startDate=<date>&endDate=<date>`

   Utilize ferramentas como Postman ou Swagger (disponível em `http://localhost:8080/swagger`) para interagir com a API.

---

