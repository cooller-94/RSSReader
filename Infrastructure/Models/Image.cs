using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ImageId { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Link { get; set; }

        public double? Width { get; set; }

        public double? Height { get; set; }

        public string Description { get; set; }
    }
}
