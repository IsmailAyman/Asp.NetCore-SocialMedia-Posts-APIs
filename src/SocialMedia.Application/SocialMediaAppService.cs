using System;
using System.Collections.Generic;
using System.Text;
using SocialMedia.Localization;
using Volo.Abp.Application.Services;

namespace SocialMedia;

/* Inherit your application services from this class.
 */
public abstract class SocialMediaAppService : ApplicationService
{
    protected SocialMediaAppService()
    {
        LocalizationResource = typeof(SocialMediaResource);
    }
}
