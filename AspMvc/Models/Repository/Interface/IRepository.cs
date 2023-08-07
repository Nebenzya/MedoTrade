using System.Collections.Generic;

namespace AspMvc.Models.Repository.Interface
{
    public interface IBookRepository
    {
        void Create(Book book);
        void Update(Book book);
        IEnumerable<Book> GetAll();
        Book GetById(int id);
    }
}
