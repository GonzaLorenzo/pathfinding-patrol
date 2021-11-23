using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Node startingNode;
    public Node goalNode;
    public Pathfinding _pf;
    public float debugTime;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        _pf = new Pathfinding();
    }

    public void SearchPath()
    {
        StartCoroutine(_pf.PaintAStar(startingNode, goalNode, debugTime));        
    }

    public void SetNodes()
    {
        
    }

    public void PaintGameObjectColor(GameObject go, Color color)
    {
        if (go == null) return;
        go.GetComponent<Renderer>().material.color = color;
    }
}
