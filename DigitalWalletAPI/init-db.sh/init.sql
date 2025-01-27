-- Criação do usuário e banco de dados
CREATE USER digitalwallet_user WITH PASSWORD 'digital@12';
CREATE DATABASE digitalwalletdbhomolog;
GRANT ALL PRIVILEGES ON DATABASE digitalwalletdbhomolog TO digitalwallet_user;