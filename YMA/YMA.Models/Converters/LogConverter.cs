using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class LogConverter
    {
        public log ToLog(LogModel log) => new log()
        {
            type = log.type,
            message = log.message,
            data = log.data,
        };
    }
}
