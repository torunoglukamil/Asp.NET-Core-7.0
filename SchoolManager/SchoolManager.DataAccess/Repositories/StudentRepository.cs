using Microsoft.AspNetCore.Mvc;
using SchoolManager.Models.Models;

namespace SchoolManager.DataAccess.Repositories
{
    public class StudentRepository : ControllerBase
    {
        private readonly school_managerContext _db;

        public StudentRepository(school_managerContext context)
        {
            _db = context;
        }

        public IActionResult CreateStudent(student student)
        {
            try
            {
                student? _student = _db.students.Where(s => s.email == student.email).FirstOrDefault();
                if (_student != null)
                {
                    return BadRequest("email-already-in-use");
                }
                if (student.phone != null)
                {
                    _student = _db.students.Where(s => s.phone == student.phone).FirstOrDefault();
                    if (_student != null)
                    {
                        return BadRequest("phone-already-in-use");
                    }
                }
                classroom? classroom = _db.classrooms.Where(c => c.id == student.classroom_id).FirstOrDefault();
                if (classroom == null)
                {
                    return BadRequest("classroom-not-found");
                }
                if (student.first_name.Length > 50)
                {
                    return BadRequest("first-name-over-50-characters");
                }
                if (student.last_name.Length > 50)
                {
                    return BadRequest("last-name-over-50-characters");
                }
                if (student.age < 18)
                {
                    return BadRequest("age-under-18");
                }
                if (student.email.Length > 100)
                {
                    return BadRequest("email-over-100-characters");
                }
                if ((student.phone?.Length ?? 0) > 15)
                {
                    return BadRequest("phone-over-15-characters");
                }
                _db.students.Add(student);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        public IActionResult UpdateStudent(student student)
        {
            try
            {
                student? _student = _db.students.Where(s => s.id == student.id).FirstOrDefault();
                if (_student == null)
                {
                    return BadRequest("student-not-found");
                }
                classroom? classroom = _db.classrooms.Where(c => c.id == student.classroom_id).FirstOrDefault();
                if (classroom == null)
                {
                    return BadRequest("classroom-not-found");
                }
                if (student.first_name.Length > 50)
                {
                    return BadRequest("first-name-over-50-characters");
                }
                if (student.last_name.Length > 50)
                {
                    return BadRequest("last-name-over-50-characters");
                }
                if (student.age < 18)
                {
                    return BadRequest("age-under-18");
                }
                _student.first_name = student.first_name;
                _student.last_name = student.last_name;
                _student.age = student.age;
                _student.classroom_id = student.classroom_id;
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        public IActionResult DeleteStudentById(int id)
        {
            try
            {
                student? student = _db.students.Where(s => s.id == id).FirstOrDefault();
                if (student == null)
                {
                    return BadRequest("student-not-found");
                }
                _db.students.Remove(student);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
