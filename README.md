# Digital Wallet API

## Descrição Geral

O **Digital Wallet API** é uma aplicação desenvolvida em **.NET 8** que fornece uma solução completa para o gerenciamento de carteiras digitais. A API permite o cadastro de usuários, criação e manipulação de carteiras, bem como a execução de transações financeiras, como créditos e débitos.

## Estrutura do Projeto

### Principais Componentes

1. **Arquitetura:** O projeto segue uma arquitetura modular com as seguintes camadas:
   - **Domain:** Define as entidades e as regras de negócio.
   - **Application:** Contém os casos de uso e a lógica de aplicação.
   - **Infrastructure:** Gerencia a comunicação com o banco de dados e outros recursos externos.
   - **IoC (Inversion of Control):** Configurações para injeção de dependências.

2. **Banco de Dados:**
   - Utiliza **PostgreSQL** para persistência.
   - Inclui tabelas como `User`, `Wallet` e `Transaction` para organizar os dados do sistema.

3. **Docker:**
   - Contém um **Dockerfile** e um arquivo **docker-compose** para a orquestração dos contêineres.
   - Configurações incluem substituição de portas padrão para evitar conflitos.
   - Inclui serviços para a API e o banco PostgreSQL.

4. **Funcionalidades:**
   - Cadastro e gerenciamento de usuários.
   - Criação e consulta de carteiras com saldo.
   - Execução de transações financeiras associadas às carteiras (créditos e débitos).

5. **Script de População:**
   - Um script SQL é disponibilizado para popular o banco de dados com 50 registros fictícios em cada tabela, facilitando os testes e demonstrações.

## Tecnologias Utilizadas
- .NET 8
- PostgreSQL
- Docker/Docker Compose
- Entity Framework Core
- MediatR (para implementação de padrão CQRS)

## Estrutura de Diretórios

```
DigitalWalletAPI/
├── Application/               # Camada de Aplicação (Casos de Uso)
├── Domain/                    # Núcleo do Domínio
├── Infrastructure/            # Implementação de Infraestrutura
├── IoC/                       # Configuração de Injeção de Dependência
├── API/                       # Controllers e Entrada da API
├── Tests/                     # Testes Unitários e de Integração
├── docker-compose.yml         # Configuração do Docker Compose
└── Dockerfile                 # Configuração de imagem Docker
```

## Configuração do Docker

O **docker-compose.yml** define dois serviços principais:

- **API:** Responsável por executar a aplicação.
- **Database:** Um contêiner PostgreSQL configurado com o banco `digital_wallet`.

### Comandos Principais

1. Build e inicialização dos contêineres:
   ```bash
   docker-compose up --build
   ```

2. Parar os contêineres:
   ```bash
   docker-compose down
   ```

## Endpoints Principais

- **Usuários:**
  - `POST /users` - Criação de usuário.
  - `GET /users/{id}` - Consulta de usuário por ID.

- **Carteiras:**
  - `POST /wallets` - Criação de carteira.
  - `GET /wallets/{id}` - Consulta de carteira por ID.

- **Transações:**
  - `POST /transactions` - Realiza uma transação (crédito ou débito).
  - `GET /transactions` - Lista transações filtradas por período (opcional).

## Observações Finais
Este projeto tem como objetivo fornecer uma base escalável e bem estruturada para aplicações financeiras, permitindo expansões futuras como relatórios ou integração com sistemas externos.

