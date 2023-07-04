using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class LogConverter
    {
        public static log ToLog(LogModel log) => new()
        {
            type = log.type,
            message = log.message,
            data = log.data,
        };
    }
}
