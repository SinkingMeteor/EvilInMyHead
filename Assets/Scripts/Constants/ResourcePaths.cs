using System.Collections.Generic;
using Sheldier.UI;

namespace Sheldier.Constants
{
    public static class ResourcePaths
    {
        //actor
        public const string ACTOR_TEMPLATE = "Actors/Actor_Humanoid";
        public const string ACTOR_HAND = "Actors/Hand";
        public const string ACTOR_INTERACT_MODULE = "Actors/InteractModule";
        public const string ACTOR_MAP = "Actors/ActorsMap";
        public const string ACTOR_APPEARANCE_DIRECTORY = "Appearance/";
        
        //camera
        public const string PIXEL_PERFECT_CAMERA = "Scene/MainCamera";
        
        //ui
        public static readonly Dictionary<UIType, string> UI_STATE_PATHS = new Dictionary<UIType, string>()
        {
            {UIType.Inventory, "UIState/InventoryUIState"},
            {UIType.Dialogue, "UIState/DialoguesUIState"}
        };

        //sceneData
        public const string COLONY_SCENE_DATA_PATH = "SceneData/ColonyOutside";

        //materials
        public const string UNLIT_OUTLINE_MATERIAL = "UnlitOutline";
        
        //locations
        public const string LOCATIONS_PATH_TEMPLATE = "Locations/";
        
        //sprites
        public const string SPRITE_PATH_TEMPLATE = "Sprites/";

        //fonts
        public const string FONT_MAP = "Fonts/FontMap";
        
        //installers
        public const string INPUT_INSTALLER = "Installers/Input";
        public const string TIME_INSTALLER = "Installers/Time";
        public const string POOLS_INSTALLER = "Installers/Pools";
        public const string AUDIO_INSTALLER = "Installers/Audio";
        
        //effects
        public const string EFFECTS_MAP = "Effects/EffectsDataMap";

        //items
        public const string ITEMS_MAP = "Items/ItemMap";
        
        //cutscenes
        public const string TEST_CUTSCENE = "Cutscenes/TestCutScene";
        
        //data
        public const string JSON_PATH_DIRECTORY = "Text/";
    }
}