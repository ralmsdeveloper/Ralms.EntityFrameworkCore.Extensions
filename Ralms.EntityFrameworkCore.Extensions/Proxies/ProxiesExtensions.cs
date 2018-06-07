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

using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Ralms.EntityFrameworkCore.Extensions.Proxies.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore
{
    public static class RalmsProxiesExtensions
    {
        public static DbContextOptionsBuilder UseLazyLoadingProxies(
            this DbContextOptionsBuilder optionsBuilder,
            bool useLazyLoadingProxies = true)
        {
            var extension = optionsBuilder.Options.FindExtension<ProxiesOptionsExtension>()
                            ?? new ProxiesOptionsExtension();

            extension = extension.WithLazyLoading(useLazyLoadingProxies);

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            return optionsBuilder;
        }

        public static DbContextOptionsBuilder<TContext> UseLazyLoadingProxies<TContext>(
            this DbContextOptionsBuilder<TContext> optionsBuilder,
            bool useLazyLoadingProxies = true)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseLazyLoadingProxies((DbContextOptionsBuilder)optionsBuilder, useLazyLoadingProxies);

        public static object CreateProxy(
            this DbContext context,
            Type entityType,
            params object[] constructorArguments)
        {
            return context.GetInfrastructure().CreateProxy(entityType, constructorArguments);
        }

        public static TEntity CreateProxy<TEntity>(
            this DbContext context,
            params object[] constructorArguments)
            => (TEntity)context.CreateProxy(typeof(TEntity), constructorArguments);

        public static TEntity CreateProxy<TEntity>(
            this DbSet<TEntity> set,
            params object[] constructorArguments)
            where TEntity : class
        {
            return (TEntity)set.GetInfrastructure().CreateProxy(typeof(TEntity), constructorArguments);
        }

        private static object CreateProxy(
            this IServiceProvider serviceProvider,
            Type entityType,
            params object[] constructorArguments)
        {
            var options = serviceProvider.GetService<IDbContextOptions>().FindExtension<ProxiesOptionsExtension>();

            if (options?.UseLazyLoadingProxies != true)
            {
                throw new InvalidOperationException($"Unable to create proxy for '{entityType.ShortDisplayName()}' because proxies are not enabled.Call 'DbContextOptionsBuilder.UseLazyLoadingProxies' to enable lazy-loading proxies.");
            }

            return serviceProvider.GetService<IProxyFactory>().Create(
                serviceProvider.GetService<ICurrentDbContext>().Context,
                entityType,
                constructorArguments);
        }
    }
}
