using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sheldier.Actors.Pathfinding
{
    public class PathSeeker
    {
        private PathfindingGrid _grid;
        
        private const int DIAGONAL_COST = 14;
        private const int CROSS_COST = 10;

        public void InitializeOnScene(PathfindingGrid grid)
        {
            _grid = grid;
        }
        public void StartFindPath(Vector2 pathStart, Vector2 pathEnd, Action<Vector2[], bool> onFinished)
        {
            _grid.StartCoroutine(FindPathCoroutine(pathStart, pathEnd, onFinished));
        }
        private IEnumerator FindPathCoroutine(Vector3 startPos, Vector3 targetPos, Action<Vector2[], bool> onFinished)
        {
            Vector2[] wayPoints = new Vector2[0];
            bool isSuccess = false;
            
            NodeHeap<Node> opeNodes = new NodeHeap<Node>(_grid.MaxSize);
            HashSet<Node> closedNodes = new HashSet<Node>();
            
            Node startNode = _grid.WorldToGridPosition(startPos);
            Node targetNode = _grid.WorldToGridPosition(targetPos);

            if (!startNode.IsWalkable || !targetNode.IsWalkable)
            {
                onFinished(wayPoints, isSuccess);
                yield break;
            }
            
            opeNodes.Add(startNode);

            while (opeNodes.Count > 0)
            {
                Node currentNode = opeNodes.RemoveFirstItem();

                closedNodes.Add(currentNode);
                if (currentNode == targetNode)
                {
                    isSuccess = true;
                    break;
                }

                foreach (var neighbour in _grid.GetNeighbours(currentNode))
                {
                    if(!neighbour.IsWalkable || closedNodes.Contains(neighbour))
                        continue;
                    int newCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);
                    if (newCostToNeighbour < neighbour.GCost || !opeNodes.Contains(neighbour))
                    {
                        neighbour.SetGCost(newCostToNeighbour);
                        neighbour.SetHCost(GetDistance(neighbour, targetNode));
                        neighbour.SetParent(currentNode);
                        
                        if(!opeNodes.Contains(neighbour))
                            opeNodes.Add(neighbour);
                    }
                }
            }

            yield return null;
            if (isSuccess)
            {
                wayPoints = RetracePath(startNode, targetNode);
            }

            onFinished(wayPoints, isSuccess);
        }

        private Vector2[] RetracePath(Node startNode, Node targetNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = targetNode;
            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            Vector2[] waypoints = SimplifyPath(path);
            Array.Reverse(waypoints);
            return waypoints;
        }

        private Vector2[] SimplifyPath(List<Node> path)
        {
            List<Vector2> waypoints = new List<Vector2>();
            Vector2 directionOld = Vector2.zero;

            for (int i = 1; i < path.Count; i++)
            {
                Vector2 directionNew = path[i - 1].GridPosition - path[i].GridPosition;
                if (directionNew != directionOld)
                {
                    waypoints.Add(path[i].WorldPosition);
                }

                directionOld = directionNew;
            }

            return waypoints.ToArray();
        }
        
        private int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.GridPosition.x - nodeB.GridPosition.x);
            int dstY = Mathf.Abs(nodeA.GridPosition.y - nodeB.GridPosition.y);

            if (dstX > dstY)
                return DIAGONAL_COST * dstY + CROSS_COST * (dstX - dstY);
            return DIAGONAL_COST * dstX + CROSS_COST * (dstY - dstX);
        }
    }
}