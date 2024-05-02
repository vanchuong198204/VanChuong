﻿using System.ComponentModel.DataAnnotations;

namespace BTLAB_API.Models.Domain
{
    public class Authors 
    {
        [Key] 
        public int Id { get; set; }
        public string? FullName { get; set; }
        public List<Book_Author>? Book_Authors { get; set; }
        
    }
}
