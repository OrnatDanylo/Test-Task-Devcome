using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Column(TypeName = "varchar(20)")]
        [Unique]
        public string Login { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? Password { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? FirstName { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public char? Gender { get; set; }

        [BindNever]
        public virtual ICollection<OrderEntity>? Orders { get; set; }
    }
}
