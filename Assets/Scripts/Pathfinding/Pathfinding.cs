﻿using System.Collections;
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
                //path.Reverse();
                return path;
            }

            foreach (Node next in current.GetNeighbors())
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
        return default;
    }

    public IEnumerator PaintAStar(Node startingNode, Node goalNode, float time)
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
            GameManager.instance.PaintGameObjectColor(current.gameObject, Color.blue);
            yield return new WaitForSeconds(time);
            if (current == goalNode)
            {
                List<Node> path = new List<Node>();
                Node nodeToAdd = current;
                while (nodeToAdd != null)
                {
                    path.Add(nodeToAdd);
                    nodeToAdd = cameFrom[nodeToAdd];
                }
                PaintNodeList(path);
                yield break;
            }

            foreach (Node next in current.GetNeighbors())
            {
                if (next.blocked) continue;
                int newCost = costSoFar[current] + next.cost;
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    if (!costSoFar.ContainsKey(next))
                    {
                        cameFrom.Add(next, current);
                        costSoFar.Add(next, newCost);
                        GameManager.instance.PaintGameObjectColor(current.gameObject, Color.gray);
                    }
                    else
                    {   
                        costSoFar[next] = newCost;
                        cameFrom[next] = current;
                        GameManager.instance.PaintGameObjectColor(current.gameObject, Color.red);
                    }
                    float priority = newCost + Heuristic(next.transform.position, goalNode.transform.position);
                    frontier.Put(next, priority);

                }
            }
            GameManager.instance.PaintGameObjectColor(current.gameObject, Color.gray);
        }
    }

    public float Heuristic(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }

    void PaintNodeList(List<Node> list)
    {
        foreach (var item in list)
        {
            GameManager.instance.PaintGameObjectColor(item.gameObject, Color.cyan);
        }
    }
}