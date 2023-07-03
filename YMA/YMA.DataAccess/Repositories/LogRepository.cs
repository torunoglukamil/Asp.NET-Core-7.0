using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Repositories
{
    public class LogRepository
    {
        private readonly ymaContext _db;
        private readonly LogConverter _logConverter;

        public LogRepository(ymaContext db, LogConverter logConverter)
        {
            _db = db;
            _logConverter = logConverter;
        }

        public void CreateLog(LogModel log)
        {
            try
            {
                log _log = _logConverter.ToLog(log);
                _log.create_date = DateTime.Now;
                _db.logs.Add(_log);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
