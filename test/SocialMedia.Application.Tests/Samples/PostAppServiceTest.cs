using Bdaya.SocialTraining.V1;
using Google.Api;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using SocialMedia.SocialMedia;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Xunit;

namespace SocialMedia.Samples
{
    public abstract class PostAppServiceTest<TStartupModule> : SocialMediaApplicationTestBase<TStartupModule>
      where TStartupModule : IAbpModule
    {
        private IPostService AppService { get; }

        private readonly IIdentityUserAppService _userAppService;
        public PostAppServiceTest()
        {
            AppService = GetRequiredService<IPostService>();
            _userAppService = GetRequiredService<IIdentityUserAppService>();
        }

        [Fact]

        public async Task EnsureCreatePostAndGetPostReturnSameResult()
        {
            var allUsers = await _userAppService.GetListAsync(new());
            var targetUser = allUsers.Items[0];
            using var userDisposable = LoginUser(targetUser.Id);

            var createPostResponse = await AppService.CreatePost(new CreatePostRequest() { });
            var theCreatedpost = createPostResponse.Result;

            theCreatedpost.Content.ShouldBe("Content");
            theCreatedpost.User.Name.ShouldBe("admin");
            theCreatedpost.Images.ShouldContain(I => I.Name == "Image1");
            theCreatedpost.Images.Count.ShouldBe(2);
            theCreatedpost.User.ShouldNotBeNull();
            theCreatedpost.User.Id.ShouldNotBeNull();
            theCreatedpost.User.Name.ShouldNotBeNull();


            var getPost = await AppService.GetPost(new()
            {
                Id = theCreatedpost.Id
            });

            var theReturnedPost = getPost.Result;
            theReturnedPost.Id.ShouldBe(theCreatedpost.Id);
            theReturnedPost.Content.ShouldBe(theCreatedpost.Content);
          

        }

        [Fact]
        public async Task EnsureUpdatePostReturnPost_WithTheUpdatedValues()
        {
            var allUsers = await _userAppService.GetListAsync(new());
            var targetUser = allUsers.Items[0];
            using var userDisposable = LoginUser(targetUser.Id);

            var createPostResponse = await AppService.CreatePost(new CreatePostRequest() { });
            var theCreatedpost = createPostResponse.Result;

            theCreatedpost.Content.ShouldBe("Content");
            theCreatedpost.User.Name.ShouldBe("admin");
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
            var allUsers = await _userAppService.GetListAsync(new());
            var targetUser = allUsers.Items[0];
            using var userDisposable = LoginUser(targetUser.Id);

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
            var allUsers = await _userAppService.GetListAsync(new());
            var targetUser = allUsers.Items[0];
            using var userDisposable = LoginUser(targetUser.Id);

            List<Post> returnedPosts = new List<Post>();
            List<string> userIds = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                var response = await AppService.CreatePost(new CreatePostRequest() { });
                returnedPosts.Add(response.Result);
                userIds.Add(response.Result.User.Id);

                response.Result.User.ShouldNotBeNull();
                response.Result.User.Id.ShouldBe(targetUser.Id.ToString());
                response.Result.User.Name.ShouldBe(targetUser.Name);
            }

            returnedPosts.Count.ShouldBe(10);


            var postsListResponse = await AppService.ListPosts(new ListPostsRequest()
            {
                Pagination = new InfiniteScrollPaginationInfo { PageSize = 5, PageToken = "1" },
                Filter = new ListPostsFilter { UserIds = { userIds } },
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