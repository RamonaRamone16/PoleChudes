using System;

namespace PoleChudes.DAL.Entities
{
    public class BaseEntity<TKey> : IEntityBase<TKey>
    {
        public TKey Id { get; set; }
        public DateTime? UpdatedUtc { get; set; }
        public DateTime CreatedUtc { get; set; }
    }
}
