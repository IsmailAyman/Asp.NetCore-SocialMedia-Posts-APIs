using AutoMapper;
using Bdaya.SocialTraining.V1;
using Microsoft.Extensions.Configuration;
using SocialMedia.SocialMedia;
using System;
using System.Collections;
using System.Collections.Generic;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;

namespace SocialMedia;

public class SocialMediaApplicationAutoMapperProfile : Profile
{
    public SocialMediaApplicationAutoMapperProfile()
    {


        CreateMap<PostSM, Post>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .Ignore(dest => dest.Date)
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.Review, opt => opt.MapFrom(src => src.Review))
            .ForMember(dest => dest.Stats, opt => opt.MapFrom(src => src.Stats))
            .ForMemberWithItemEntry(x => x.User, nameof(IdentityUser),
            (PostSM src, IReadOnlyDictionary<Guid, IdentityUser> usersDict)
            => usersDict.GetValueOrDefaultBetter(src.CreatorId)
            );



        CreateMap<IdentityUser, UserInfo>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .Ignore(dest => dest.ImageUrl);
            

        CreateMap<PostReviewSM, PostReview>()
            .ForMember(dest => dest.ReviewedAt, opt => opt.Ignore())
            .ForMember(dest => dest.ReviewDetails, opt => opt.MapFrom(src => src.ReviewDetails))
            .ForMember(dest => dest.ReviewedBy, opt => opt.MapFrom(src => src.ReviewedBy))
            .Ignore(dest => dest.Status);


        CreateMap<AppImageSM, AppImage>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Width))
            .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .Ignore(dest => dest.TakenDateTime);

        CreateMap<PostStatsSM, PostStats>()
             .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes))
              .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
               .ForMember(dest => dest.Shares, opt => opt.MapFrom(src => src.Shares));
    }
}
