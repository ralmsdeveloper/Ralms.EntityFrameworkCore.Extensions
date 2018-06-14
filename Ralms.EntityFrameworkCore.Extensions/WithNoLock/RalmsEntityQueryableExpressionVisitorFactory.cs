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
using Remotion.Linq.Clauses;
using System.Linq.Expressions;

namespace Ralms.EntityFrameworkCore.Extensions.WithNoLock
{
    public class RalmsEntityQueryableExpressionVisitorFactory : IEntityQueryableExpressionVisitorFactory
    {
        public RalmsEntityQueryableExpressionVisitorFactory(
            RelationalEntityQueryableExpressionVisitorDependencies dependencies)
            => Dependencies = dependencies;

        protected virtual RelationalEntityQueryableExpressionVisitorDependencies Dependencies { get; }

        public virtual ExpressionVisitor Create(
            EntityQueryModelVisitor queryModelVisitor, IQuerySource querySource)
            => new RalmsEntityQueryableExpressionVisitor(
                Dependencies,
                (RelationalQueryModelVisitor)queryModelVisitor,
                querySource);
    }
}