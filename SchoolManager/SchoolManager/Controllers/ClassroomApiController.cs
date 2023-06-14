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
            IActionResult result = _service.Query.GetClassrooms();
            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetClassroomById/{id}")]
        public IActionResult Get(int id)
        {
            IActionResult result = _service.Query.GetClassroomById(id);
            return result;
        }

        [HttpPost]
        [Route("api/[controller]/CreateClassroom")]
        public async Task<IActionResult> Post([FromBody] Classroom classroom)
        {
            IActionResult result = await _service.Repository.CreateClassroom(classroom);
            return result;
        }

        [HttpPut]
        [Route("api/[controller]/UpdateClassroom")]
        public async Task<IActionResult> Put([FromBody] Classroom classroom)
        {
            IActionResult result = await _service.Repository.UpdateClassroom(classroom);
            return result;
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteClassroomById/{id}")]
        public IActionResult Delete(int id)
        {
            IActionResult result = _service.Repository.DeleteClassroomById(id);
            return result;
        }
    }
}
