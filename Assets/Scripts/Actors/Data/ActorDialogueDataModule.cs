using Sheldier.Actors.Data;
using Sheldier.Common.Audio;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorDialogueDataModule
    {
        public float TypeSpeed => _typeSpeed;
        public Color TypeColor => _typeColor;
        public AudioUnit TypeVoice => _typeVoice;

        private float _typeSpeed;
        private Color _typeColor;
        private AudioUnit _typeVoice;
        public ActorDialogueDataModule(ActorDialogueConfig dataDialogueConfig)
        {
            _typeSpeed = dataDialogueConfig.TypeSpeed;
            _typeColor = dataDialogueConfig.TextColor;
            _typeVoice = dataDialogueConfig.VoiceSound;

        }
    }
}