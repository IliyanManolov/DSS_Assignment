using System.ComponentModel.DataAnnotations;

namespace DSS_Assignment.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
        //Comments?
    }
}
