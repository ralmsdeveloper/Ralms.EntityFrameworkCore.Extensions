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
using System.Linq.Expressions;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;
using Remotion.Linq.Clauses.StreamedData;

namespace Microsoft.EntityFrameworkCore.Query.ResultOperators.Internal
{
    public class HintResultOperator : SequenceTypePreservingResultOperatorBase, IQueryAnnotation
    {
        public HintResultOperator(string hint, bool repeatForInclude)
        {
            Hint = hint;
            RepeatForInclude = repeatForInclude;
        }
        
        public virtual bool RepeatForInclude { get; }
        public virtual string Hint { get; }
        public virtual IQuerySource QuerySource { get; set; }
        public virtual QueryModel QueryModel { get; set; }
        public override string ToString() => Hint;
        public override ResultOperatorBase Clone(CloneContext cloneContext)
            => new HintResultOperator(Hint,RepeatForInclude);

        public override void TransformExpressions(Func<Expression, Expression> transformation)
        {
        }

        public override StreamedSequence ExecuteInMemory<T>(StreamedSequence input) => input;
    }
}
