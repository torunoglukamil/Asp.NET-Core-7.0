using Microsoft.AspNetCore.Mvc;
using SchoolManager.Business.Services;
using SchoolManager.Models.Models;

namespace PostgreSqlExample.Controllers
{
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        private readonly StudentService _service;

        public StudentApiController(StudentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/[controller]/GetStudents")]
        public IActionResult Get()
        {
            var result = _service.Query.GetAllList();
            return Ok(result);
        }

        [HttpGet]
        [Route("api/[controller]/GetStudentById/{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.Query.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/[controller]/CreateStudent")]
        public IActionResult Post([FromBody] students student)
        {
            var result = _service.Repository.Add(student);
            return Ok(result);
        }

        [HttpPut]
        [Route("api/[controller]/UpdateStudent")]
        public IActionResult Put([FromBody] students student)
        {
            var result = _service.Repository.Add(student);
            return Ok(result);
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteStudentById/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Query.GetById(id);
            return Ok(result);
        }
    }
}
