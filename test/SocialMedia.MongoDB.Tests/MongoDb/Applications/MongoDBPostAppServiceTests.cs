using SocialMedia.MongoDB;
using SocialMedia.Samples;
using Xunit;

namespace SocialMedia.MongoDb.Applications;

[Collection(SocialMediaTestConsts.CollectionDefinitionName)]
public class MongoDBPostAppServiceTests : PostAppServiceTest<SocialMediaMongoDbTestModule>
{

}
