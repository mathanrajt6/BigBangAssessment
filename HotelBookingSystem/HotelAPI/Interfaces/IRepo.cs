namespace HotelAPI.Interfaces
{
    public interface IRepo<T,K>
    {
        ICollection<T> GetAll();
        T Get(K id);
        T Add(T item);
        T Update(T item);
        T Delete(K id);
    }
}
