using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAPIDemoRepositorys.Data;

[Table("LocalUser")]
public partial class LocalUser :BaseEntity
{
    //[Key]
    //[Column("id")]
    //public int Id { get; set; }

    //[Column("name", TypeName = "character varying")]
    //public string? Name { get; set; }
    [Column(TypeName = "character varying")]
    public string? UserName { get; set; }
    [Column(TypeName = "character varying")]
    public string? Password { get; set; }

    [Column(TypeName = "character varying")]
    public string? Role { get; set; }
}
