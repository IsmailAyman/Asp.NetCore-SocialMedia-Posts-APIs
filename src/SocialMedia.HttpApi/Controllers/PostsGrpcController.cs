using Bdaya.SocialTraining.V1;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bdaya.SocialTraining.V1.PostService;

namespace SocialMedia.Controllers
{
    public class PostsGrpcController : PostServiceBase
    {
        protected IPostService PostService { get; }
        public PostsGrpcController(IPostService postService)
        {
            PostService = postService;
        }
        public override async Task<ListPostsResponse> ListPosts(ListPostsRequest request, ServerCallContext context)
        {
            return await PostService.ListPosts(request);
        }

        public override async Task<GetPostResponse> GetPost(GetPostRequest request, ServerCallContext context)
        {
            return await PostService.GetPost(request);
        }

        public override async Task<CreatePostResponse> CreatePost(CreatePostRequest request, ServerCallContext context)
        {
            return await PostService.CreatePost(request);
        }

        public override async Task<UpdatePostResponse> UpdatePost(UpdatePostRequest request, ServerCallContext context)
        {
            return await PostService.UpdatePost(request);
        }

        public override async Task<DeletePostResponse> DeletePost(DeletePostRequest request, ServerCallContext context)
        {
            return await PostService.DeletePost(request);
        }
    }
}
