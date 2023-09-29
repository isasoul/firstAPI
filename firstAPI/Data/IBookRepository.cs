using firstAPI.Models;

namespace firstAPI.Data
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task <Book> GetDetails(int id);
        Task Insert (Book book);
        Task Update (Book book);
        Task Delete (int id);
    }
}
