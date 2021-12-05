using System.Collections;
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

    public List<Node> GetNeighbours(Node cn)
    {
        List<Node> _neighbors = new List<Node>();

        foreach(Node node in GameManager.instance.nodes)
        {
            Vector2 dir = node.transform.position - cn.transform.position;
           
            RaycastHit2D hit = Physics2D.Raycast(cn.transform.position, dir, dir.magnitude, obstacleMask);
            if(hit == true)
            {
                Debug.Log("Este chocó " + node + "ANalizando este ajajaj " + cn);
            } 
            else
            {
                Debug.Log("Este NO chocó " + node + "ANalizando este ajajaj " + cn);
                _neighbors.Add(node);
            }
        }
        return _neighbors;
    }
}
