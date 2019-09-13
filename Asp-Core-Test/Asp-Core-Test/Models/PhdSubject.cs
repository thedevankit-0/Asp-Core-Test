using System;
using System.ComponentModel.DataAnnotations;

namespace Asp_Core_Test.Models
{
    #region "Subject model"
    public class PhdSubject
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please add subject name")]
        [RegularExpression(@"([a-zA-Z]{4,50}\s*)+", ErrorMessage = "Subject name accept only characters.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Name of subject must be between 4 and 50 charcharacters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please add subject description")]
        [StringLength(150, MinimumLength = 4, ErrorMessage = "Description of subject must be between 4 and 150 characters.")]
        public string Description { get; set; }
    }
    #endregion
}
