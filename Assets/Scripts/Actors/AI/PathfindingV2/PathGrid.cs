using Sheldier.Common.Utilities;
using Sheldier.Constants;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Sheldier.Actors.Pathfinding
{
    public class PathGrid : MonoBehaviour
    {
        public int GridSizeX => _gridSizeX;
        public int GridSizeY => _gridSizeY;
        public NativeArray<PathNode> Grid => _grid;

        [SerializeField] private Vector2 gridWorldPosition;
        [SerializeField] private Vector2 gridWorldSize;
        [SerializeField] private float nodeRadius;

#if UNITY_EDITOR
        [SerializeField] private bool _showNodes;
#endif
        
        private NativeArray<PathNode> _grid;
        
        private float _nodeDiameter;
        
        private int _gridSizeX;
        private int _gridSizeY;

        public void Initialize()
        {
            _nodeDiameter = nodeRadius * 2.0f;
            _gridSizeX = Mathf.RoundToInt(gridWorldSize.x / _nodeDiameter);
            _gridSizeY = Mathf.RoundToInt(gridWorldSize.y / _nodeDiameter);

            CreateGrid();
        }
        public int2 WorldToGridPosition(Vector3 objectWorldPosition)
        {
            Vector2 objectPosition2D = objectWorldPosition.DiscardZ();
            Vector2 fromOriginToObject = objectPosition2D - gridWorldPosition;
            float percentX = fromOriginToObject.x / gridWorldSize.x;
            float percentY = fromOriginToObject.y / gridWorldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);

            return new int2(x,y);
        }

        public Vector2 GridToWorldPosition(int2 gridPosition)
        {
            return _grid[CalculateIndex(gridPosition.x, gridPosition.y)].WorldPosition;
        }
        private int CalculateIndex(int x, int y) => x + y * _gridSizeX;
        private void CreateGrid()
        {
            _grid = new NativeArray<PathNode>(_gridSizeX * _gridSizeY, Allocator.Persistent);
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    Vector3 worldPoint = gridWorldPosition + Vector2.right * (x * _nodeDiameter + nodeRadius) + Vector2.up * ( y * _nodeDiameter + nodeRadius);
                    bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, EnvironmentConstants.OBSTACLE_LAYER_MASK | EnvironmentConstants.PIT_LAYER_MASK));
                    
                    _grid[CalculateIndex(x,y)] = new PathNode
                    {
                        X = x, Y = y, Index =  CalculateIndex(x,y), GCost = int.MaxValue, HCost = int.MaxValue, FCost = int.MaxValue,
                        IsWalkable = walkable, CameFromNodeIndex = -1, WorldPosition = worldPoint
                    };
                }
            }
        }
        private void OnDrawGizmos()
        {
            Vector2 maxPoint = gridWorldPosition + gridWorldSize;
            Vector2 verticalLine = new Vector2(0.0f, maxPoint.y - gridWorldPosition.y);
            Gizmos.DrawLine(gridWorldPosition, gridWorldPosition + verticalLine);
            Vector2 horizontalLine = new Vector2(maxPoint.x - gridWorldPosition.x, 0.0f);
            Gizmos.DrawLine(gridWorldPosition + horizontalLine, gridWorldPosition);
            Gizmos.DrawLine(maxPoint, maxPoint - verticalLine);
            Gizmos.DrawLine(maxPoint, maxPoint - horizontalLine);
            
            if (_grid == null || !_showNodes) return;

            foreach (var node in _grid)
            {
                Gizmos.color = node.IsWalkable ? Color.green : Color.red;
                Gizmos.DrawCube(node.WorldPosition, Vector3.one*(_nodeDiameter-0.1f));
            }
            
        }

        public void Dispose()
        {
            _grid.Dispose();
        }
    }
}