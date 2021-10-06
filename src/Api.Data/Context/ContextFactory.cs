using System;
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

            if (Environment.GetEnvironmentVariable("DATABASE").ToUpper().Equals("MYSQL"))
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION_MYSQL"));
            else
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION_SQLSERVER"));

            return new MyContext(optionsBuilder.Options);
        }
    }
}
