using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS_Assignment.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string Image { get; set; }
        public int CommentsAmount { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
    }
}
