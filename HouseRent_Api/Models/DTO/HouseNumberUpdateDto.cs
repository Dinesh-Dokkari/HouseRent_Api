using System.ComponentModel.DataAnnotations;

namespace HouseRent_Api.Models.DTO
{
    public class HouseNumberUpdateDto
    {
        [Required]
        public int HouseNo { get; set; }

        [Required]
        public int HouseId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
