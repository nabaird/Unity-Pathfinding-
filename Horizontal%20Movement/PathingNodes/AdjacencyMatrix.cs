using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdjacencyMatrix : MonoBehaviour {

    public class vertex
    {
        public GameObject node;
        public float weight;

        public vertex(GameObject n, float w)
        {
            node = n;
            weight = w; 
        }
    }

    public List<vertex> aMatrix;
    public GameObject[] allNodes;
    public List<GameObject> test;
    public int index; 

    public void GenerateMatrix()
    {
        allNodes = GameObject.Find("Nodes").GetComponent<AllNodes>().NodeList;
        aMatrix = new List<vertex>();

        foreach (GameObject node in allNodes)
        {
            float distance = Vector3.Distance(node.transform.position, transform.position);//the weight is the distance in unity between nodes
            if (node != this)
            {
                RaycastHit hit;//We use raycasting to see which nodes within view of other nodes
                if (Physics.Raycast(transform.position, Vector3.Normalize(node.transform.position - transform.position), out hit))
                {
                    if (hit.collider == node.GetComponent<Collider>())
                    {
                        aMatrix.Add(new vertex(node, distance));
                    }
                }
            }
        }

        test = new List<GameObject>();
        foreach (vertex v in aMatrix)
        {
            test.Add(v.node);
        }
    }

	
