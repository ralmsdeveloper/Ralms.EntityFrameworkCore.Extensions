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
using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ralms.EntityFrameworkCore.Extensions.Proxies.Internal
{
    public class ProxyFactory : IProxyFactory
    {
        private readonly ProxyGenerator _generator = new ProxyGenerator();
        private static readonly Type[] _additionalInterfacesToProxy = { typeof(IProxyLazyLoader) };

        public virtual object Create(
            DbContext context,
            Type entityClrType,
            params object[] constructorArguments)
        {
            var entityType = context.Model.FindRuntimeEntityType(entityClrType);
            if (entityType == null)
            {
                throw new InvalidOperationException(CoreStrings.EntityTypeNotFound(entityClrType.ShortDisplayName()));
            }

            return CreateLazyLoadingProxy(entityType, context.GetService<ILazyLoader>(), constructorArguments);
        }

        public virtual Type CreateLazyLoadingProxyType(IEntityType entityType)
            => _generator.ProxyBuilder.CreateClassProxyType(
                entityType.ClrType,
                _additionalInterfacesToProxy,
                ProxyGenerationOptions.Default);

        public virtual object CreateLazyLoadingProxy(
            IEntityType entityType,
            ILazyLoader loader,
            object[] constructorArguments)
            => _generator.CreateClassProxy(
                entityType.ClrType,
                _additionalInterfacesToProxy,
                ProxyGenerationOptions.Default,
                constructorArguments,
                new LazyLoadingInterceptor(entityType, loader));
    }
}
