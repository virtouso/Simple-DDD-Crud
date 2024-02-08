using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mc2.CrudTest.Infrastructure.Models;

public class DbCustomer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, Column(Order = 0)]
    public Int64 Id { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    [Column(TypeName = "nvarchar(12)")]
    public string PhoneNumber { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string Email { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string BankAccountNumber { get; set; }
}