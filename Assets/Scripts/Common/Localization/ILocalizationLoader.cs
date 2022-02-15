using System.Collections.Generic;

namespace Sheldier.Common.Localization
{
    public interface ILocalizationLoader
    {
        Dictionary<string, string> LoadFile(Language language);
    }
}