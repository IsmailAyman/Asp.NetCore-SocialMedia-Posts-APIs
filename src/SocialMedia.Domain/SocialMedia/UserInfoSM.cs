using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace SocialMedia.SocialMedia
{
    public class UserInfoSM : CreationAuditedEntity<Guid>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected UserInfoSM() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        
        public UserInfoSM(Guid id, string name, string image_Url) : base(id)
        {
            Name = name;
            Image_Url = image_Url;
        }
        public string Name { get; set; }

        public string Image_Url { get; set; }


    }
}
