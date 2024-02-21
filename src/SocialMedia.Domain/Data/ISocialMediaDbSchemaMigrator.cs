using System.Threading.Tasks;

namespace SocialMedia.Data;

public interface ISocialMediaDbSchemaMigrator
{
    Task MigrateAsync();
}
