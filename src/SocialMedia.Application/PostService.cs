using AutoMapper;
using Bdaya.SocialTraining.V1;
using Google.Protobuf.Collections;
using SocialMedia.SocialMedia;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IO;
using Volo.Abp.ObjectMapping;
using Volo.Abp.OpenIddict;
using static Google.Rpc.Context.AttributeContext.Types;

namespace SocialMedia
{
    [ExposeServices(typeof(IPostService))]
    public class PostService : SocialMediaAppService, IPostService , ITransientDependency
    {
        public IRepository<PostSM, Guid> Repository { get; set; }
        public PostService(IRepository<PostSM, Guid> repository)
        {
            Repository = repository;
        }

        public async Task<ListPostsResponse> ListPosts(ListPostsRequest postsRequest)
        {   
            var token = postsRequest.Pagination.PageToken;  
            int skip = 0;
            IEnumerable<PostSM> postList;
            if (token is not null) 
            {
                skip = int.Parse(token);
            }    
            postList = await Repository.GetPagedListAsync(skipCount: skip, maxResultCount: postsRequest.Pagination.PageSize, sorting: nameof(PostSM.CreationTime).ToString()); 
            postList = postList.Where(p => postsRequest.Filter.UserIds.Contains(p.User.Id.ToString()));
            var mappedPostList = ObjectMapper.Map<IEnumerable<PostSM>, IEnumerable<Post>>(postList);
           
            return new ListPostsResponse()
            {
                Posts = { mappedPostList } ,
                NextPageToken = (skip + postsRequest.Pagination.PageSize).ToString()
            };
        }

        public async Task<GetPostResponse> GetPost(GetPostRequest postRequest)
        {
            var post = await Repository.GetAsync(Guid.Parse(postRequest.Id));
           
            return new GetPostResponse()
            {
                Result = ObjectMapper.Map<PostSM, Post>(post)
            };
        }

        public async Task<CreatePostResponse> CreatePost(CreatePostRequest post)
        {
            var userInfo = new UserInfoSM(GuidGenerator.Create(), "Ismail", "Image");
            List<AppImageSM> imageList = new List<AppImageSM>() {new AppImageSM(GuidGenerator.Create(), 3.3f, 5.5f, "Image1", "FirstUrl")
                                                                ,new AppImageSM(GuidGenerator.Create(), 2.5f, 5f, "Image2", "SecondUrl")};
            var createdPost = new PostSM(GuidGenerator.Create(), userInfo, "Content", imageList,null,null);
            await Repository.InsertAsync(createdPost);

            return new CreatePostResponse()
            {
                Result = ObjectMapper.Map<PostSM, Post>(createdPost)
            };
        }

        public async Task<UpdatePostResponse> UpdatePost(UpdatePostRequest updatePostId)
        {
            var post = await Repository.GetAsync(Guid.Parse(updatePostId.Id));
                post.Content = "Updated Content";
                await Repository.UpdateAsync(post);

                return new UpdatePostResponse()
                {
                    Result = ObjectMapper.Map<PostSM, Post>(post)
                };
        }

        public async Task<DeletePostResponse> DeletePost(DeletePostRequest postId)
        {
            var post = await Repository.GetAsync(Guid.Parse(postId.Id));
             await Repository.DeleteAsync(post.Id);        
            
            return new DeletePostResponse();
           
        }
    }
}
