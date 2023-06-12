using Microsoft.AspNetCore.Mvc;
using SchoolManager.Business.Services;
using SchoolManager.Models.Models;

namespace SchoolManager.Controllers
{
    [ApiController]
    public class StudentApiController
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
            var result = _service.Query.GetStudents();
            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetStudentById/{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.Query.GetStudentById(id);
            return result;
        }

        [HttpPost]
        [Route("api/[controller]/CreateStudent")]
        public IActionResult Post([FromBody] student student)
        {
            var result = _service.Repository.CreateStudent(student);
            return result;
        }

        [HttpPut]
        [Route("api/[controller]/UpdateStudent")]
        public IActionResult Put([FromBody] student student)
        {
            var result = _service.Repository.UpdateStudent(student);
            return result;
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteStudentById/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Repository.DeleteStudentById(id);
            return result;
        }
    }
}
