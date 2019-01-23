using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using BookApp.Models;

namespace BookApp.Controllers
{
    public class BooksController : ApiController
    {
        //private BookContext repository = new BookContext();

        private IBookRepository repository;

        public BooksController()
        {
            repository = new BookRepository();
        }

        public BooksController(IBookRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Books
        public IEnumerable<Book> GetBooks()
        {
            return repository.GetAll();
        }

        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = repository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            repository.Update(book);

            try
            {
                repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.NoContent);

            return Content(HttpStatusCode.Accepted, book);
        }

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Create(book);
            repository.Save();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = repository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            repository.Delete(id);
            repository.Save();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return repository.GetAll().Count(e => e.Id == id) > 0;
        }
    }
}