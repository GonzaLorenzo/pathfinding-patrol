using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private List<Node> _neighbors = new List<Node>();
    //public bool blocked;
    //public int cost;

    public List<Node> GetNeighbors()
    {
        return _neighbors;
    }
}
