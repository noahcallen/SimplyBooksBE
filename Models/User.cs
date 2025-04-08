using System.Diagnostics.Eventing.Reader;

namespace SimplyBooks.Models
{
  public class User
  {
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<Author> Authors { get; set; }
    public List<Book> Books { get; set; }
  }
}