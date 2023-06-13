using Microsoft.AspNetCore.Mvc;
using SchoolManager.Models.Models;

namespace SchoolManager.DataAccess.Repositories
{
    public class ClassroomRepository : ControllerBase
    {
        private readonly school_managerContext _db;

        public ClassroomRepository(school_managerContext context)
        {
            _db = context;
        }

        public IActionResult CreateClassroom(classroom classroom)
        {
            try
            {
                classroom? _classroom = _db.classrooms.Where(c => c.grade == classroom.grade && c.branch == classroom.branch).FirstOrDefault();
                if (_classroom != null)
                {
                    return BadRequest("classroom-already-exist");
                }
                if (classroom.branch.Length > 1)
                {
                    return BadRequest("branch-over-1-character");
                }
                _db.classrooms.Add(classroom);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        public IActionResult UpdateClassroom(classroom classroom)
        {
            try
            {
                classroom? _classroom = _db.classrooms.Where(c => c.grade == classroom.grade && c.branch == classroom.branch).FirstOrDefault();
                if (_classroom != null && _classroom.id != classroom.id)
                {
                    return BadRequest("classroom-already-exist");
                }
                _classroom = _db.classrooms.Where(c => c.id == classroom.id).FirstOrDefault();
                if (_classroom == null)
                {
                    return BadRequest("classroom-not-found");
                }
                if (classroom.branch.Length != 1)
                {
                    return BadRequest("branch-not-1-character");
                }
                _classroom.grade = classroom.grade;
                _classroom.branch = classroom.branch;
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        public IActionResult DeleteClassroomById(int id)
        {
            try
            {
                classroom? classroom = _db.classrooms.Where(c => c.id == id).FirstOrDefault();
                if (classroom == null)
                {
                    return BadRequest("classroom-not-found");
                }
                _db.classrooms.Remove(classroom);
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
