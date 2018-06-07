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
using System.Reflection;
using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ralms.EntityFrameworkCore.Extensions.Proxies.Internal
{
    public class LazyLoadingInterceptor : IInterceptor
    {
        private static readonly PropertyInfo _lazyLoaderProperty
            = typeof(IProxyLazyLoader).GetProperty(nameof(IProxyLazyLoader.LazyLoader));

        private static readonly MethodInfo _lazyLoaderGetter = _lazyLoaderProperty.GetMethod;
        private static readonly MethodInfo _lazyLoaderSetter = _lazyLoaderProperty.SetMethod;

        private readonly IEntityType _entityType;
        private ILazyLoader _loader;

        public LazyLoadingInterceptor(
            IEntityType entityType,
            ILazyLoader loader)
        {
            _entityType = entityType;
            _loader = loader;
        }

        public virtual void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;

            if (_lazyLoaderGetter.Equals(invocation.Method))
            {
                invocation.ReturnValue = _loader;
            }
            else if (_lazyLoaderSetter.Equals(invocation.Method))
            {
                _loader = (ILazyLoader)invocation.Arguments[0];
            }
            else
            {
                if (_loader != null
                    && methodName.StartsWith("get_", StringComparison.Ordinal))
                {
                    var navigationName = methodName.Substring(4);
                    var navigation = _entityType.FindNavigation(navigationName);

                    if (navigation != null)
                    {
                        _loader.Load(invocation.Proxy, navigationName);
                    }
                }

                invocation.Proceed();
            }
        }
    }
}
