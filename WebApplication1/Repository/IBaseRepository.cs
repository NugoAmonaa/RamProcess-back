namespace WebApplication1.Repository
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetList();
        Task<T> GetById(int id);
        Task Insert(T obj);
        Task Update(T obj);
        Task DeleteById(int id);
        Task SaveChanges();
    }
}
