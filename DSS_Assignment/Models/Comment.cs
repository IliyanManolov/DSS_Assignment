using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS_Assignment.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        [ForeignKey("Articles")]
        public int ArticleId { get; set; }
    }
}
