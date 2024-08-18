﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ShoesStore.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public   Product? product { get; set; }


    }
}
