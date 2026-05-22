using System;

namespace Consinco.Complemento.Produto.Data
{
    public static class Utils
    {
        public static object DBNullParse(object parametro)
        {
            if (parametro == null)
            {
                return DBNull.Value;
            }

            return parametro;
        }
    }
}
