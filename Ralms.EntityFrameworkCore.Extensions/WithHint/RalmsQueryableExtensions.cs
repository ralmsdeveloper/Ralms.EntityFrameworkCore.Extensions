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

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore
{
    public static class RalmsQueryableExtensions
    {
        #region WithHint
        internal static readonly MethodInfo WithHintMethodInfo
            = typeof(RalmsQueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(WithHint))
                .Single();

        public static IQueryable<TEntity> WithHint<TEntity>(
            this IQueryable<TEntity> source,
            [NotParameterized] string hint)
            where TEntity : class
        {
            var infrastructure = source as IInfrastructure<IServiceProvider>;
            var serviceProvider = infrastructure.Instance;
            var currentDbContext = serviceProvider.GetService(typeof(ICurrentDbContext))
                                       as ICurrentDbContext;
            var providerName = currentDbContext.Context.Database.ProviderName;

            if (providerName != "Microsoft.EntityFrameworkCore.SqlServer")
                return source;

            return source.Provider.CreateQuery<TEntity>(
                Expression.Call(
                    null,
                    WithHintMethodInfo.MakeGenericMethod(typeof(TEntity)),
                    source.Expression,
                    Expression.Constant(hint, typeof(string))));
        }
        #endregion 
    }
}

