-- ------------------------------------------------------------
-- TABELA: COMPLEMENTOS
-- Tabela criada para armazenar as informacoes complementares
-- dos produtos, conforme requisito da proposta
-- ------------------------------------------------------------
CREATE TABLE CONSINCO.COMPLEMENTOS (
    CMP_ID                  NUMBER(10)      NOT NULL,
    CMP_PRD_ID              NUMBER(10)      NOT NULL,
    CMP_LOTE_FABRICACAO     VARCHAR2(50)    NOT NULL,
    CMP_DATA_CRIACAO        DATE            DEFAULT SYSDATE NOT NULL,
    CMP_DESCRICAO_RESUMIDA  VARCHAR2(500)   NOT NULL,
    CONSTRAINT PK_COMPLEMENTOS         PRIMARY KEY (CMP_ID),
    CONSTRAINT FK_COMPLEMENTOS_PRODUTO FOREIGN KEY (CMP_PRD_ID)
        REFERENCES CONSINCO.PRODUTOS (PRD_ID)
);

COMMENT ON TABLE  CONSINCO.COMPLEMENTOS                     IS 'Informacoes complementares dos produtos - Modulo Consinco';
COMMENT ON COLUMN CONSINCO.COMPLEMENTOS.CMP_ID              IS 'Identificador unico do complemento';
COMMENT ON COLUMN CONSINCO.COMPLEMENTOS.CMP_PRD_ID          IS 'FK - Referencia ao produto em PRODUTOS.PRD_ID';
COMMENT ON COLUMN CONSINCO.COMPLEMENTOS.CMP_LOTE_FABRICACAO IS 'Numero ou codigo do lote de fabricacao';
COMMENT ON COLUMN CONSINCO.COMPLEMENTOS.CMP_DATA_CRIACAO    IS 'Data de criacao do registro complementar';
COMMENT ON COLUMN CONSINCO.COMPLEMENTOS.CMP_DESCRICAO_RESUMIDA IS 'Descricao resumida do complemento do produto';


-- ------------------------------------------------------------
-- SEQUENCE + TRIGGER: Auto incremento para COMPLEMENTOS
-- ------------------------------------------------------------
CREATE SEQUENCE CONSINCO.SEQ_COMPLEMENTOS
    START WITH 1
    INCREMENT BY 1
    NOCACHE
    NOCYCLE;

CREATE OR REPLACE TRIGGER CONSINCO.TRG_COMPLEMENTOS_BI
    BEFORE INSERT ON CONSINCO.COMPLEMENTOS
    FOR EACH ROW
BEGIN
    IF :NEW.CMP_ID IS NULL THEN
        :NEW.CMP_ID := CONSINCO.SEQ_COMPLEMENTOS.NEXTVAL;
    END IF;
END;
/


-- ------------------------------------------------------------
-- INDEX: Melhora a performance de consultas por produto
-- ------------------------------------------------------------
CREATE INDEX CONSINCO.IDX_COMPLEMENTOS_PRD_ID
    ON CONSINCO.COMPLEMENTOS (CMP_PRD_ID);