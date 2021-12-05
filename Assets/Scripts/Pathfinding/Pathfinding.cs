﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    public List<Node> ConstructPathAStar(Node startingNode, Node goalNode)
    {
        PriorityQueue frontier = new PriorityQueue();
        frontier.Put(startingNode, 0);
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        Dictionary<Node, int> costSoFar = new Dictionary<Node, int>();
        cameFrom.Add(startingNode, null);
        costSoFar.Add(startingNode, 0);


        while (frontier.Count() > 0)
        {
            Node current = frontier.Get();

            if (current == goalNode)
            {
                List<Node> path = new List<Node>();
                Node nodeToAdd = current;
                while (nodeToAdd != null)
                {
                    path.Add(nodeToAdd);
                    nodeToAdd = cameFrom[nodeToAdd];
                }
                return path;
            }

            foreach (Node next in current.GetNeighbours(current)) //Antes usaba starting node
            {
                int newCost = costSoFar[current] + next.cost;
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    if (costSoFar.ContainsKey(next))
                    {
                        costSoFar[next] = newCost;
                        cameFrom[next] = current;
                    }
                    else
                    {
                        cameFrom.Add(next, current);
                        costSoFar.Add(next, newCost);
                    }
                    float priority = newCost + Heuristic(next.transform.position, goalNode.transform.position);
                    frontier.Put(next, priority);

                }
            }
        }
        //Debug.Log("PATH somo s " + path.Count);
        return default; 
    }
    
    float Heuristic(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b);
        }

    void PaintNodeList(List<Node> list)
        {
            foreach (var item in list)
            {
                GameManager.instance.PaintGameObjectColor(item.gameObject, Color.cyan);
            }
        }
}
