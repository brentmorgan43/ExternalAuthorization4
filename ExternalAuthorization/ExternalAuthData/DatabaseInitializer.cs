using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalAuthData
{
    public static class DatabaseInitializer
    {
        public static ExternalAuthContext Initialize(string ConnectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExternalAuthContext>();
            optionsBuilder.UseSqlServer(ConnectionString);

            ExternalAuthContext context = new ExternalAuthContext(optionsBuilder.Options);

            context.Database.EnsureCreated();

            return context;
        }
    }
}
