using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplyBooks.Data;
using SimplyBooks.Models;

var builder = WebApplication.CreateBuilder(args);


// Enable OpenAPI (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow passing DateTimes without timezone data
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Connect API to PostgreSQL Database
builder.Services.AddNpgsql<SimplyBooksDbContext>(builder.Configuration["SimplyBooksDbConnectionString"]);

// Set JSON serialization options (Prevents circular JSON errors)
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

//  CORS Policy (Allow frontend at `localhost:3000`)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:3000") //  Adjusted origin for your frontend
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});

var app = builder.Build();

// ✅ Enable CORS Middleware BEFORE routing
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// === CRUD endpoints for Authors ===
app.MapGet("/authors/user/{userId}", async (int userId, SimplyBooksDbContext db) =>
  await db.Authors.Include(a => a.Books).Where(a => a.UserId == userId).ToListAsync());

app.MapGet("/authors/{id}", async (int id, SimplyBooksDbContext db) =>
  await db.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id));

app.MapPost("/authors", async (Author author, SimplyBooksDbContext db) =>
{
  db.Authors.Add(author);
  await db.SaveChangesAsync();
  return Results.Created($"/authors/{author.Id}", author);
});

app.MapPut("/authors/{id}", async (int id, Author updatedAuthor, SimplyBooksDbContext db) =>
{
  var author = await db.Authors.FindAsync(id);
  if (author is null) return Results.NotFound();

  author.FirstName = updatedAuthor.FirstName;
  author.LastName = updatedAuthor.LastName;
  author.Email = updatedAuthor.Email;
  author.Image = updatedAuthor.Image;
  author.Favorite = updatedAuthor.Favorite;

  await db.SaveChangesAsync();
  return Results.Ok(author);
});

app.MapDelete("/authors/{id}", async (int id, SimplyBooksDbContext db) =>
{
  var author = await db.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
  if (author is null) return Results.NotFound();

  db.Authors.Remove(author);
  await db.SaveChangesAsync();
  return Results.NoContent();
});

// === CRUD endpoints for Books ===
app.MapGet("/books/user/{userId}", async (int userId, SimplyBooksDbContext db) =>
  await db.Books.Include(b => b.Author).Where(b => b.UserId == userId).ToListAsync());

app.MapGet("/books/{id}", async (int id, SimplyBooksDbContext db) =>
  await db.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id));

app.MapPost("/books", async (Book book, SimplyBooksDbContext db) =>
{
  db.Books.Add(book);
  await db.SaveChangesAsync();
  return Results.Created($"/books/{book.Id}", book);
});

app.MapPut("/books/{id}", async (int id, Book updatedBook, SimplyBooksDbContext db) =>
{
  var book = await db.Books.FindAsync(id);
  if (book is null) return Results.NotFound();

  book.Title = updatedBook.Title;
  book.Description = updatedBook.Description;
  book.Image = updatedBook.Image;
  book.Price = updatedBook.Price;
  book.Sale = updatedBook.Sale;
  book.AuthorId = updatedBook.AuthorId;

  await db.SaveChangesAsync();
  return Results.Ok(book);
});

app.MapDelete("/books/{id}", async (int id, SimplyBooksDbContext db) =>
{
  var book = await db.Books.FindAsync(id);
  if (book is null) return Results.NotFound();

  db.Books.Remove(book);
  await db.SaveChangesAsync();
  return Results.NoContent();
});

app.Run();
