using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace SocialMedia.SocialMedia
{
    public class AppImageSM: CreationAuditedEntity<Guid>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected AppImageSM()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            
        }
        public AppImageSM(Guid id, float width, float height, string name, string url) : base(id)
        {
            Width = width;
            Height = height;
            Name = name;
            Url = url;
        }
        public float Width { get; set; }

        public float Height { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
    }
}
