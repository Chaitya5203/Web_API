using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemoRepositorys.ViewModel
{
    public class GetAllVillaDTO
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
}
