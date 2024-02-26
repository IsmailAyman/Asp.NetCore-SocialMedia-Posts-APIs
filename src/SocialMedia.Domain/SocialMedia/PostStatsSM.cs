using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace SocialMedia.SocialMedia
{
    public class PostStatsSM : CreationAuditedEntity<Guid>
    {
        protected PostStatsSM() { }
        
        public PostStatsSM(Guid id, ulong likes = 0, ulong comments = 0, ulong shares = 0) : base(id)
        {
            Likes = likes;
            Comments = comments;
            Shares = shares;
        }
        public ulong Likes { get; set; }

        public ulong Comments { get; set; } 

        public ulong Shares { get; set; } 

    }
}
