using UnityEngine;

namespace Sheldier.Actors.Pathfinding
{
    public struct PathNode
    {
        public int X;
        public int Y;
            
        public int Index;
            
        public int GCost;
        public int HCost;
        public int FCost;

        public bool IsWalkable;

        public int CameFromNodeIndex;
        public Vector2 WorldPosition;

        public void CalculateFCost() => FCost = GCost + HCost;

        public void SetWalkable(bool isWalkable) => IsWalkable = isWalkable;
    }
}