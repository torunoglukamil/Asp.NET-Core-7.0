using Microsoft.AspNetCore.Mvc;
using SchoolManager.Models.Models;

namespace SchoolManager.DataAccess.Queries
{
    public class StudentQuery : ControllerBase
    {
        private readonly school_managerContext _db;

        public StudentQuery(school_managerContext context)
        {
            _db = context;
        }

        public IActionResult GetStudents()
        {
            try
            {
                List<Student> studentList = _db.students.ToList();
                return Ok(studentList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult GetStudentById(int id)
        {
            try
            {
                Student? student = _db.students.Where(x => x.id == id).FirstOrDefault();
                if (student == null)
                {
                    return BadRequest("Öğrenci bulunamadı.");
                }
                Classroom? classroom = _db.classrooms.Where(x => x.id == student.classroom_id).FirstOrDefault();
                if (classroom == null)
                {
                    return BadRequest("Sınıf bulunamadı.");
                }
                student.classroom = classroom;
                return Ok(student);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
