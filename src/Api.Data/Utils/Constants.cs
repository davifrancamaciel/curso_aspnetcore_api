namespace Api.Data.Utils
{
    public class Constants
    {
        public static string DataBase = Env.Get("DATABASE", "MYSQL");
        //mysql
        private static string _connectionStringMySql = "Persist Security Info=True;Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=86801qaz";
        public static string ConnectionStringMySql = Env.Get("DB_CONNECTION_MYSQL", _connectionStringMySql);

        private static string _connectionStringMySqlTest = "Persist Security Info=True;Server=localhost;Port=3306;Database=DATABASE_TEST_NAME;Uid=root;Pwd=86801qaz";
        public static string ConnectionStringMySqlTest = Env.Get("DB_CONNECTION_MYSQL_TEST", _connectionStringMySql);
        //sqlserver
        private static string _connectionStringSqlServer = "Server=.\\SQLEXPRESS2017;Database=dbAPI;User Id=sa;Password=8680";
        public static string ConnectionStringSqlServer = Env.Get("DB_CONNECTION_SQLSERVER", _connectionStringSqlServer);


    }
}
