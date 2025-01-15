using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class AuthorService(ApplicationDbContext dbContext) : IAuthorService
{
	private readonly ApplicationDbContext _dbContext = dbContext;
	public async Task<Author> GetAuthor()
	{
		var maxLength = await _dbContext.Books
			.MaxAsync(x => x.Title.Length);

		var bookWithMaxLength = await _dbContext.Books
			.Where(x => x.Title.Length == maxLength)
			.OrderBy(x => x.AuthorId)
			.FirstAsync();

		var author = await _dbContext.Authors
			.FirstAsync(x => x.Id == bookWithMaxLength.AuthorId);

		return author;
	}

	public async Task<List<Author>> GetAuthors()
	{
		DateTime date = new (2015, 12, 31);
		var authors = await _dbContext.Authors
			.Where(a => a.Books
				.Count(b => b.PublishDate > date) % 2 == 0)
			.ToListAsync();

		return authors;
	}
}
