using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace SocialMedia.Data;

/* This is used if database provider does't define
 * ISocialMediaDbSchemaMigrator implementation.
 */
public class NullSocialMediaDbSchemaMigrator : ISocialMediaDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
