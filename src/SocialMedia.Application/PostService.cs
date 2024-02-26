using AutoMapper;
using Bdaya.SocialTraining.V1;
using Google.Api;
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
using Volo.Abp.Identity;
using Volo.Abp.IO;
using Volo.Abp.ObjectMapping;
using Volo.Abp.OpenIddict;
using static Google.Rpc.Context.AttributeContext.Types;

namespace SocialMedia
{
    [ExposeServices(typeof(IPostService))]
    public class PostService : SocialMediaAppService, IPostService , ITransientDependency
    {
        public IRepository<PostSM, Guid> Repository { get; }
        public IIdentityUserRepository identityUserRepository { get; }
        public PostService(IRepository<PostSM, Guid> repository, IIdentityUserRepository identityUserRepository)
        {
            Repository = repository;
            this.identityUserRepository = identityUserRepository;
        }

        public async Task<ListPostsResponse> ListPosts(ListPostsRequest postsRequest)
        {   
            var token = postsRequest.Pagination.PageToken;  
            int skip = 0;
            
            if (token is not null) 
            {
                skip = int.Parse(token);
            }
           

            var filterUserIds = postsRequest.Filter.UserIds.Select(Guid.Parse).Cast<Guid?>().ToHashSet(); 
            var posts = await Repository.GetQueryableAsync();
            var filteredList = posts.Where(p => filterUserIds.Contains(p.CreatorId))                   
                                                                   .OrderBy(p => p.CreationTime);
            var postList = await Repository.AsyncExecuter.ToListAsync(filteredList.Skip(skip).Take(postsRequest.Pagination.PageSize));
          

            var userIds = postList.Select(x => x.CreatorId).Where(x=>x.HasValue).Select(x=>x!.Value).ToHashSet();  
            var users = await identityUserRepository.GetListByIdsAsync(userIds);
            var dict = users.ToDictionary(x => x.Id);           
            var mappedPostList = ObjectMapper.GetMapper().Map<IEnumerable<PostSM>, IEnumerable<Post>>(postList, opt =>
            {
                opt.Items[nameof(IdentityUser)] = dict;
            });
           
            return new ListPostsResponse()
            {
                Posts = { mappedPostList } ,
                NextPageToken = (skip + postsRequest.Pagination.PageSize).ToString()
            };
        }

        public async Task<GetPostResponse> GetPost(GetPostRequest postRequest)
        {
            var post = await Repository.GetAsync(Guid.Parse(postRequest.Id));

            var userIds = new HashSet<Guid?>() { post.CreatorId }.Where(x => x.HasValue).Select(x => x!.Value).ToHashSet();
            var users = await identityUserRepository.GetListByIdsAsync(userIds);
            var dict = users.ToDictionary(x => x.Id);

            return new GetPostResponse()
            {
                Result = ObjectMapper.GetMapper().Map<PostSM, Post>(post, opt =>
                {
                    opt.Items[nameof(IdentityUser)] = dict;
                })
            };
        }

        public async Task<CreatePostResponse> CreatePost(CreatePostRequest post)
        {
          
            List<AppImageSM> imageList = new List<AppImageSM>() {new AppImageSM(GuidGenerator.Create(), 3.3f, 5.5f, "Image1", "FirstUrl")
                                                                ,new AppImageSM(GuidGenerator.Create(), 2.5f, 5f, "Image2", "SecondUrl")};
            var createdPost = new PostSM(GuidGenerator.Create(), "Content", imageList);
            await Repository.InsertAsync(createdPost);

            var userIds = new HashSet<Guid?>() { createdPost.CreatorId}.Where(x=>x.HasValue).Select(x=>x!.Value).ToHashSet();
            var users = await identityUserRepository.GetListByIdsAsync(userIds);
            var dict = users.ToDictionary(x => x.Id);
           
            return new CreatePostResponse()
            {
                Result = ObjectMapper.GetMapper().Map<PostSM, Post>(createdPost, opt =>
                {
                    opt.Items[nameof(IdentityUser)] = dict;
                })
            };
        }

        public async Task<UpdatePostResponse> UpdatePost(UpdatePostRequest updatePostId)
        {
            var post = await Repository.GetAsync(Guid.Parse(updatePostId.Id));
                post.Content = "Updated Content";
                await Repository.UpdateAsync(post);

            var userIds = new HashSet<Guid?>() { post.CreatorId }.Where(x => x.HasValue).Select(x => x!.Value).ToHashSet();
            var users = await identityUserRepository.GetListByIdsAsync(userIds);
            var dict = users.ToDictionary(x => x.Id);

            return new UpdatePostResponse()
                {
                Result = ObjectMapper.GetMapper().Map<PostSM, Post>(post, opt =>
                {
                    opt.Items[nameof(IdentityUser)] = dict;
                })
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
