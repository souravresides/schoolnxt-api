using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.DTOs
{
    public class CreateAcademicYearDto
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsCurrent { get; set; } = false;
        public bool IsLocked { get; set; } = false;
    }
}
