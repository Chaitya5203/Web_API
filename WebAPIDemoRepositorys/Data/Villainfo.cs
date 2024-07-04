using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPIDemoRepositorys.Data;
[Table("villainfo")]
public partial class Villainfo : BaseEntity
{
    //[Key]
    //[Column("id")]
    //public int Id { get; set; }
    //[Column("name")]
    //[StringLength(100)]
    //public string Name { get; set; } = null!;
    [Column("details")]
    [StringLength(100)]
    public string Details { get; set; } = null!;
    [Column("rate")]
    public decimal? Rate { get; set; }
    [Column("sqft")]
    public int? Sqft { get; set; }
    [Column("occupancy")]
    public int? Occupancy { get; set; }
    [Column("amenity")]
    [StringLength(100)]
    public string? Amenity { get; set; }
    [Column("createddate", TypeName = "timestamp without time zone")]
    public DateTime Createddate { get; set; }
    [Column("modifieddate", TypeName = "timestamp without time zone")]
    public DateTime? Modifieddate { get; set; }
}