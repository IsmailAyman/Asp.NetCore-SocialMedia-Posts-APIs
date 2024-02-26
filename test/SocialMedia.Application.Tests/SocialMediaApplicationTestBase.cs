using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Volo.Abp.Modularity;
using Volo.Abp.Security.Claims;

namespace SocialMedia;

public abstract class SocialMediaApplicationTestBase<TStartupModule> : SocialMediaTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private ICurrentPrincipalAccessor _currentPrincipalAccessor = null!;

    protected override IServiceProvider CreateServiceProvider(IServiceCollection services)
    {
        var res = base.CreateServiceProvider(services);
        _currentPrincipalAccessor = res.GetRequiredService<ICurrentPrincipalAccessor>();
        return res;
    }

    protected IDisposable LoginUser(
        Guid userId,
        string? name = null,
        string? surName = null,
        string? phoneNumber = null,
        bool phoneNumberVerified = false,
        string? email = null,
        bool emailVerified = false,
        string? userName = null,
        Claim[]? claims= null,
        string[]? roles = null,
        Guid? tenantId = default)
    {
        var list = new List<Claim>
        {
            new Claim(AbpClaimTypes.UserId,userId.ToString()),
        };

        void AddIfNotNull(string key,string? value)
        {
            if (value == null)       
                return;           
           list.Add(new Claim(key, value));           
        }
        AddIfNotNull(AbpClaimTypes.Name,name);
        AddIfNotNull(AbpClaimTypes.SurName,surName);
        AddIfNotNull(AbpClaimTypes.UserName,userName);
        AddIfNotNull(AbpClaimTypes.PhoneNumber,phoneNumber);
        AddIfNotNull(AbpClaimTypes.PhoneNumberVerified,phoneNumberVerified.ToString());
        AddIfNotNull(AbpClaimTypes.Email,email);
        AddIfNotNull(AbpClaimTypes.EmailVerified,emailVerified.ToString());
        AddIfNotNull(AbpClaimTypes.TenantId,tenantId?.ToString());
        if(roles != null)
        {
            foreach(var r in roles)
            {
                AddIfNotNull(AbpClaimTypes.Role, r);
            }
        }
        if(claims != null)
        {
            list.AddRange(claims);
        }
        return LoginUser(list);
    }

    protected IDisposable LoginUser(IEnumerable<Claim> P)
    {
        return _currentPrincipalAccessor.Change(new ClaimsPrincipal(new ClaimsIdentity(P, "Basic")));
    }

    protected IDisposable LoginUser(ClaimsPrincipal p)
    {
        return _currentPrincipalAccessor.Change(p);
    }

    public static List<Claim> GetAdminClaims()
    {
        return new List<Claim>
        {
            new Claim(AbpClaimTypes.UserId, "2e701e62-0953-4dd3-910b-dc6cc93ccb0d"),
            new Claim(AbpClaimTypes.UserName, "admin"),
            new Claim(AbpClaimTypes.Email, "admin@abp.io"),
            new Claim(AbpClaimTypes.Role, "admin"),
        };
    }

}
