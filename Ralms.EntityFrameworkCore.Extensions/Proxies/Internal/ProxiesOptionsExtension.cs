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
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Ralms.EntityFrameworkCore.Extensions.Proxies.Internal
{
    public class ProxiesOptionsExtension : IDbContextOptionsExtension
    {
        private bool _useLazyLoadingProxies;
        private string _logFragment;

        public ProxiesOptionsExtension()
        {
        }

        protected ProxiesOptionsExtension(ProxiesOptionsExtension copyFrom)
        {
            _useLazyLoadingProxies = copyFrom._useLazyLoadingProxies;
        }

        protected virtual ProxiesOptionsExtension Clone() => new ProxiesOptionsExtension(this);

        public virtual bool UseLazyLoadingProxies => _useLazyLoadingProxies;

        public virtual ProxiesOptionsExtension WithLazyLoading(bool useLazyLoadingProxies = true)
        {
            var clone = Clone();

            clone._useLazyLoadingProxies = useLazyLoadingProxies;

            return clone;
        }

        public virtual long GetServiceProviderHashCode() => _useLazyLoadingProxies ? 541 : 0;

        public virtual void Validate(IDbContextOptions options)
        {
            if (_useLazyLoadingProxies)
            {
                var internalServiceProvider = options.FindExtension<CoreOptionsExtension>()?.InternalServiceProvider;
                if (internalServiceProvider != null)
                {
                    using (var scope = internalServiceProvider.CreateScope())
                    {
                        if (scope.ServiceProvider
                                .GetService<IEnumerable<IConventionSetBuilder>>()
                                ?.Any(s => s is ProxiesConventionSetBuilder) == false)
                        {
                            throw new InvalidOperationException("UseLazyLoadingProxies requires AddEntityFrameworkProxies to be called on the internal service provider used.");
                        }
                    }
                }
            }
        }

        public virtual bool ApplyServices(IServiceCollection services)
        {
            services.AddEntityFrameworkProxies();

            return false;
        }
        
        public virtual string LogFragment
        {
            get
            {
                if (_logFragment == null)
                {
                    _logFragment = _useLazyLoadingProxies
                        ? "using lazy-loading proxies "
                        : "";
                }

                return _logFragment;
            }
        }
    }
}
