using firstAPI.Data;
using firstAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace firstAPI.Controllers
{
    //decoradores permite definir como llamaremos el endpoint
    [ApiController]
    [Route("api/[controller]")] // localhost:port/api/books
    public class BooksController: Controller 
    {
        private readonly IBookRepository _repository;
        public BooksController(IBookRepository repository) 
        {
            _repository = repository;
        }    
        //GET: api/books
        //ActionResult encapsula la respuesta que le vamos a dar al usuario
        //IEnumerable porque vamos a devolver una coleccion de libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return Ok(await _repository.GetAll());
        }
        //GET: api/books/2
        //decorador con parametro ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _repository.GetDetails(id);
            if(book == null)
                return NotFound();
            return book;
        }

        //POST: api/books
        //para crear un libro
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
        
            await _repository.Insert(book);

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        //PUT: api/books/2
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
                return BadRequest();

            var bookInDb = await _repository.GetDetails(id);

            if (bookInDb == null)
                return NotFound();


            await _repository.Update(book);

            return NoContent();
        }

        //DELETE: api/books/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _repository.GetDetails(id);
            if (book == null)
                return NotFound();
            await _repository.Delete(id);
            return book;
        }

   

    }
}
