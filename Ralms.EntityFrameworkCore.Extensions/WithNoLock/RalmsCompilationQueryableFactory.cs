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

using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.ResultOperators.Internal;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class RalmsCompilationQueryableFactory : QueryCompilationContextFactory
    {
        public RalmsCompilationQueryableFactory(
            QueryCompilationContextDependencies dependencies,
            RelationalQueryCompilationContextDependencies relationalDependencies)
            : base(dependencies)
        {
            relationalDependencies
                .NodeTypeProviderFactory
                .RegisterMethods(WithNoLockExpressionNode.SupportedMethods, typeof(WithNoLockExpressionNode)); 
        }

        public override QueryCompilationContext Create(bool async)
            => async
                ? new RelationalQueryCompilationContext(
                    Dependencies,
                    new AsyncLinqOperatorProvider(),
                    new AsyncQueryMethodProvider(),
                    TrackQueryResults)
                : new RelationalQueryCompilationContext(
                    Dependencies,
                    new LinqOperatorProvider(),
                    new QueryMethodProvider(),
                    TrackQueryResults);
    }
}
