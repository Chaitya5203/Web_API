using System.ComponentModel.DataAnnotations;
namespace WebAPIDemoRepositorys.ViewModel
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string? Name { get; set; }
        public string Details { get; set; }
        public decimal Rate { get; set; }
        public int sqft { get; set; }
        public string? Amenity { get; set; }
        public int occupancy { get; set; }
    }
    public class VillaDto2
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public bool PreviousPage { get; set; }
        public bool NextPage { get; set; }
        public List<VillaDTO>? data { get; set; }
    }
}
