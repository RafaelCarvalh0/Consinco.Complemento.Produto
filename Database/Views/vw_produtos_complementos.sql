-- ============================================================
-- PROJETO  : Consinco - Cadastro Complementar de Produtos
-- SCRIPT   : 04_VIEW.sql
-- DESCRICAO: View para consulta rapida do usuario DBA
--            Exibe todas as informacoes de produtos e seus
--            complementos cadastrados
-- SCHEMA   : CONSINCO
-- AUTOR    : Desenvolvimento
-- DATA     : 2026
-- ============================================================


-- ------------------------------------------------------------
-- VIEW: VW_PRODUTOS_COMPLEMENTOS
-- DESCRICAO: Consolida em uma unica consulta todas as
--            informacoes de produtos e complementos.
--            Produtos sem complementos tambem sao exibidos (LEFT JOIN).
-- ------------------------------------------------------------
CREATE OR REPLACE VIEW CONSINCO.VW_PRODUTOS_COMPLEMENTOS AS
    SELECT
        -- Dados do Produto (ERP)
        P.PRD_ID                                        AS PRODUTO_ID,
        P.PRD_CODIGO                                    AS PRODUTO_CODIGO,
        P.PRD_DESCRICAO                                 AS PRODUTO_DESCRICAO,
        CASE P.PRD_ATIVO
            WHEN 'S' THEN 'Ativo'
            ELSE 'Inativo'
        END                                             AS PRODUTO_STATUS,

        -- Dados do Complemento
        C.CMP_ID                                        AS COMPLEMENTO_ID,
        C.CMP_LOTE_FABRICACAO                           AS LOTE_FABRICACAO,
        TO_CHAR(C.CMP_DATA_CRIACAO, 'DD/MM/YYYY')       AS DATA_CRIACAO,
        C.CMP_DESCRICAO_RESUMIDA                        AS DESCRICAO_RESUMIDA,

        -- Coluna auxiliar para o DBA identificar rapidamente
        CASE
            WHEN C.CMP_ID IS NULL THEN 'Sem Complemento'
            ELSE 'Com Complemento'
        END                                             AS SITUACAO_COMPLEMENTO

    FROM
        CONSINCO.PRODUTOS P
        LEFT JOIN CONSINCO.COMPLEMENTOS C ON C.CMP_PRD_ID = P.PRD_ID
    ORDER BY
        P.PRD_CODIGO, C.CMP_DATA_CRIACAO DESC;

-- Comentarios da View
COMMENT ON TABLE CONSINCO.VW_PRODUTOS_COMPLEMENTOS
    IS 'View consolidada de produtos e complementos para consulta rapida do DBA';
