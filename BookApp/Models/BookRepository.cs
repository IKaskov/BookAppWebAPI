using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookApp.Models
{
    public class BookRepository : IDisposable, IBookRepository
    {
        private BookContext db = new BookContext();

        public IEnumerable<Book> GetAll()
        {
            return db.Books;
        }

        public Book GetBook(int id)
        {
            return db.Books.FirstOrDefault(p => p.Id == id);
        }

        public void Create(Book book)
        {
            db.Books.Add(book);
        }

        public void Update(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Book book = GetBook(id);
            if (book != null)
                db.Books.Remove(book);
        }


        public void Save()
        {
            db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}