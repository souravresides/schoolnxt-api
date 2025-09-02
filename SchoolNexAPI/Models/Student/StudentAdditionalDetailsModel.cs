using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Student
{
    public class StudentAdditionalDetailsModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public StudentModel Student { get; set; }
        public Guid SchoolId { get; set; }
        public string? Religion { get; set; }
        public string? Category { get; set; }
        public bool RightToEducation { get; set; }
        public string? Nationality { get; set; }
        public bool BPLStudent { get; set; }
        public string? BPLCardNo { get; set; }
        public bool PwD { get; set; }
        public string? TypeOfDisability { get; set; }
        public string? IdentificationMark { get; set; }
        public string? MotherTongue { get; set; }
        public string? SecondLanguage { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public bool SingleParentChild { get; set; }
        public string? SingleParent { get; set; }
        public bool SponsoredStudent { get; set; }
        public string? SponsorName { get; set; }
    }
}
