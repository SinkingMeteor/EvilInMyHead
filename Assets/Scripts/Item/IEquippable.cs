using Sheldier.Actors;
using UnityEngine;

namespace Sheldier.Item
{

    public interface IEquippable
    {
        Transform Transform { get; }
        void OnEquip(IActorsHand actorsHand);
        void OnUnEquip();
    }
    
    
}