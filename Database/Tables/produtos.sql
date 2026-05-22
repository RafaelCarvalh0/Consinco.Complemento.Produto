-- ============================================================
-- PROJETO  : Consinco - Cadastro Complementar de Produtos
-- SCRIPT   : 01_DDL_TABLES.sql
-- DESCRICAO: Criacao das tabelas PRODUTOS e COMPLEMENTOS
-- SCHEMA   : CONSINCO
-- AUTOR    : Desenvolvimento
-- DATA     : 2026
-- ============================================================


-- ------------------------------------------------------------
-- TABELA: PRODUTOS
-- Simula a tabela de produtos ja existente no ERP TOTVS
-- ------------------------------------------------------------
CREATE TABLE CONSINCO.PRODUTOS (
    PRD_ID          NUMBER(10)      NOT NULL,
    PRD_CODIGO      VARCHAR2(20)    NOT NULL,
    PRD_DESCRICAO   VARCHAR2(200)   NOT NULL,
    PRD_ATIVO       CHAR(1)         DEFAULT 'S' NOT NULL,
    CONSTRAINT PK_PRODUTOS        PRIMARY KEY (PRD_ID),
    CONSTRAINT UQ_PRODUTOS_CODIGO UNIQUE      (PRD_CODIGO),
    CONSTRAINT CK_PRODUTOS_ATIVO  CHECK      (PRD_ATIVO IN ('S', 'N'))
);

COMMENT ON TABLE  CONSINCO.PRODUTOS              IS 'Tabela de produtos do ERP TOTVS Consinco';
COMMENT ON COLUMN CONSINCO.PRODUTOS.PRD_ID       IS 'Identificador unico do produto';
COMMENT ON COLUMN CONSINCO.PRODUTOS.PRD_CODIGO   IS 'Codigo do produto no ERP';
COMMENT ON COLUMN CONSINCO.PRODUTOS.PRD_DESCRICAO IS 'Descricao completa do produto';
COMMENT ON COLUMN CONSINCO.PRODUTOS.PRD_ATIVO    IS 'Indica se o produto esta ativo: S=Sim, N=Nao';


-- ------------------------------------------------------------
-- SEQUENCE + TRIGGER: Auto incremento para PRODUTOS
-- ------------------------------------------------------------
CREATE SEQUENCE CONSINCO.SEQ_PRODUTOS
    START WITH 1
    INCREMENT BY 1
    NOCACHE
    NOCYCLE;

CREATE OR REPLACE TRIGGER CONSINCO.TRG_PRODUTOS_BI
    BEFORE INSERT ON CONSINCO.PRODUTOS
    FOR EACH ROW
BEGIN
    IF :NEW.PRD_ID IS NULL THEN
        :NEW.PRD_ID := CONSINCO.SEQ_PRODUTOS.NEXTVAL;
    END IF;
END;