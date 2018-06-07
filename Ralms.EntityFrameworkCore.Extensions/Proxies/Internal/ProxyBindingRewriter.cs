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
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ralms.EntityFrameworkCore.Extensions.Proxies.Internal
{
    public class ProxyBindingRewriter : IModelBuiltConvention
    {
        private static readonly MethodInfo _createLazyLoadingProxyMethod
            = typeof(IProxyFactory).GetTypeInfo().GetDeclaredMethod(nameof(IProxyFactory.CreateLazyLoadingProxy));

        private static readonly PropertyInfo _lazyLoaderProperty
            = typeof(IProxyLazyLoader).GetProperty(nameof(IProxyLazyLoader.LazyLoader));

        private readonly ConstructorBindingConvention _directBindingConvention;
        private readonly IProxyFactory _proxyFactory;
        private readonly ProxiesOptionsExtension _options;

        public ProxyBindingRewriter(
            IProxyFactory proxyFactory,
            IConstructorBindingFactory bindingFactory,
            ProxiesOptionsExtension options)
        {
            _directBindingConvention = new ConstructorBindingConvention(bindingFactory);
            _proxyFactory = proxyFactory;
            _options = options;
        }

        public virtual InternalModelBuilder Apply(InternalModelBuilder modelBuilder)
        {
            if (_options?.UseLazyLoadingProxies == true)
            {
                foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
                {
                    if (entityType.ClrType != null
                        && !entityType.ClrType.IsAbstract
                        && entityType.GetNavigations().Any(p => p.PropertyInfo.GetMethod.IsVirtual))
                    {
                        if (entityType.ClrType.IsSealed)
                        {
                            throw new InvalidOperationException($"Entity type '{entityType.DisplayName()}' is sealed. UseLazyLoadingProxies requires all entity types to be public, unsealed, have virtual navigation properties, and have a public or protected constructor.");
                        }

                        var proxyType = _proxyFactory.CreateLazyLoadingProxyType(entityType);

                        var serviceProperty = entityType.GetServiceProperties().FirstOrDefault(e => e.ClrType == typeof(ILazyLoader));
                        if (serviceProperty == null)
                        {
                            serviceProperty = entityType.AddServiceProperty(_lazyLoaderProperty, ConfigurationSource.Convention);
                            serviceProperty.SetParameterBinding(
                                (ServiceParameterBinding)new LazyLoaderParameterBindingFactory().Bind(
                                    entityType,
                                    typeof(ILazyLoader),
                                    nameof(IProxyLazyLoader.LazyLoader)));
                        }

                        var binding = (ConstructorBinding)entityType[CoreAnnotationNames.ConstructorBinding];
                        if (binding == null)
                        {
                            _directBindingConvention.Apply(modelBuilder);
                        }

                        binding = (ConstructorBinding)entityType[CoreAnnotationNames.ConstructorBinding];

                        entityType[CoreAnnotationNames.ConstructorBinding]
                            = new FactoryMethodConstructorBinding(
                                _proxyFactory,
                                _createLazyLoadingProxyMethod,
                                new List<ParameterBinding>
                                {
                                    new EntityTypeParameterBinding(),
                                    new DefaultServiceParameterBinding(typeof(ILazyLoader), typeof(ILazyLoader), serviceProperty),
                                    new ObjectArrayParameterBinding(binding.ParameterBindings)
                                },
                                proxyType);

                        foreach (var navigation in entityType.GetNavigations())
                        {
                            if (navigation.PropertyInfo == null)
                            {
                                throw new InvalidOperationException(
                                    $"Navigation property '{navigation.Name}' on entity type '{entityType.DisplayName()}' is mapped without a CLR property. UseLazyLoadingProxies requires all entity types to be public, unsealed, have virtual navigation properties, and have a public or protected constructor.");
                            }

                            if (navigation.PropertyInfo.GetMethod.IsVirtual)
                            {
                                navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
                            }
                        }
                    }
                }
            }

            return modelBuilder;
        }
    }
}
