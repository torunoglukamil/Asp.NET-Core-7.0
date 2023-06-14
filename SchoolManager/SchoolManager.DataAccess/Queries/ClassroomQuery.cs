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
                List<classroom> classroomList = _db.classrooms.ToList();
                return Ok(classroomList);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        public IActionResult GetClassroomById(int id)
        {
            try
            {
                classroom? classroom = _db.classrooms.Where(x => x.id == id).FirstOrDefault();
                if (classroom == null)
                {
                    return NotFound();
                }
                return Ok(classroom);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
