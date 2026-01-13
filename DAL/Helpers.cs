using System.Text;

namespace estoque_api.DAL
{
    public static class Helpers
    {
        public static System.Data.Common.DbParameter criarParametro(string nome, object value)
        {
            System.Data.Common.DbParameter p = null;
            p = new MySql.Data.MySqlClient.MySqlParameter(nome.ParameterName(), value == null ? DBNull.Value : value);
            return p;
        }
        public static string ParameterName(this string str)
        {
            StringBuilder sb = new StringBuilder();
            if (str.Contains(","))
            {
                string[] p = str.Split(',');
                foreach (var item in p)
                {
                    if (!item.Trim().Equals(""))
                    {
                        sb.Append(getParametro(item.Trim()));
                    }
                    sb.Append(",");
                }
                if (sb.ToString().EndsWith(","))
                    sb.Remove(sb.Length - 1, 1);
            }
            else
            {
                sb.Append(getParametro(str));
            }
            return sb.ToString();
        }
        private static string getParametro(string str)
        {
            string[] parameter = { ":", "@" };
            str = parameter.Contains(str.Substring(0, 1)) ? str.Substring(1) : str;
            if (str.ToLower().Trim().Equals("sysdate") || str.ToLower().Trim().Equals("sysdate()"))
                str = "SYSDATE()";
            else
                str = string.Concat("@", str);
            return str;
        }
    }
}