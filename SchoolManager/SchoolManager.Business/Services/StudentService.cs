using SchoolManager.DataAccess.Queries;
using SchoolManager.DataAccess.Repositories;

namespace SchoolManager.Business.Services
{
    public class StudentService
    {
        private readonly StudentQuery _query;
        private readonly StudentRepository _repository;

        public StudentService(StudentQuery query, StudentRepository repository)
        {
            _query = query;
            _repository = repository;
        }

        public StudentQuery Query { get {  return _query; } }

        public StudentRepository Repository { get {  return _repository; } }
    }
}
