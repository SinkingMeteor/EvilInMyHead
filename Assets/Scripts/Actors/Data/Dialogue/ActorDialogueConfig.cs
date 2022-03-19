using Sheldier.Common.Audio;
using Sheldier.ScriptableObjects;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    [CreateAssetMenu(fileName = "ActorDialogueConfig", menuName = "Sheldier/Actor/DialogueConfig")]
    public class ActorDialogueConfig : BaseScriptableObject
    {
        public float TypeSpeed => typeSpeed;
        public AudioUnit VoiceSound => voiceSound;
        public Color TextColor => textColor;
        
        [SerializeField] [Range(0.01f, 5f)] private float typeSpeed;
        [SerializeField] private AudioUnit voiceSound;
        [SerializeField] private Color textColor;
    }
}