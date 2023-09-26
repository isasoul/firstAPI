using firstAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace firstAPI.Data
{
    public class BooksDb : DbContext
    {
        //dbcontextoptions permite definir cosas como donde esta la base de datos  que elementos tiene.
        public BooksDb(DbContextOptions<BooksDb>options) : base(options) 
        {
        }
        //Books es la propiedad que vamos a utilizar para agregar listar y eliminar elementos de nuestra BD

        public DbSet<Book> Books => Set<Book>();
    }
}
