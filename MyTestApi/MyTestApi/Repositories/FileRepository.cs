using MyTestApi.Models;

namespace MyTestApi.Repositories
{
    public class FileRepository
    {
        public async Task<ResponseModel> UploadFile(FileModel file)
        {
            Console.WriteLine(file.name);
            Console.WriteLine(file.bytes);
            return new ResponseModel
            {
                status_code = StatusCodes.Status200OK,
                data = file.bytes,
            };
        }

        public ResponseModel Test(string fileName)
        {
            Console.WriteLine(fileName);
            return new ResponseModel
            {
                status_code = StatusCodes.Status200OK,
            };
        }
    }
}
