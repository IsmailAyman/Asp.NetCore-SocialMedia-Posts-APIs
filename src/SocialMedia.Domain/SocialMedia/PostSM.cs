using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Guids;

namespace SocialMedia.SocialMedia
{
    public class PostSM : FullAuditedAggregateRoot<Guid>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected PostSM() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        public PostSM(Guid id, string content, List<AppImageSM>? images) : base(id)
        {
           
            Date = DateTime.Now;
            Content = content;        
            if (images != null) { Images = images; }
            Review = null;
            Stats = new PostStatsSM(Guid.NewGuid(),0,0, 0);
        }
      
        public DateTime Date { get; set; }

        public string Content { get; set; }
        public PostStatsSM Stats { get; set; }

        public PostReviewSM? Review { get; set; }

        public List<AppImageSM>? Images { get; set; }

    }
}
