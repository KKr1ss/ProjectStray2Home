namespace ProjectStrayToHomeAPI.Models.EF.Base
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }
}
