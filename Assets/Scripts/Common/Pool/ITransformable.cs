using UnityEngine;

namespace Sheldier.Common.Pool
{
    public interface ITransformable
    {
        Transform Transform { get; }
    }
}