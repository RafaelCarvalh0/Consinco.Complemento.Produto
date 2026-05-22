-- ============================================================
-- PROJETO  : Consinco - Cadastro Complementar de Produtos
-- SCRIPT   : 02_DML_SEED.sql
-- DESCRICAO: Carga inicial de dados na tabela PRODUTOS
--            Simula produtos ja existentes no ERP TOTVS
-- SCHEMA   : CONSINCO
-- AUTOR    : Desenvolvimento
-- DATA     : 2026
-- ============================================================


-- ------------------------------------------------------------
-- CARGA: PRODUTOS (simulacao ERP TOTVS)
-- ------------------------------------------------------------
INSERT INTO CONSINCO.PRODUTOS (PRD_CODIGO, PRD_DESCRICAO, PRD_ATIVO)
VALUES ('PRD-00001', 'Arroz Branco Tipo 1 - 5kg', 'S');

INSERT INTO CONSINCO.PRODUTOS (PRD_CODIGO, PRD_DESCRICAO, PRD_ATIVO)
VALUES ('PRD-00002', 'Feijao Carioca - 1kg', 'S');

INSERT INTO CONSINCO.PRODUTOS (PRD_CODIGO, PRD_DESCRICAO, PRD_ATIVO)
VALUES ('PRD-00003', 'Oleo de Soja Refinado - 900ml', 'S');

INSERT INTO CONSINCO.PRODUTOS (PRD_CODIGO, PRD_DESCRICAO, PRD_ATIVO)
VALUES ('PRD-00004', 'Acucar Cristal - 2kg', 'S');

INSERT INTO CONSINCO.PRODUTOS (PRD_CODIGO, PRD_DESCRICAO, PRD_ATIVO)
VALUES ('PRD-00005', 'Macarrao Espaguete - 500g', 'S');

INSERT INTO CONSINCO.PRODUTOS (PRD_CODIGO, PRD_DESCRICAO, PRD_ATIVO)
VALUES ('PRD-00006', 'Sal Refinado Iodado - 1kg', 'S');

INSERT INTO CONSINCO.PRODUTOS (PRD_CODIGO, PRD_DESCRICAO, PRD_ATIVO)
VALUES ('PRD-00007', 'Cafe Torrado e Moido - 500g', 'S');

INSERT INTO CONSINCO.PRODUTOS (PRD_CODIGO, PRD_DESCRICAO, PRD_ATIVO)
VALUES ('PRD-00008', 'Farinha de Trigo - 1kg', 'S');

INSERT INTO CONSINCO.PRODUTOS (PRD_CODIGO, PRD_DESCRICAO, PRD_ATIVO)
VALUES ('PRD-00009', 'Leite Integral UHT - 1L', 'S');

INSERT INTO CONSINCO.PRODUTOS (PRD_CODIGO, PRD_DESCRICAO, PRD_ATIVO)
VALUES ('PRD-00010', 'Manteiga com Sal - 200g', 'N');

COMMIT;
