using System.Collections.Generic;
using Sheldier.ScriptableObjects;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;

namespace Sheldier.Common.Localization
{
    [CreateAssetMenu(menuName = "Sheldier/Common/FontMap", fileName = "FontMap")]
    public class FontMap : BaseScriptableObject
    {
        [OdinSerialize] private Dictionary<FontType, TMP_FontAsset[]> fontCollection;

        public TMP_FontAsset GetFontAsset(FontType fontType, Language language)
        {
            return fontCollection[fontType][(int) language];
        }
    }
}