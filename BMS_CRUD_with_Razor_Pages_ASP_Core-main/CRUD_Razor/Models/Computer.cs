using System.ComponentModel.DataAnnotations;

namespace CRUD_Razor.Models
{
    public class Computer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand name is required")]
        [StringLength(50)]
        [Display(Name = "Brand / Manufacturer")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model name is required")]
        [StringLength(50)]
        [Display(Name = "Model")]
        public string Model { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Processor (CPU)")]
        public string? Processor { get; set; }

        [StringLength(50)]
        [Display(Name = "RAM (Memory)")]
        public string? Ram { get; set; }

        [StringLength(50)]
        [Display(Name = "Storage (SSD/HDD)")]
        public string? Storage { get; set; }

        [Range(0, 100000, ErrorMessage = "Price must be a valid positive amount")]
        [DataType(DataType.Currency)]
        [Display(Name = "Price ($)")]
        public decimal? Price { get; set; }

        [StringLength(50)]
        [Display(Name = "Status")]
        public string? Status { get; set; }
    }
}
