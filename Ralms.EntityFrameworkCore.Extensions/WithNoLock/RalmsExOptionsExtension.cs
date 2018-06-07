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

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Ralms.EntityFrameworkCore.Extensions
{
    class RalmsExOptionsExtension : IDbContextOptionsExtension
    {
        public string LogFragment => string.Empty;

        public long GetServiceProviderHashCode()
            => base.GetHashCode()  * 3;

        public void Validate(IDbContextOptions options)
        {
        }

        public bool ApplyServices(IServiceCollection services)
        {
            services.AddRalmsExtensions(); 
            return false;
        }
    }
}
