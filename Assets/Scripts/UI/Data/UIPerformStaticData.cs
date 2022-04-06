using System;
using Sheldier.Data;

namespace Sheldier.UI
{
    [Serializable]
    public struct UIPerformStaticData : IDatabaseItem
    {
        public string ID => PerformType;
        public string PerformType;
        public string Localization;
    }
}