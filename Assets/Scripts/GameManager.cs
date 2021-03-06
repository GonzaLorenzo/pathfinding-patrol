using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Node startingNode;
    public Node goalNode;
    public Pathfinding _pf;
    private float debugTime = 1;
    private Vector2 closeNodes;
    private Node closestNode;
    private bool first = true;
    public LayerMask obstacleMask;

    public List<Node> nodes = new List<Node>();
    public List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        _pf = new Pathfinding();
    }

    public void AddEnemies(Enemy enemy)
    {
        enemies.Add(enemy);
        Debug.Log("Enemy added");
    }

    public void AddNodes(Node node)
    {
        nodes.Add(node);
        Debug.Log("Node added");
    }

    public Node GetStartNode(Transform position) 
    {
        foreach(Node node in GameManager.instance.nodes)
        {           
            Vector2 dir = node.transform.position - position.transform.position;
            
            RaycastHit2D hit = Physics2D.Raycast(position.transform.position, dir, dir.magnitude, obstacleMask);

            if(first)
            {
                if(hit == true)
                {

                }
                else
                {
                    closeNodes = dir;
                    closestNode = node;
                    first = false;
                }
            }
            if(dir.magnitude < closeNodes.magnitude)
            {
                if(hit == true)
                {

                }
                else
                {
                    closeNodes = dir;
                    closestNode = node;
                }       
            }
        }

        first = true;
        return closestNode; 
    }

    public Node GetEndNode(Transform position) 
    {
        foreach(Node node in GameManager.instance.nodes)
        {           
            Vector2 dir = node.transform.position - position.transform.position;


            if(first)
            {
                closeNodes = dir;
                closestNode = node;
                first = false;
            }
            if(dir.magnitude < closeNodes.magnitude)
            {
                closeNodes = dir;
                closestNode = node;
            }
        }

        first = true;
        return closestNode; 
    }
}
