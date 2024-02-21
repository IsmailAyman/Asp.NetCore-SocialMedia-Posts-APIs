using SocialMedia.Samples;
using Xunit;

namespace SocialMedia.MongoDB.Domains;

[Collection(SocialMediaTestConsts.CollectionDefinitionName)]
public class MongoDBSampleDomainTests : SampleDomainTests<SocialMediaMongoDbTestModule>
{

}
