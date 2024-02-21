using Volo.Abp.Modularity;

namespace SocialMedia;

/* Inherit from this class for your domain layer tests. */
public abstract class SocialMediaDomainTestBase<TStartupModule> : SocialMediaTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
