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
            IActionResult result = _service.Query.GetStudents();
            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetStudentById/{id}")]
        public IActionResult Get(int id)
        {
            IActionResult result = _service.Query.GetStudentById(id);
            return result;
        }

        [HttpPost]
        [Route("api/[controller]/CreateStudent")]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            IActionResult result = await _service.Repository.CreateStudent(student);
            return result;
        }

        [HttpPut]
        [Route("api/[controller]/UpdateStudent")]
        public async Task<IActionResult> Put([FromBody] Student student)
        {
            IActionResult result = await _service.Repository.UpdateStudent(student);
            return result;
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteStudentById/{id}")]
        public IActionResult Delete(int id)
        {
            IActionResult result = _service.Repository.DeleteStudentById(id);
            return result;
        }
    }
}
