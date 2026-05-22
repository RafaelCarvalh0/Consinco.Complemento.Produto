CREATE OR REPLACE PROCEDURE CONSINCO.PRC_EXCLUIR_COMPLEMENTO (
    p_cmp_id IN CONSINCO.COMPLEMENTOS.CMP_ID%TYPE
)
AS
    v_registros_afetados NUMBER(10);
BEGIN
    DELETE FROM CONSINCO.COMPLEMENTOS
     WHERE CMP_ID = p_cmp_id;

    v_registros_afetados := SQL%ROWCOUNT;

    IF v_registros_afetados = 0 THEN
        RAISE_APPLICATION_ERROR(-20003, 'Complemento nao encontrado. ID: ' || p_cmp_id);
    END IF;

    COMMIT;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END PRC_EXCLUIR_COMPLEMENTO;