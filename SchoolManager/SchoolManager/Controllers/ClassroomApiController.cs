using Microsoft.AspNetCore.Mvc;
using SchoolManager.Business.Services;
using SchoolManager.Models.Models;

namespace SchoolManager.Controllers
{
    [ApiController]
    public class ClassroomApiController
    {
        private readonly ClassroomService _service;

        public ClassroomApiController(ClassroomService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/[controller]/GetClassrooms")]
        public IActionResult Get()
        {
            var result = _service.Query.GetClassrooms();
            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetClassroomById/{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.Query.GetClassroomById(id);
            return result;
        }

        [HttpPost]
        [Route("api/[controller]/CreateClassroom")]
        public IActionResult Post([FromBody] classroom classroom)
        {
            var result = _service.Repository.CreateClassroom(classroom);
            return result;
        }

        [HttpPut]
        [Route("api/[controller]/UpdateClassroom")]
        public IActionResult Put([FromBody] classroom classroom)
        {
            var result = _service.Repository.UpdateClassroom(classroom);
            return result;
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteClassroomById/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Repository.DeleteClassroomById(id);
            return result;
        }
    }
}
