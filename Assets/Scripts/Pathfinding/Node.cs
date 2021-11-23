using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private List<Node> _neighbors = new List<Node>();
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
    public List<Node> GetNeighbors(Node sn)
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
            //else if(dir.x < closeNodes.x && dir.y < closeNodes.y && dir.x > 0 && dir.y > 0)
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
    }
}
