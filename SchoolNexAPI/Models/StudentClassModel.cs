using SchoolNexAPI.Models.Student;
using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class StudentClassModel
    {
        [Key]
        public Guid Id { get; set; }
        public string ClassName { get; set; }

        public List<StudentModel> Students { get; set; }
    }
}
