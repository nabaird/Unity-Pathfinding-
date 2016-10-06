using UnityEngine;
using System.Collections.Generic;

public class AllNodes : MonoBehaviour {

    public GameObject[] NodeList;
    public int size;
    void Awake()
    {
       
        NodeList = GameObject.FindGameObjectsWithTag("Node");
       
        for (int i=0; i<size; i++)
        {
            NodeList[i].GetComponent<AdjacencyMatrix>().index = i;
            NodeList[i].GetComponent<AdjacencyMatrix>().GenerateMatrix();
        }
    }

}
