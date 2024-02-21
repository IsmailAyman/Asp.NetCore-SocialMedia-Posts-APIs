using Bdaya.SocialTraining.V1;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using SocialMedia.SocialMedia;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Xunit;

namespace SocialMedia.Samples
{
    public abstract class PostAppServiceTest<TStartupModule> : SocialMediaApplicationTestBase<TStartupModule>
      where TStartupModule : IAbpModule
    {
        private IPostService AppService { get; }
        public PostAppServiceTest()
        {
            AppService = GetRequiredService<IPostService>();
        }

        [Fact]

        public async Task EnsureCreatePostAndGetPostReturnSameResult()
        {

            var createPostResponse = await AppService.CreatePost(new CreatePostRequest() { });

            var theCreatedpost = createPostResponse.Result;

            theCreatedpost.Content.ShouldBe("Content");
            theCreatedpost.User.Name.ShouldBe("Ismail");
            theCreatedpost.Images.ShouldContain(I => I.Name == "Image1");
            theCreatedpost.Images.Count.ShouldBe(2);

            var getPost = await AppService.GetPost(new()
            {
                Id = theCreatedpost.Id
            });

            var theReturnedPost = getPost.Result;
            theReturnedPost.Id.ShouldBe(theCreatedpost.Id);
            theReturnedPost.Content.ShouldBe(theCreatedpost.Content);
            theReturnedPost.User.ImageUrl.ShouldBe("Image");

        }

        [Fact]
        public async Task EnsureUpdatePostReturnPost_WithTheUpdatedValues()
        {

            var createPostResponse = await AppService.CreatePost(new CreatePostRequest() { });
            var theCreatedpost = createPostResponse.Result;

            theCreatedpost.Content.ShouldBe("Content");
            theCreatedpost.User.Name.ShouldBe("Ismail");
            theCreatedpost.Images.ShouldContain(I => I.Name == "Image1");
            theCreatedpost.Images.Count.ShouldBe(2);

            var updatePost = await AppService.UpdatePost(new UpdatePostRequest()
            {
                Id = theCreatedpost.Id
            });
            var theUpdatedPost = updatePost.Result;
            theUpdatedPost.Content.ShouldBe("Updated Content");
        }

        [Fact]
        public async Task EnsureDeletePostWillDeleteIt()
        {

            var response = await AppService.CreatePost(new CreatePostRequest() { });
            var theCreatedpost = response.Result;
            await AppService.DeletePost(new DeletePostRequest()
            {
                Id = theCreatedpost.Id
            });

            var test = "";
            try
            {
                var getPost = await AppService.GetPost(new()
                {
                    Id = theCreatedpost.Id
                });
            }
            catch (Exception)
            {
                test = "test";
            }

            test.ShouldBe("test");

        }


        [Fact]
        public async Task EnsureListPostsReturnsListOfPaginated_SortedAndFilteredPosts()
        {

            List<Post> returnedPosts = new List<Post>();
            List<string> userIds = new List<string>();
            for (int i = 0; i < 10; i++)
            {
              var response = await AppService.CreatePost(new CreatePostRequest() { });
                returnedPosts.Add(response.Result);
                userIds.Add(response.Result.User.Id);
            }

            returnedPosts.Count.ShouldBe(10);

            var postsListResponse = await AppService.ListPosts(new ListPostsRequest()
            {
                Pagination = new InfiniteScrollPaginationInfo { PageSize = 5,PageToken = 1.ToString()},
                Filter = new ListPostsFilter { UserIds ={ userIds }},
                Sort = new ListPostsSorting
                {
                    CreationTime = Bdaya.SocialTraining.V1.SortDirection.Ascending
                                            ,
                    LatestCommentTime = Bdaya.SocialTraining.V1.SortDirection.Unspecified
                }

            });

            postsListResponse.Posts.Count.ShouldBe(5);


        }
    }
}