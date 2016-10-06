using UnityEngine;
using System.Collections.Generic;

public class FindPath : MonoBehaviour {

    class nodeData//Temporary data 
    {
        public int name; //the index of the node within the nodes[]
        public float distanceFromS = -1;//The default value functions equivalently to infinity
        public GameObject path;//allows us to construct the path backwards when the algorithm is finished
    }

    public Stack<GameObject> BreadthFirst(GameObject s, GameObject d)
    {
        GameObject[] nodes = GameObject.Find("Nodes").GetComponent<AllNodes>().NodeList;
        int nodeCount = GameObject.Find("Nodes").GetComponent<AllNodes>().size;

        nodeData[] data = new nodeData[nodeCount];//We create an additional temporary array to store data that we do not want to persist globally 
        for (int i = 0; i < nodeCount; i++)
        {
            data[i] = new global::FindPath.nodeData();
            data[i].name = i;
        }

        Queue<nodeData> q = new Queue<nodeData>();

        data[s.GetComponent<AdjacencyMatrix>().index].distanceFromS = 0;

        q.Enqueue(data[s.GetComponent<AdjacencyMatrix>().index]);

        while (q.Count > 0)//ensures that we stop the dequeue once we are considering nodes further away than the current distance we are considering
        {
            nodeData i = q.Dequeue();

            foreach (AdjacencyMatrix.vertex j in nodes[i.name].GetComponent<AdjacencyMatrix>().aMatrix)
            {

                if (data[j.node.GetComponent<AdjacencyMatrix>().index].distanceFromS == -1 || data[j.node.GetComponent<AdjacencyMatrix>().index].distanceFromS > i.distanceFromS + j.weight)//if the node has not yet been processed OR if the distance is greater than what the current processing would result in
                {
                    data[j.node.GetComponent<AdjacencyMatrix>().index].distanceFromS = i.distanceFromS + j.weight;
                    data[j.node.GetComponent<AdjacencyMatrix>().index].path = nodes[i.name];
                    q.Enqueue(data[j.node.GetComponent<AdjacencyMatrix>().index]);
                }
            }
        }

        Stack<GameObject> tempStack = new Stack<GameObject>();//Here we simulate recursion with a stack
        GameObject check = d;
        while (check != s)//we add to the stack until we reach our starting point 
        {
            tempStack.Push(check);
            check = data[check.GetComponent<AdjacencyMatrix>().index].path;
        }

        return tempStack;
    }
}
