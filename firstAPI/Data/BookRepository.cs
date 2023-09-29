using Dapper;
using firstAPI.Models;
using System.Data;
using System.Data.Common;

namespace firstAPI.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly IDbConnection _dbConnection;

        public BookRepository(IDbConnection dbconnection)
        {
            _dbConnection = dbconnection;
        }

        public async Task Delete(int id)
        {
            var sql = @"  DELETE FROM Books 
                            WHERE Id = @Id";
            ;

            await _dbConnection.ExecuteAsync(sql,
               new
               {
                   Id = id

               });
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var sql = @" SELECT Id
                                ,Title
                                ,Author
                                ,ISAvailable
                          FROM   Books";

            return await _dbConnection.QueryAsync<Book>(sql, new { });
        }

        public async Task<Book> GetDetails(int id)
        {
            var sql = @" SELECT Id
                                ,Title
                                ,Author
                                ,ISAvailable
                          FROM   Books
                          WHERE Id =  @Id";

            return await _dbConnection.QueryFirstOrDefaultAsync<Book>(sql, new { Id = id });
        }
    

        public async Task Insert(Book book)
        {
        var sql = @" INSERT INTO Books (Id,Title, Author, IsAvailable)
                    VALUES(@Id, @Title, @Author, @IsAvailable) ";
        ;

         await _dbConnection.ExecuteAsync(sql,
            new
            {
                book.Id,
                book.Title,
                book.Author,
                book.IsAvailable

            }); ;
    }


        public async Task Update(Book book)
        {
            var sql = @"  UPDATE Books
                    SET Title = @Title,
                        Author = @Author,
                        IsAvailable = @IsAvailable
                  WHERE Id = @Id ";
    ;

     await _dbConnection.ExecuteAsync(sql,
        new
        {
            book.Id,
            book.Title,
            book.Author,
            book.IsAvailable

        }); 
        }
    }
}
