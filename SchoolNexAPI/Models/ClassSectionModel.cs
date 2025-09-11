using SchoolNexAPI.Models.Student;
using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class ClassSectionModel
    {
        [Key]
        public Guid Id { get; set; }
        public string SectionName { get; set; }

        public List<StudentModel> Students { get; set; }
    }
}
