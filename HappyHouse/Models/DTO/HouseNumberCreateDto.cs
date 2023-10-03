using System.ComponentModel.DataAnnotations;

namespace HappyHouse.Models
{
    public class HouseNumberCreateDto
    {
        [Required]
        public int HouseNo { get; set; }

        [Required]
        public int HouseId { get; set; }
        public string? SpecialDetails { get; set; }
    }
}
