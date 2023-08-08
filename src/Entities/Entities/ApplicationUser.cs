using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Entities.Entities;

public class ApplicationUser : IdentityUser
{
    [Column("Cpf")]
    public string Cpf { get; set; }
}
