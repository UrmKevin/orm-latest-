﻿namespace orm.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public int AuthorId { get; set; }
    }
}