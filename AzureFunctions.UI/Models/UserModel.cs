﻿using System.ComponentModel.DataAnnotations;

namespace AzureFunctions.UI.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Phone { get; set; }

        public string? Status { get; set; }
    }
}
