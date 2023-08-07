using AspMvc.Models;
using AspMvc.Models.Repository;
using AspMvc.Models.Repository.Interface;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AspMvc.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BooksController()
        {
            // нужен DI
            _bookRepository = new BookRepository();
        }

        public ActionResult AllBooks()
        {
            IEnumerable<Book> books = _bookRepository.GetAll();
            return View(books);
        }

        [HttpPost]
        public PartialViewResult AddBook(string title, string description)
        {
            var book = new Book { Title = title, Description = description };
            _bookRepository.Create(book);
            return PartialView(book);
        }


        [HttpPut]
        public ContentResult Edit(Book book)
        {
            _bookRepository.Update(book);
            return Content("success");
        }
    }
}