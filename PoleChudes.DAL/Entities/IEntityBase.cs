using EntityFrameworkCore.CommonTools;

namespace PoleChudes.DAL.Entities
{
    public interface IEntityBase<TKey> : IEntityBase
    {
        TKey Id { get; set; }
    }

    public interface IEntityBase : IModificationTrackable, ICreationTrackable
    {
    }
}
