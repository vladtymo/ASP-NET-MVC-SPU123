using Data.Entities;

namespace SPU123_Shop_MVC.Interfaces
{
    public interface ICartService
    {
        void Add(int id);
        void Remove(int id);
        bool IsInCart(int id);
        int GetCount();
        IEnumerable<Product> GetAll();
    }
}
