using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace SocialMedia.MongoDB;

[DependsOn(
    typeof(SocialMediaApplicationTestModule),
    typeof(SocialMediaMongoDbModule)
)]
public class SocialMediaMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = SocialMediaMongoDbFixture.GetRandomConnectionString();
        });
    }
}
