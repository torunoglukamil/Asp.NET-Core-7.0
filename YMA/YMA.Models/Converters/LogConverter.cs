using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class LogConverter
    {
        public static log ToLog(LogModel log) => new()
        {
            id = Guid.NewGuid().ToString(),
            type = log.type,
            message = log.message,
            data = log.data,
            create_date = DateTime.Now,
        };
    }
}
