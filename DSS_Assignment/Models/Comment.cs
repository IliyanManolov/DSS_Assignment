using System.ComponentModel.DataAnnotations.Schema;

namespace DSS_Assignment.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
    }
}
