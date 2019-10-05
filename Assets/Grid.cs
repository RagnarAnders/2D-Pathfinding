﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private Node[,] grid;
    [SerializeField] private Vector2 gridWorldSize;
    [SerializeField] private float nodeRadius;
    [SerializeField] private LayerMask unwalkableMask;

    private int gridSizeX, gridSizeY;
    private float nodeDiameter;
    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        
        for(int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1f, gridWorldSize.y));

        if(grid != null)
        {
            foreach(Node n in grid)
            {
                Gizmos.color = n.IsWalkable() ? Color.white : Color.red;
                Gizmos.DrawCube(n.GetWorldPosition(), Vector3.one * (nodeDiameter -0.1f));
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float precentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float precentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        precentX = Mathf.Clamp01(precentX);
        precentY = Mathf.Clamp01(precentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * precentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * precentY);

        return grid[x, y]; 
    }
}