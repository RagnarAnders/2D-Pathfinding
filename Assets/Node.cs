using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private bool walkable;
    private Vector3 worldPosition;
    private int gridX;
    private int gridY;

    private int gCost;
    private int hCost;

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

    public int fCost()
    {
        return gCost + hCost;
    }
    public int getHCost()
    {
        return hCost;
    }
    public int getGridX()
    {
        return gridX;
    }
    public int getGridY()
    {
        return gridY;
    }
}
