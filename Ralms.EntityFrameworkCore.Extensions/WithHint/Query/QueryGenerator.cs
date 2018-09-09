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
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Sql.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Ralms.EntityFrameworkCore.Extensions.With.Query;

namespace Ralms.EntityFrameworkCore.Extensions
{
    public class QueryGenerator : SqlServerQuerySqlGenerator
    {
        public QueryGenerator(
            QuerySqlGeneratorDependencies dependencies,
            SelectExpression selectExpression,
            bool rowNumberPagingEnabled)
            : base(dependencies, selectExpression, rowNumberPagingEnabled)
        { 
        }

        public override Expression VisitTable(TableExpression tableExpression)
        {
            var table =  tableExpression as TableExpressionExtension;

            var visitTable = base.VisitTable(table);

            if (!string.IsNullOrWhiteSpace(table.Hint))
            {
                Sql.Append($" WITH ({table.Hint}) ");
            }
            
            return visitTable;
        } 
    }
}
