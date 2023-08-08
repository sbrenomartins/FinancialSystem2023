using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;

namespace Entities.Entities;

[Table("Expense")]
public class Expense : Base
{
    public decimal Value { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public ExpenseTypeEnum ExpenseType { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool Paid { get; set; }
    public bool ExpenseLate { get; set; }

    [ForeignKey("Category")]
    [Column(Order = 1)]
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}
