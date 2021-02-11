using System.Configuration;


namespace ISDhhMuszakBeosztasDataAccess.DBHelper
{
    public static class Helper
    {
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
