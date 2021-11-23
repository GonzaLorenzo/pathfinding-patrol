using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Node startingNode;
    public Node goalNode;
    public Pathfinding _pf;
    private float debugTime;
    private Vector2 closeNodes;
    private Node closestNode;
    private bool first = true;


    public List<Node> nodes = new List<Node>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        _pf = new Pathfinding();
    }

    void Update()
    {

    }

    public void SearchPath(Node startingNode, Node goalNode)
    {
        StartCoroutine(_pf.PaintAStar(startingNode, goalNode, debugTime));
        Debug.Log("Hubo exito");        
    }

    public void AddNodes(Node node)
    {
        nodes.Add(node);
        Debug.Log("Node added");
    }

    //public void SetNodes(Node s, Node g)
    //{
        //startingNode = s;
        //goalNode = g;
    //}

    public Node GetGoalNode(Transform position) //Vector2 position
    {
        foreach(Node node in GameManager.instance.nodes)
        {
            //Vector2 dir = node.transform.position - transform.position;
            //if(closeNodes == null || dir.x < closeNodes.x && dir.y < closeNodes.y)
            
            Vector2 dir = node.transform.position - position.transform.position;
            Debug.Log("foreach parte dos " + dir + node);

            if(first)
            {
                Debug.Log("Me saqué el firts");
                closeNodes = dir;
                closestNode = node;
                first = false;
            }
            //if(dir.x < closeNodes.x && dir.y < closeNodes.y)
            if(dir.magnitude < closeNodes.magnitude)
            {
                closeNodes = dir;
                closestNode = node;

                Debug.Log("Vamos por " + node);
            }
        }

        Debug.Log("tomá " + closestNode);
        first = true;
        return closestNode; 
    }

    public void PaintGameObjectColor(GameObject go, Color color)
    {
        if (go == null) return;
        go.GetComponent<Renderer>().material.color = color;
    }
}
