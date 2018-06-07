/* Copyright (c) .NET Foundation. All rights reserved.
   Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

               Fork: https://github.com/aspnet/EntityFrameworkCore
 

 *          Copyright (c) 2017-2018 Rafael Almeida (ralms@ralms.net)
 *
 *                    Ralms.EntityFrameworkCore.Extensions
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

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Ralms.EntityFrameworkCore.Extensions.Proxies.Internal;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ProxiesServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkProxies(
            this IServiceCollection serviceCollection)
        {
            new EntityFrameworkServicesBuilder(serviceCollection)
                .TryAdd<IConventionSetBuilder, ProxiesConventionSetBuilder>()
                .TryAddProviderSpecificServices(
                    b => b.TryAddSingleton<IProxyFactory, ProxyFactory>());

            return serviceCollection;
        }
    }
}
