/*
 *          Copyright (c) 2018 Rafael Almeida (ralms@ralms.net)
 *
 *           Ralms.Microsoft.EntitityFrameworkCore.Extensions
 *
 * THIS MATERIAL IS PROVIDED AS IS, WITH ABSOLUTELY NO WARRANTY EXPRESSED
 * OR IMPLIED.  ANY USE IS AT YOUR OWN RISK.
 *
 * Permission is hereby granted to use or copy this program
 * for any purpose,  provided the above notices are retained on all copies.
 * Permission to modify the code and to distribute modified code is granted,
 * provided the above notices are retained, and a notice that the code was
 * modified is included with the above copyright notice.
 *
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ralms.EntityFrameworkCore.Tests
{
    public class SampleContext : DbContext
    {
        private readonly string NameDatabase = "";
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public SampleContext(string database)
        {
            NameDatabase = database;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={NameDatabase};Integrated Security=True;")
                .RalmsExtendFunctions()
                .UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelo)
        {
            modelo.EnableSqlServerDateDIFF();
        }

        private static readonly ILoggerFactory _loggerFactory
            = new LoggerFactory().AddConsole((s, l) => l == LogLevel.Information && !s.EndsWith("Connection"));
    }
}
