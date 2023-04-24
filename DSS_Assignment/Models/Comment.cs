using System.ComponentModel.DataAnnotations.Schema;

namespace DSS_Assignment.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
    }
}
