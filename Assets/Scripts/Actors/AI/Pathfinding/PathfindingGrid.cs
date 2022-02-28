using System.Collections.Generic;
using Sheldier.Common.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Actors.Pathfinding
{
    public class PathfindingGrid : SerializedMonoBehaviour
    {
        public int MaxSize => _gridSizeX * _gridSizeY;

        [SerializeField] private Vector2 gridWorldPosition;
        [SerializeField] private Vector2 gridWorldSize;
        [SerializeField] private float nodeRadius;
        [SerializeField] private LayerMask obstacleLayer;
        
        #if UNITY_EDITOR
        [SerializeField] private bool _showNodes;
        #endif
        
        private Node[,] _grid;
        
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

        private void CreateGrid()
        {
            _grid = new Node[_gridSizeX, _gridSizeY];
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    Vector3 worldPoint = gridWorldPosition + Vector2.right * (x * _nodeDiameter + nodeRadius) + Vector2.up * ( y * _nodeDiameter + nodeRadius);
                    bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, obstacleLayer));
                    _grid[x, y] = new Node(walkable, worldPoint, new Vector2Int(x,y));
                }
            }
        }

        public Node WorldToGridPosition(Vector3 objectWorldPosition)
        {
            Vector2 objectPosition2D = objectWorldPosition.DiscardZ();
            Vector2 fromOriginToObject = objectPosition2D - gridWorldPosition;
            float percentX = fromOriginToObject.x / gridWorldSize.x;
            float percentY = fromOriginToObject.y / gridWorldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);

            return _grid[x, y];
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if(x == 0 && y == 0)
                        continue;
                    Vector2Int checkPosition = node.GridPosition + new Vector2Int(x, y);

                    if (checkPosition.x >= 0 && checkPosition.x < _gridSizeX && checkPosition.y >= 0 &&
                        checkPosition.y < _gridSizeY)
                    {
                        neighbours.Add(_grid[checkPosition.x, checkPosition.y]);
                    }
                }
            }

            return neighbours;
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
    }
}
