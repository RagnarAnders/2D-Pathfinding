using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private Grid grid;
    private void Awake()
    {
        grid = GetComponent<Grid>();
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
                if(openSet[i].fCost() < currentNode.fCost() || openSet[i].fCost() == currentNode.fCost() && openSet[i].getHCost() < currentNode.getHCost())
                { 
                    currentNode = openSet[i];
                }  
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return;
            }

            foreach(Node neighbours in grid.GetNeighbours(currentNode))
            {
                if(!neighbours.IsWalkable() || closedSet.Contains(neighbours))
                {
                    continue;
                }
            }
        }
    }

    private int GetDinstance(Node nodeA, Node nodeB)
    {

    }
}
