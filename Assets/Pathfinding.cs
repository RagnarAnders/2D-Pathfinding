using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private Grid grid;
    [SerializeField]private Transform seeker, target;
    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath(seeker.position, target.position);
    }

    private void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPosition(startPos);
        Node targetNode = grid.NodeFromWorldPosition(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for(int i = 1; i<openSet.Count; i++)
            {
                if(openSet[i].FCost() < currentNode.FCost() || openSet[i].FCost() == currentNode.FCost() && openSet[i].GetHCost() < currentNode.GetHCost())
                { 
                    currentNode = openSet[i];
                }  
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach(Node neighbour in grid.GetNeighbours(currentNode))
            {
                if(!neighbour.IsWalkable() || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.GetGCost() + GetDinstance(currentNode, neighbour);
                if(newMovementCostToNeighbour < neighbour.GetGCost() || !openSet.Contains(neighbour))
                {
                    neighbour.SetGCost(newMovementCostToNeighbour);
                    neighbour.SetHCost(GetDinstance(neighbour, targetNode));
                    neighbour.SetParent(currentNode);

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    private void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.GetParent();
        }

        path.Reverse();

        grid.SetPath(path);
    }

    private int GetDinstance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.GetGridX() - nodeB.GetGridX());
        int dstY = Mathf.Abs(nodeA.GetGridY() - nodeB.GetGridY());
        if(dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        else
        {
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}
