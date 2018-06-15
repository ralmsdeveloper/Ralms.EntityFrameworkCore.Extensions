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
using Remotion.Linq.Clauses;

namespace Ralms.EntityFrameworkCore.Extensions.WithNoLock.Query
{
    public class TableExpressionExtension : TableExpression
    {
        public virtual string Hint { get; }
        public virtual bool WithNoLock { get; }
        public TableExpressionExtension(
            string table, 
            string schema, 
            string alias, 
            bool withNoLock,
            string hint,
            IQuerySource querySource) 
            :base(table,schema,alias,querySource)
        {
            WithNoLock = withNoLock;
            Hint = hint;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return GetType().GetHashCode() ^ (base.GetHashCode() * 397);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() 
                && Equals((TableExpressionExtension)obj);
        }

        private bool Equals(TableExpressionExtension oldExpression)
            => string.Equals(Table, oldExpression.Table)
               && string.Equals(Schema, oldExpression.Schema)
               && string.Equals(Alias, oldExpression.Alias)
               && string.Equals(Hint, oldExpression.Hint)
               && Equals(QuerySource, oldExpression.QuerySource);

        public override string ToString() 
            => Table + " " + Alias + (WithNoLock ? " WITH (NOLOCK)" : "") + (!WithNoLock ? Hint : "");
    }
}
