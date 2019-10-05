using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private bool walkable;
    private Vector3 worldPosition;

    public Node(bool walkable, Vector3 worldPosition)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return worldPosition;
    }
    public bool IsWalkable()
    {
        return walkable;
    }
}
