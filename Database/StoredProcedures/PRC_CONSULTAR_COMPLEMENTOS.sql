CREATE OR REPLACE PROCEDURE CONSINCO.PRC_CONSULTAR_COMPLEMENTOS (
    p_prd_codigo       IN  CONSINCO.PRODUTOS.PRD_CODIGO%TYPE     DEFAULT NULL,
    p_prd_descricao    IN  CONSINCO.PRODUTOS.PRD_DESCRICAO%TYPE  DEFAULT NULL,
    p_lote_fabricacao  IN  CONSINCO.COMPLEMENTOS.CMP_LOTE_FABRICACAO%TYPE DEFAULT NULL,
    p_data_criacao_de  IN  CONSINCO.COMPLEMENTOS.CMP_DATA_CRIACAO%TYPE    DEFAULT NULL,
    p_data_criacao_ate IN  CONSINCO.COMPLEMENTOS.CMP_DATA_CRIACAO%TYPE    DEFAULT NULL,
    p_cursor_out       OUT SYS_REFCURSOR
)
AS
    v_filtros_informados NUMBER(1) := 0;
BEGIN
    -- Valida se ao menos um filtro foi informado
    IF p_prd_codigo      IS NOT NULL THEN v_filtros_informados := 1; END IF;
    IF p_prd_descricao   IS NOT NULL THEN v_filtros_informados := 1; END IF;
    IF p_lote_fabricacao IS NOT NULL THEN v_filtros_informados := 1; END IF;
    IF p_data_criacao_de IS NOT NULL THEN v_filtros_informados := 1; END IF;
    IF p_data_criacao_ate IS NOT NULL THEN v_filtros_informados := 1; END IF;

    IF v_filtros_informados = 0 THEN
        RAISE_APPLICATION_ERROR(-20004, 'Informe ao menos um filtro para realizar a consulta.');
    END IF;

    OPEN p_cursor_out FOR
        SELECT
            C.CMP_ID,
            P.PRD_CODIGO,
            P.PRD_DESCRICAO,
            C.CMP_LOTE_FABRICACAO,
            C.CMP_DATA_CRIACAO,
            C.CMP_DESCRICAO_RESUMIDA
        FROM
            CONSINCO.COMPLEMENTOS C
            INNER JOIN CONSINCO.PRODUTOS P ON P.PRD_ID = C.CMP_PRD_ID
        WHERE
            (p_prd_codigo      IS NULL OR UPPER(P.PRD_CODIGO)           LIKE '%' || UPPER(p_prd_codigo)      || '%')
        AND (p_prd_descricao   IS NULL OR UPPER(P.PRD_DESCRICAO)        LIKE '%' || UPPER(p_prd_descricao)   || '%')
        AND (p_lote_fabricacao IS NULL OR UPPER(C.CMP_LOTE_FABRICACAO)  LIKE '%' || UPPER(p_lote_fabricacao) || '%')
        AND (p_data_criacao_de  IS NULL OR C.CMP_DATA_CRIACAO >= p_data_criacao_de)
        AND (p_data_criacao_ate IS NULL OR C.CMP_DATA_CRIACAO <= p_data_criacao_ate)
        ORDER BY
            P.PRD_CODIGO, C.CMP_DATA_CRIACAO DESC;

END PRC_CONSULTAR_COMPLEMENTOS;