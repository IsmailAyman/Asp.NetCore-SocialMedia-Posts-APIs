using SocialMedia.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace SocialMedia.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SocialMediaController : AbpControllerBase
{
    protected SocialMediaController()
    {
        LocalizationResource = typeof(SocialMediaResource);
    }
}
