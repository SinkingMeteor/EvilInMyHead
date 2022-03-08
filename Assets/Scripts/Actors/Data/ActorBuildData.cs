using Sheldier.ScriptableObjects;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    [CreateAssetMenu(menuName = "Sheldier/Actor/ActorBuildData", fileName = "ActorBuildData")]
    public class ActorBuildData : BaseScriptableObject
    {
        public ActorData Data => actorData;
        public bool CanMove => _canMove;
        public bool CanEquip => _canEquip;
        public bool IsEffectsPerceptive => _isEffectPerceptive;

        [SerializeField] private ActorData actorData;
        [SerializeField] private bool _canMove;
        [SerializeField] private bool _canEquip;
        [SerializeField] private bool _isEffectPerceptive;
    }
}