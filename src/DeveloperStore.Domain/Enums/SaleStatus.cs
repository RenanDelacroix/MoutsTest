using System.ComponentModel.DataAnnotations;

namespace DeveloperStore.Domain.Enums;

public enum SaleStatus
{
    [Display(Name = "Created")]
    Created = 1,

    [Display(Name = "Paid")]
    Paid = 2,

    [Display(Name = "Cancelled")]
    Cancelled = 3
}
