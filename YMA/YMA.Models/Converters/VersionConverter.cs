using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class VersionConverter
    {
        public static VersionModel ToModel(version version) => new()
        {
            version = version.version1,
            download_url = version.download_url,
        };
    }
}
