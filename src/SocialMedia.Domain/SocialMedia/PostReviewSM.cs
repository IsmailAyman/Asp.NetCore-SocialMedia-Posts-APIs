using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace SocialMedia.SocialMedia
{
    public class PostReviewSM: CreationAuditedEntity<Guid>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected PostReviewSM() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public PostReviewSM(Guid id, string review_details, string reviewed_by) : base(id)
        {
            ReviewDetails = review_details;
            ReviewedBy = reviewed_by;
            ReviewedAt = DateTime.Now;
        }
        public string ReviewDetails { get; set; }

        public string ReviewedBy { get; set; }

        public DateTime ReviewedAt { get; set; }

        public int Status { get; set; }
    }
}
