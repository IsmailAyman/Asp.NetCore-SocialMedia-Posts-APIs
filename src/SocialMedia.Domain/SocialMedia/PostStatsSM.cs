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
        public UInt64 Likes { get; set; }

        public UInt64 Comments { get; set; }

        public UInt64 Shares { get; set; }

    }
}
