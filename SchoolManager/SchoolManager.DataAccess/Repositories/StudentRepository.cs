using SchoolManager.Models.Models;

namespace SchoolManager.DataAccess.Repositories
{
    public class StudentRepository
    {
        private readonly school_managerContext _context;

        public StudentRepository(school_managerContext context)
        {
            _context = context;
        }


        public students Add(students entitiy)
        {
            return entitiy;
        }
    }
}
