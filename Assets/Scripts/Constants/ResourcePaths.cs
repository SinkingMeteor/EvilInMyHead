using System.Collections.Generic;
using Sheldier.UI;

namespace Sheldier.Constants
{
    public static class ResourcePaths
    {
        public const string ACTOR_TEMPLATE = "Actors/A_Yellow";
        public const string ACTOR_HAND = "Actors/Hand";
        public const string ACTOR_INTERACT_MODULE = "Actors/InteractModule";
        
        public const string PIXEL_PERFECT_CAMERA = "Scene/MainCamera";
        
        public static readonly Dictionary<UIType, string> UI_STATE_PATHS = new Dictionary<UIType, string>()
        {
            {UIType.Inventory, "UIState/InventoryUIState"},
            {UIType.GameplayPauseMenu, ""}
        };

        public const string COLONY_SCENE_DATA_PATH = "SceneData/ColonyOutside";

        public const string UNLIT_OUTLINE_MATERIAL = "UnlitOutline";
    }
}