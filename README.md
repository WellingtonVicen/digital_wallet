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
DO $$ 
BEGIN 
    -- Popula a tabela 'users'
    INSERT INTO "users" ("Name", "Email", "PasswordHash", "CreatedAt")
    SELECT 
        first_names || ' ' || last_names AS Name,
        LOWER(first_names || '.' || last_names || '@example.com') AS Email,
        crypt('Password123', gen_salt('bf')) AS PasswordHash,
        NOW() AS CreatedAt
    FROM (
        SELECT 
            unnest(ARRAY['John', 'Jane', 'Michael', 'Emily', 'Chris', 'Sarah', 'David', 'Laura', 'James', 'Emma']) AS first_names,
            unnest(ARRAY['Smith', 'Johnson', 'Brown', 'Taylor', 'Anderson', 'Thomas', 'Jackson', 'White', 'Harris', 'Martin']) AS last_names
    ) AS names 
    LIMIT 50;
END $$;

-- Popula a tabela 'wallets'
DO $$ 
BEGIN
    FOR i IN 1..10 LOOP
        INSERT INTO "wallets" ("Id", "Balance", "UserId")
        VALUES (
            i, 
            random() * 10000::numeric,  -- Converte o valor para numeric e arredonda com 2 casas decimais
            i -- Usa o valor de 'i' como 'UserId'
        );
    END LOOP;
END $$;

   -- Popula a tabela 'transactions'
DO $$ 
BEGIN
    FOR i IN 1..50 LOOP
       INSERT INTO public.transactions(
	"Id", "FromWalletId", "ToWalletId", "Amount", "Type", "Description", "CreatedAt")
        VALUES (
            i,
            (i % 50) + 1, 
			 (i % 50) + 1,  
            random() * 1000::numeric,  -- Converte para 'numeric' e arredonda com 2 casas decimais
             'Credit',  -- Determina o tipo de transação
			 'TESTE',
            NOW()  
        );
    END LOOP;
END $$;

END $$;
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

