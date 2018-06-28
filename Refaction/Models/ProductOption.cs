using System;
using System.ComponentModel.DataAnnotations;

namespace Refaction.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

    }
}