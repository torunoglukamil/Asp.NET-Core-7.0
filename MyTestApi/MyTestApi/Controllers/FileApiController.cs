using Microsoft.AspNetCore.Mvc;
using MyTestApi.Models;
using MyTestApi.Repositories;

namespace MyTestApi.Controllers
{
    [ApiController]
    public class FileApiController
    {
        private readonly FileRepository _fileRepository;

        public FileApiController(FileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [HttpPost]
        [Route("api/[controller]/UploadFile")]
        public async Task<ResponseModel> UploadFile([FromBody] FileModel file) => await _fileRepository.UploadFile(file);

        [HttpPost]
        [Route("api/[controller]/Test/{fileName}")]
        public ResponseModel Test(string fileName) => _fileRepository.Test(fileName);
    }
}
