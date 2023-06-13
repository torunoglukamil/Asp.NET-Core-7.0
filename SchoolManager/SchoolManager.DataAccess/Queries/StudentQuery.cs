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
                List<student> studentList = _db.students.ToList();
                return Ok(studentList);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        public IActionResult GetStudentById(int id)
        {
            try
            {
                student? student = _db.students.Where(s => s.id == id).FirstOrDefault();
                if (student == null)
                {
                    return NotFound();
                }
                classroom? classroom = _db.classrooms.Where(c => c.id == student.classroom_id).FirstOrDefault();
                if (classroom == null)
                {
                    return NotFound();
                }
                student.classroom = classroom;
                return Ok(student);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
