using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Core_Test.Models
{
    #region "Student model validation."
    public class Student
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [RegularExpression(@"([a-zA-Z]{4,50}\s*)+", ErrorMessage = "First name accept only character")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "First name must be between 4 and 50 character")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [RegularExpression(@"([a-zA-Z]{3,20}\s*)+", ErrorMessage = "Last name only accepte characters.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Last name must be between 4 and 50 character")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression(@"^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$", ErrorMessage = "Email address should be in the format abc@abc.com")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Email must be between 4 and 50 character")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Address must be between 4 and 50 character")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your contact number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?([0-9]{3})?([0-9]{4})$", ErrorMessage = "Contact number only accepte 10 digit numbers.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Contact number must be 10 digit")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Please select your gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please enter education year")]
        public YearOfGraduation Year { get; set; }

        [Required(ErrorMessage = "Please select your subject")]
        public Guid PhdSubjectId { get; set; }
        [ForeignKey("PhdSubjectId")]
        public virtual PhdSubject phdSubject { get; set; }
    }
    #endregion

    #region "Enum for Year"
    public enum YearOfGraduation
    {
        FE = 1,
        SE = 2
    }
    #endregion
}