﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Vector2 closeNodes;
    private Node closestNode;
    public LayerMask obstacleMask;
    private bool first = true;
    public bool blocked;
    public int cost;

    private void Start()
    {
        GameManager.instance.AddNodes(this);
    }
    
    /* public List<Node> GetNeighbors(Node sn)
    {
        foreach(Node node in GameManager.instance.nodes)
        {
            Vector2 dir = node.transform.position - transform.position;
           
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dir.magnitude, obstacleMask);
            if(hit == true) continue;
            //Debug.Log("foreach " + dir + node);

            if(first)
            {
                closeNodes = dir;
                closestNode = node;
                first = false;
            }
            else if(dir.magnitude < closeNodes.magnitude && dir.magnitude > 0.5)
            {
                if(node != sn)
                {
                    if(!_neighbors.Contains(node))
                    {
                        closeNodes = dir;
                        closestNode = node;
                        Debug.Log("Selecciono " + closeNodes + closestNode);
                    }
                }
            }
        }      
        _neighbors.Add(closestNode);
        Debug.Log("Añadi " + closestNode);
       
        first = true;
        return _neighbors;
    } */

    public List<Node> GetNeighbours(Node cn)
    {
        List<Node> _neighbors = new List<Node>();

        foreach(Node node in GameManager.instance.nodes)
        {
            Vector2 dir = node.transform.position - cn.transform.position;
           
            RaycastHit2D hit = Physics2D.Raycast(cn.transform.position, dir, dir.magnitude, obstacleMask);
            if(hit == true)
            {
                continue;
            } 
            else
            {
                _neighbors.Add(node);
            }
        }
        return _neighbors;
    }
}
