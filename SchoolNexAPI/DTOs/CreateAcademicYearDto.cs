using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.DTOs
{
    public class CreateAcademicYearDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public Guid SchoolId { get; set; }
    }
}
