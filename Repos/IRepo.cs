using GymManagmentSystem.Dtos;

namespace GymManagmentSystem.Repos
{
    public interface IRepo<T,TKey>
    {
        List<T> getAll();
        T getById(TKey id);

        Task<bool> add(T E,string pass);

        bool Delete(TKey id);

        







    }
}
