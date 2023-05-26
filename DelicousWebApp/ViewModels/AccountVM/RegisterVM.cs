using System.ComponentModel.DataAnnotations;

namespace DelicousWebApp.ViewModels.AccountVM
{
    public class RegisterVM
    {
        [EmailAddress,Required]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password),MinLength(8)]
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        [MaxLength(29),Required]
        public string UserName { get; set; } = null!;
        [DataType(DataType.Password), MinLength(8),Compare(nameof(Password))]
        public string ConfrimPassword { get; set; } = null!;
    }
}
