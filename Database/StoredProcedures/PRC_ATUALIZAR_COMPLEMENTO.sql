CREATE OR REPLACE PROCEDURE CONSINCO.PRC_ATUALIZAR_COMPLEMENTO (
    p_cmp_id             IN CONSINCO.COMPLEMENTOS.CMP_ID%TYPE,
    p_lote_fabricacao    IN CONSINCO.COMPLEMENTOS.CMP_LOTE_FABRICACAO%TYPE,
    p_data_criacao       IN CONSINCO.COMPLEMENTOS.CMP_DATA_CRIACAO%TYPE,
    p_descricao_resumida IN CONSINCO.COMPLEMENTOS.CMP_DESCRICAO_RESUMIDA%TYPE
)
AS
    v_registros_afetados NUMBER(10);
BEGIN
    UPDATE CONSINCO.COMPLEMENTOS
       SET CMP_LOTE_FABRICACAO    = p_lote_fabricacao,
           CMP_DATA_CRIACAO       = p_data_criacao,
           CMP_DESCRICAO_RESUMIDA = p_descricao_resumida
     WHERE CMP_ID = p_cmp_id;

    v_registros_afetados := SQL%ROWCOUNT;

    IF v_registros_afetados = 0 THEN
        RAISE_APPLICATION_ERROR(-20002, 'Complemento nao encontrado. ID: ' || p_cmp_id);
    END IF;

    COMMIT;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END PRC_ATUALIZAR_COMPLEMENTO;