using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class BookService(ApplicationDbContext dbContext) : IBookService
{
	private readonly ApplicationDbContext _dbContext = dbContext;

	public async Task<Book> GetBook()
	{
		var book = await _dbContext.Books
			.OrderByDescending(b => b.Price * b.QuantityPublished)
			.FirstOrDefaultAsync();

		return book;
	}

	public async Task<List<Book>> GetBooks()
	{
		DateTime dateCarolusRex = new (2012, 5, 25);
		string name = "Red";

		var books = await _dbContext.Books
			.Where(b => b.PublishDate > dateCarolusRex)
			.Where(b => b.Title.Contains(name))
			.ToListAsync();

		return books;
	}
}
