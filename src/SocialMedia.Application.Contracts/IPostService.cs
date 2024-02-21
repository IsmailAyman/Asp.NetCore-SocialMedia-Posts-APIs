using Bdaya.SocialTraining.V1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SocialMedia
{
    public interface IPostService : IApplicationService
    {

        Task<ListPostsResponse> ListPosts(ListPostsRequest postsRequest);

        Task<GetPostResponse> GetPost(GetPostRequest postRequest);

        Task<CreatePostResponse> CreatePost(CreatePostRequest post);

        Task<UpdatePostResponse> UpdatePost(UpdatePostRequest updatePostId);

        Task<DeletePostResponse> DeletePost(DeletePostRequest postId);



    }
}
