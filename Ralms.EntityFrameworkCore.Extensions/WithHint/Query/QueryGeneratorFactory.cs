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

using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace Ralms.EntityFrameworkCore.Extensions
{
    public class QueryGeneratorFactory : QuerySqlGeneratorFactoryBase
    {
        private readonly ISqlServerOptions _sqlServerOptions;
        public QueryGeneratorFactory(
           QuerySqlGeneratorDependencies dependencies,
           ISqlServerOptions sqlServerOptions)
            : base(dependencies)
        {
            _sqlServerOptions = sqlServerOptions;
        }

        public override IQuerySqlGenerator CreateDefault(SelectExpression selectExpression)
            => new QueryGenerator(
                Dependencies,
                selectExpression,
                _sqlServerOptions.RowNumberPagingEnabled);
    }
}
