using Volo.Abp.Settings;

namespace SocialMedia.Settings;

public class SocialMediaSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SocialMediaSettings.MySetting1));
    }
}
