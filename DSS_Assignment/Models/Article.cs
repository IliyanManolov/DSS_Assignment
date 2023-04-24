﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS_Assignment.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        //[ForeignKey("Comments")]
        //public List<Comment> CommentsId { get; set; }
    }
}
