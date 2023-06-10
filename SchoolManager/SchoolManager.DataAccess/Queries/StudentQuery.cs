using SchoolManager.Models.Models;

namespace SchoolManager.DataAccess.Queries
{
    public class StudentQuery
    {
        private readonly school_managerContext _context;

        public StudentQuery(school_managerContext context)
        {
            _context = context;
        }

        public List<students> GetAllList()
        {
            var isAdmin = true;
            var query = _context.students.AsQueryable();
            if (!isAdmin)
            {
                query = query.Where(p => p.id > 0);
            }
            var result = query.ToList();
            return result;
        }

        public students GetById(int id)
        {
            var query = _context.students.Where(p => p.id == id);
            var result = query.FirstOrDefault();
            return result;
        }
    }
}
