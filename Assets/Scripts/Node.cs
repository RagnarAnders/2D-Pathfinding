using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    private bool walkable;
    private Vector3 worldPosition;
    private int gridX;
    private int gridY;

    private int gCost;
    private int hCost;
    private Node parent;
    private int heapIndex;
    public int HeapIndex {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public Vector3 GetWorldPosition()
    {
        return worldPosition;
    }
    public bool IsWalkable()
    {
        return walkable;
    }

    public int FCost()
    {
        return gCost + hCost;
    }
    public int GetHCost()
    {
        return hCost;
    }
    public int GetGridX()
    {
        return gridX;
    }
    public int GetGridY()
    {
        return gridY;
    }
    public int GetGCost()
    {
        return gCost;
    }
    public void SetGCost(int cost)
    {
        gCost = cost;
    }
    public void SetHCost(int cost)
    {
        hCost = cost;
    }
    public Node GetParent()
    {
        return parent;
    }
    public void SetParent(Node value)
    {
        parent = value; 
    }

    public int CompareTo(Node other)
    {
        int compare = FCost().CompareTo(other.FCost());
        if(compare == 0)
        {
            compare = hCost.CompareTo(other.hCost);
        }
        return -compare;
    }
}
