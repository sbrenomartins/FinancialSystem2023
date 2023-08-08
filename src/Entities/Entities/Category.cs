using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities;

[Table("Category")]
public class Category : Base
{
    [ForeignKey("FinancialSystem")]
    [Column(Order = 1)]
    public int SystemId { get; set; }
    public virtual FinancialSystem FinancialSystem { get; set; }
}
