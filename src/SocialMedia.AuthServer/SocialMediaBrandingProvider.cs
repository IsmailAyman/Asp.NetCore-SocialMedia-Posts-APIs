using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace SocialMedia;

[Dependency(ReplaceServices = true)]
public class SocialMediaBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "SocialMedia";
}
