using System;
using Api.Data.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Usado para Criar as Migrações
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

            if (Constants.DataBase.ToUpper().Equals("MYSQL"))
                optionsBuilder.UseMySql(Constants.ConnectionStringMySql);
            else
                optionsBuilder.UseSqlServer(Constants.ConnectionStringSqlServer);

            return new MyContext(optionsBuilder.Options);
        }
    }
}
