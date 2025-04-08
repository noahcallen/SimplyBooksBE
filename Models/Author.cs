namespace SimplyBooks.Models
{
  public class Author
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Image { get; set; }
    public bool Favorite { get; set; }
    public int UserId { get; set; }
    public List<Book> Books { get; set; }
  }
}