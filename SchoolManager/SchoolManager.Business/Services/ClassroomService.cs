using SchoolManager.DataAccess.Queries;
using SchoolManager.DataAccess.Repositories;

namespace SchoolManager.Business.Services
{
    public class ClassroomService
    {
        private readonly ClassroomQuery _query;
        private readonly ClassroomRepository _repository;

        public ClassroomService(ClassroomQuery query, ClassroomRepository repository)
        {
            _query = query;
            _repository = repository;
        }

        public ClassroomQuery Query { get {  return _query; } }

        public ClassroomRepository Repository { get {  return _repository; } }
    }
}
