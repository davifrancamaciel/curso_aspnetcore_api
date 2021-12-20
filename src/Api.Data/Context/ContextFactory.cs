using System;
using Api.Data.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Usado para Criar as Migrações
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

            if (Constants.DataBase.ToUpper().Equals("MYSQL"))
                optionsBuilder.UseMySql(Constants.ConnectionStringMySql, new MySqlServerVersion(new Version(8, 0, 21))
                    //, mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)
                );
            else
                optionsBuilder.UseSqlServer(Constants.ConnectionStringSqlServer);

            return new MyContext(optionsBuilder.Options);
        }
    }
}
