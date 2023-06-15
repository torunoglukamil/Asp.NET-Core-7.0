using Microsoft.AspNetCore.Mvc;
using SchoolManager.Models.Models;

namespace SchoolManager.DataAccess.Queries
{
    public class ClassroomQuery : ControllerBase
    {
        private readonly school_managerContext _db;

        public ClassroomQuery(school_managerContext context)
        {
            _db = context;
        }

        public IActionResult GetClassrooms()
        {
            try
            {
                List<Classroom> classroomList = _db.classrooms.OrderBy(x => x.grade).ThenBy(x => x.branch).ToList();
                return Ok(classroomList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult GetClassroomById(int id)
        {
            try
            {
                Classroom? classroom = _db.classrooms.Where(x => x.id == id).FirstOrDefault();
                if (classroom == null)
                {
                    return NotFound();
                }
                return Ok(classroom);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
