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

using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore
{
    public static class RalmsQueryableExtensions
    {
        #region WithNoLock
        internal static readonly MethodInfo WithNoLockMethodInfo
            = typeof(RalmsQueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(WithNoLock))
                .Single();

        public static IQueryable<TEntity> WithNoLock<TEntity>(
            this IQueryable<TEntity> source,
            [NotParameterized] bool withNoLock = true)
            where TEntity : class
        {
            return source.Provider.CreateQuery<TEntity>(
                Expression.Call(
                    null,
                    WithNoLockMethodInfo.MakeGenericMethod(typeof(TEntity)),
                    source.Expression,
                    Expression.Constant(withNoLock, typeof(bool))));
        }
        #endregion 
    }
}
