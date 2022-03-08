using System.Collections.Generic;
using Sheldier.UI;

namespace Sheldier.Constants
{
    public static class ResourcePaths
    {
        public const string ACTOR_TEMPLATE = "Actors/A_Yellow";
        public const string ACTOR_HAND = "Actors/Hand";
        
        public static readonly Dictionary<UIType, string> UI_STATE_PATHS = new Dictionary<UIType, string>()
        {
            {UIType.Inventory, "UIState/InventoryUIState"},
            {UIType.GameplayPauseMenu, ""}
        };

        public const string COLONY_SCENE_DATA_PATH = "SceneData/ColonyOutside";
    }
}