using Volo.Abp.Modularity;

namespace SocialMedia;

public abstract class SocialMediaApplicationTestBase<TStartupModule> : SocialMediaTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
