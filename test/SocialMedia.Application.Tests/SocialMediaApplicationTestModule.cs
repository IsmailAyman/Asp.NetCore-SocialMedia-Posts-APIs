using Volo.Abp.Modularity;

namespace SocialMedia;

[DependsOn(
    typeof(SocialMediaApplicationModule),
    typeof(SocialMediaDomainTestModule)
)]
public class SocialMediaApplicationTestModule : AbpModule
{

}
