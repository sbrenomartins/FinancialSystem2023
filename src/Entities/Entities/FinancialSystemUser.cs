using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities;

[Table("FinancialSystemUser")]
public class FinancialSystemUser
{
    public int Id { get; set; }
    public string UserEmail { get; set; }
    public bool Administrator { get; set; }
    public bool ActualSystem { get; set; }

    [ForeignKey("FinancialSystem")]
    [Column(Order = 1)]
    public int SystemId { get; set; }
    public virtual FinancialSystem FinancialSystem { get; set; }
}
