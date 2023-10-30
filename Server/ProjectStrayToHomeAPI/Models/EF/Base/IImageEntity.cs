namespace ProjectStrayToHomeAPI.Models.EF.Base
{
    public interface IImageEntity<out TKey> : IEntity<TKey> 
    {
        byte[] Image { get; set; }
    }
}
