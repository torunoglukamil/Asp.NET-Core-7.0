using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Repositories
{
    public class LogRepository
    {
        private readonly ymaContext _db;

        public LogRepository(ymaContext db)
        {
            _db = db;
        }

        public void CreateLog(LogModel log)
        {
            try
            {
                log _log = LogConverter.ToLog(log);
                _log.create_date = DateTime.Now;
                _db.logs.Add(_log);
                _db.SaveChanges();
            }
            catch (Exception) { }
        }
    }
}
