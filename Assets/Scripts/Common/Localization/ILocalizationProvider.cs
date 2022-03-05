using System.Collections.Generic;

namespace Sheldier.Common.Localization
{
    public interface ILocalizationProvider
    {
        IReadOnlyDictionary<string, string> LocalizedText { get; }
    }
}