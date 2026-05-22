-- ============================================================
-- PROJETO  : Consinco - Cadastro Complementar de Produtos
-- SCRIPT   : 00_SETUP.sql
-- DESCRICAO: Configuracao inicial do schema e usuario Oracle
--            Executar UMA UNICA VEZ conectado como SYSTEM
-- ============================================================

-- Cria o usuario da aplicacao
CREATE USER consinco IDENTIFIED BY Admin1234;

-- Concede as permissoes necessarias
GRANT CONNECT, RESOURCE, DBA TO consinco;