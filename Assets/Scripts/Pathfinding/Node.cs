using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private HashSet<Node> _neighbors = new HashSet<Node>();
    private Vector2 closeNodes;
    private Node closestNode;
    private bool first = true;
    //public bool blocked;
    //public int cost;

    private void Start()
    {
        GameManager.instance.AddNodes(this);
    }
    public HashSet<Node> GetNeighbors()
    {
        foreach(Node node in GameManager.instance.nodes)
        {
            Vector2 dir = node.transform.position - transform.position;
            Debug.Log("foreach " + dir + node);

            if(first)
            {
                closeNodes = dir;
                closestNode = node;
                first = false;
            }
            //else if(dir.x < closeNodes.x && dir.y < closeNodes.y && dir.x > 0 && dir.y > 0)
            else if(dir.magnitude < closeNodes.magnitude && dir.magnitude > 0.2)
            {
                closeNodes = dir;
                closestNode = node;
                Debug.Log("a ver " + closeNodes + closestNode);
            }
        }

        if(!_neighbors.Contains(closestNode))
        {
            _neighbors.Add(closestNode);
        }

        
        Debug.Log("Aca es? + " + closestNode);
        first = true;
        return _neighbors;
    }
}
