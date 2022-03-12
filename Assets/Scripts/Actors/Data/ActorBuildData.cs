using Sheldier.Actors.Builder;
using Sheldier.ScriptableObjects;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    [CreateAssetMenu(menuName = "Sheldier/Actor/ActorBuildData", fileName = "ActorBuildData")]
    public class ActorBuildData : BaseScriptableObject
    {
        public bool CanMove => _canMove;
        public bool CanEquip => _canEquip;
        public bool IsEffectsPerceptive => _isEffectPerceptive;
        public bool CanInteract => _canInteract;
        public bool CanAttack => _canAttack;
        public InteractType InteractType => _interactType;

        [SerializeField] private bool _canMove;
        [SerializeField] private bool _canEquip;
        [SerializeField] private bool _isEffectPerceptive;
        [SerializeField] private bool _canInteract;
        [SerializeField] private bool _canAttack;
        [SerializeField] private InteractType _interactType;
    }
}