CREATE OR REPLACE PROCEDURE CONSINCO.PRC_INSERIR_COMPLEMENTO (
    p_prd_id             IN  CONSINCO.COMPLEMENTOS.CMP_PRD_ID%TYPE,
    p_lote_fabricacao    IN  CONSINCO.COMPLEMENTOS.CMP_LOTE_FABRICACAO%TYPE,
    p_data_criacao       IN  CONSINCO.COMPLEMENTOS.CMP_DATA_CRIACAO%TYPE DEFAULT SYSDATE,
    p_descricao_resumida IN  CONSINCO.COMPLEMENTOS.CMP_DESCRICAO_RESUMIDA%TYPE,
    p_cmp_id_out         OUT CONSINCO.COMPLEMENTOS.CMP_ID%TYPE
)
AS
    v_produto_existe NUMBER(1);
BEGIN
    -- Valida se o produto informado existe
    SELECT COUNT(1)
      INTO v_produto_existe
      FROM CONSINCO.PRODUTOS
     WHERE PRD_ID = p_prd_id
       AND PRD_ATIVO = 'S';

    IF v_produto_existe = 0 THEN
        RAISE_APPLICATION_ERROR(-20001, 'Produto nao encontrado ou inativo. ID: ' || p_prd_id);
    END IF;

    -- Insere o complemento
    INSERT INTO CONSINCO.COMPLEMENTOS (
        CMP_PRD_ID,
        CMP_LOTE_FABRICACAO,
        CMP_DATA_CRIACAO,
        CMP_DESCRICAO_RESUMIDA
    ) VALUES (
        p_prd_id,
        p_lote_fabricacao,
        p_data_criacao,
        p_descricao_resumida
    )
    RETURNING CMP_ID INTO p_cmp_id_out;

    COMMIT;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END PRC_INSERIR_COMPLEMENTO;