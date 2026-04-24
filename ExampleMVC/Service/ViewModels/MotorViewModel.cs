using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.ViewModels
{
    public class MotorViewModel
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public int Price { get; set; }
    }
}
