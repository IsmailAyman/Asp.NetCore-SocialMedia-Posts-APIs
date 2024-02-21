using Volo.Abp.Modularity;

namespace SocialMedia;

[DependsOn(
    typeof(SocialMediaDomainModule),
    typeof(SocialMediaTestBaseModule)
)]
public class SocialMediaDomainTestModule : AbpModule
{

}
