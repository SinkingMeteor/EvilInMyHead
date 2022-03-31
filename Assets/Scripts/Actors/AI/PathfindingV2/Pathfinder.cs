using System;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Sheldier.Actors.Pathfinding
{
    public class Pathfinder
    {
        private PathGrid _grid;
        private const int DIAGONAL_VALUE = 14;
        private const int CROSS_VALUE = 10;

        public void InitializeOnNewLocation(PathGrid grid)
        {
            _grid = grid;
        }

        public void FindPath(Vector3 startPosition, Vector3 endPosition, Action<Vector2[], bool> finishedProcessingPath)
        {

            int2 startIntPos = _grid.WorldToGridPosition(startPosition);
            int2 endIntPos = _grid.WorldToGridPosition(endPosition);
            NativeList<int2> paths = new NativeList<int2>(Allocator.TempJob);
            NativeArray<PathNode> nodes = new NativeArray<PathNode>(_grid.Grid, Allocator.TempJob);
            FindPathJob findPathJob = new FindPathJob();
            findPathJob.Path = paths;
            findPathJob.StartPosition = startIntPos;
            findPathJob.EndPosition = endIntPos;
            findPathJob.GridSize = new int2(_grid.GridSizeX, _grid.GridSizeY);
            findPathJob.PathNodes = nodes;
            var handle = findPathJob.Schedule();
            handle.Complete();

            int2[] reversedPaths = new int2[findPathJob.Path.Length];
            for (int i = 0; i < findPathJob.Path.Length; i++)
            {
                reversedPaths[i] = findPathJob.Path[findPathJob.Path.Length - 1 - i];
            }
            
            Vector2[] vectorPaths = new Vector2[reversedPaths.Length];
            for (int i = 0; i < reversedPaths.Length; i++)
                vectorPaths[i] = _grid.GridToWorldPosition(reversedPaths[i]);
            finishedProcessingPath.Invoke(vectorPaths, findPathJob.Path.Length > 0);
            
            nodes.Dispose();
            paths.Dispose();
        }
        
        [BurstCompile]
        private struct FindPathJob : IJob
        {
            
            [WriteOnly] public NativeList<int2> Path;
            public NativeArray<PathNode> PathNodes;
            public int2 StartPosition;
            public int2 EndPosition;
            public int2 GridSize;

            public void Execute()
            {
                for (int x = 0; x < GridSize.x; x++)
                for (int y = 0; y < GridSize.y; y++)
                {
                    PathNode node = PathNodes[CalculateIndex(x,y)];
                    node.GCost = int.MaxValue;
                    node.HCost = CalculateHCost(new int2(x, y), EndPosition);
                    node.CalculateFCost();

                    PathNodes[node.Index] = node;
                }

                NativeArray<int2> neighbourOffsetArray = new NativeArray<int2>(8, Allocator.Temp);
                neighbourOffsetArray[0] = new int2(-1, 0);
                neighbourOffsetArray[1] = new int2(-1, 1);
                neighbourOffsetArray[2] = new int2(0, 1);
                neighbourOffsetArray[3] = new int2(1, 1);
                neighbourOffsetArray[4] = new int2(1, 0);
                neighbourOffsetArray[5] = new int2(1, -1);
                neighbourOffsetArray[6] = new int2(0, -1);
                neighbourOffsetArray[7] = new int2(-1, -1);
                
                int endNodeIndex = CalculateIndex(EndPosition.x, EndPosition.y);
                PathNode startNode = PathNodes[CalculateIndex(StartPosition.x, StartPosition.y)];
                startNode.GCost = 0;
                startNode.CalculateFCost();
                PathNodes[startNode.Index] = startNode;


                NativeList<int> openList = new NativeList<int>(Allocator.Temp);
                NativeList<int> closedList = new NativeList<int>(Allocator.Temp);

                openList.Add(startNode.Index);

                while (openList.Length > 0)
                {
                    int currentNodeIndex = GetLowestFCostNodeIndex(openList);
                    PathNode currentNode = PathNodes[currentNodeIndex];
                    if (currentNode.Index == endNodeIndex)
                    {
                        break;
                    }

                    for (int i = 0; i < openList.Length; i++)
                    {
                        if (openList[i] == currentNodeIndex)
                        {
                            openList.RemoveAtSwapBack(i);
                            break;
                        }
                    }

                    closedList.Add(currentNodeIndex);

                    for (int i = 0; i < neighbourOffsetArray.Length; i++)
                    {
                        int2 neighbourOffset = neighbourOffsetArray[i];
                        int2 neighbourPosition =
                            new int2(currentNode.X + neighbourOffset.x, currentNode.Y + neighbourOffset.y);
                        if (!IsInsideGrid(neighbourPosition))
                            continue;
                        int neighbourIndex = CalculateIndex(neighbourPosition.x, neighbourPosition.y);
                        if (closedList.Contains(neighbourIndex))
                            continue;
                        PathNode neighbourNode = PathNodes[neighbourIndex];
                        if (!neighbourNode.IsWalkable)
                            continue;

                        int2 currentNodePosition = new int2(currentNode.X, currentNode.Y);
                        int tentativeGCost = currentNode.GCost + CalculateHCost(currentNodePosition, neighbourPosition);
                        if (tentativeGCost < neighbourNode.GCost)
                        {
                            neighbourNode.CameFromNodeIndex = currentNodeIndex;
                            neighbourNode.GCost = tentativeGCost;
                            neighbourNode.CalculateFCost();
                            PathNodes[neighbourIndex] = neighbourNode;

                            if (!openList.Contains(neighbourNode.Index))
                                openList.Add(neighbourNode.Index);
                        }
                    }
                }

                PathNode endNode = PathNodes[endNodeIndex];

                if(endNode.CameFromNodeIndex != -1)
                {
                    RetracePath(endNode);
                }

                openList.Dispose();
                closedList.Dispose();
                neighbourOffsetArray.Dispose();
            }
            private void RetracePath(PathNode endNode)
            {
                Path.Add(new int2(endNode.X, endNode.Y));
                PathNode currentNode = endNode;
                while (currentNode.CameFromNodeIndex != -1)
                {
                    PathNode cameFromNode = PathNodes[currentNode.CameFromNodeIndex];
                    Path.Add(new int2(cameFromNode.X, cameFromNode.Y));
                    currentNode = cameFromNode;
                }
            }
            private int CalculateIndex(int x, int y) => x + y * GridSize.x;

            private bool IsInsideGrid(int2 gridPosition)
            {
                return gridPosition.x >= 0 && gridPosition.y >= 0 && gridPosition.x < GridSize.x &&
                       gridPosition.y < GridSize.y;
            }

            private int CalculateHCost(int2 aPosition, int2 bPosition)
            {
                int distanceX = math.abs(aPosition.x - bPosition.x);
                int distanceY = math.abs(aPosition.y - bPosition.y);
                int remaining = math.abs(distanceX - distanceY);
                return DIAGONAL_VALUE * math.min(distanceX, distanceY) + CROSS_VALUE * remaining;
            }

            private int GetLowestFCostNodeIndex(NativeList<int> openList)
            {
                PathNode lowestCostPathNode = PathNodes[openList[0]];
                for (int i = 1; i < openList.Length; i++)
                {
                    PathNode testPathNode = PathNodes[openList[i]];
                    if (testPathNode.FCost < lowestCostPathNode.FCost) 
                        lowestCostPathNode = testPathNode;
                }

                return lowestCostPathNode.Index;
            }
        }

    }
}

