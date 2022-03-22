using System.Collections.Generic;

namespace Sheldier.Common.Localization
{
    public interface ILocalizationProvider
    {
        Language CurrentLanguage { get; }
        IReadOnlyDictionary<string, string> LocalizedText { get; }
        void AddListener(ILocalizationListener listener);
        void RemoveListener(ILocalizationListener listener);
    }
}