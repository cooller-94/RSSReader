using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("Image")]
    public class Image
    {
        [Column("image_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ImageId { get; set; }

        [Column("url")]
        [Required]
        public string Url { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("link")]
        public string Link { get; set; }

        [Column("width")]
        public double? Width { get; set; }

        [Column("height")]
        public double? Height { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}
