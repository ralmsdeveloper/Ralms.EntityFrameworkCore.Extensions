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

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Ralms.EntityFrameworkCore.Extensions;
using Ralms.EntityFrameworkCore.Extensions.With;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RalmsServiceCollectionExtensions
    {
        public static IServiceCollection AddRalmsExtensions(this IServiceCollection services)
        {
            return services
                .AddSingleton<IQuerySqlGeneratorFactory, QueryGeneratorFactory>()
                .AddScoped<IQueryCompilationContextFactory, RalmsCompilationQueryableFactory>()
                .AddScoped<IEntityQueryableExpressionVisitorFactory, RalmsEntityQueryableExpressionVisitorFactory>();
        }
    }
}
