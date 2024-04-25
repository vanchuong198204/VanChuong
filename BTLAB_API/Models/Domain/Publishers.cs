﻿using System.ComponentModel.DataAnnotations;

namespace BTLAB_API.Models.Domain
{
    public class Publishers
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Books> Book { get; set; }
    }
}
