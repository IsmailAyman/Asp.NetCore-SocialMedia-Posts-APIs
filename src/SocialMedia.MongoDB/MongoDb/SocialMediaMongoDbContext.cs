using MongoDB.Driver;
using SocialMedia.SocialMedia;
using Volo.Abp.Data;
using Volo.Abp.Identity.MongoDB;
using Volo.Abp.MongoDB;

namespace SocialMedia.MongoDB;

[ConnectionStringName("Default")]
public class SocialMediaMongoDbContext : AbpMongoDbContext
{ 
     public IMongoCollection<PostSM> Posts => Collection<PostSM>();


    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.Entity<PostSM>(b =>
        {
            b.CollectionName = SocialMediaConsts.DbTablePrefix + "Posts";
            b.BsonMap.ConfigureAbpConventions();
        });
      


    }
}
