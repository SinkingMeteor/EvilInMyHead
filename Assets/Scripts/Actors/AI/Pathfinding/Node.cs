using UnityEngine;

namespace Sheldier.Actors.Pathfinding
{
    public class Node : IHeapItem<Node>
    {
        public bool IsWalkable => _isWalkable;
        public Vector3 WorldPosition => _worldPosition;
        public Vector2Int GridPosition => _gridPosition;
        public int HeapIndex {
            get => _heapIndex;
            set => _heapIndex = value;
        }

        public Node Parent => _parent;
        
        public int FCost => _gCost + _hCost;
        public int GCost => _gCost;
        public int HCost => _hCost;
        
        private bool _isWalkable;
        private Vector3 _worldPosition;
        private int _gCost;
        private int _hCost;
        private Vector2Int _gridPosition;
        private Node _parent;
        private int _heapIndex;

        public Node(bool isWalkable, Vector3 worldPosition, Vector2Int gridPosition)
        {
            _isWalkable = isWalkable;
            _worldPosition = worldPosition;
            _gridPosition = gridPosition;
        }

        public void SetGCost(int gCost) => _gCost = gCost;
        public void SetHCost(int hCost) => _hCost = hCost;
        public void SetParent(Node parent) => _parent = parent;
        public int CompareTo(Node comparedNode)
        {
            int compare = FCost.CompareTo(comparedNode.FCost);
            if (compare == 0)
                compare = _hCost.CompareTo(comparedNode.HCost);
            return -compare;
        }

    }
}