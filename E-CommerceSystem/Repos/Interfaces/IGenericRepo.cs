namespace E_CommerceSystem.Repos.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task Create(T entity);
        void Update(T entity);
        Task Delete(T entity);
    }
}
