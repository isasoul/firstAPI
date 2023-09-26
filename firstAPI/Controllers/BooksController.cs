using firstAPI.Data;
using firstAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace firstAPI.Controllers
{
    //decoradores permite definir como llamaremos el endpoint
    [ApiController]
    [Route("api/[controller]")] // localhost:port/api/books
    public class BooksController: Controller 
    {
        private readonly BooksDb _context;
        public BooksController(BooksDb context) 
        { 
            _context = context;
        }    
        //GET: api/books
        //ActionResult encapsula la respuesta que le vamos a dar al usuario
        //IEnumerable porque vamos a devolver una coleccion de libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }
        //GET: api/books/2
        //decorador con parametro ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book == null)
                return NotFound();
            return book;
        }

        //POST: api/books
        //para crear un libro
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
           _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        //PUT: api/books/2
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
                return BadRequest();

            var bookInDb = await _context.Books.FindAsync(id);

            if (bookInDb == null)
                return NotFound();


            bookInDb.Title = book.Title;
            bookInDb.Author = book.Author;
            bookInDb.IsAvailable = book.IsAvailable;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        //DELETE: api/books/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return book;
        }

   

    }
}
