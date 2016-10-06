using UnityEngine;
using System.Collections;

public class PathGoalTest : MonoBehaviour {
    GameObject player;
    GameObject[] nodes;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        nodes = GameObject.Find("Nodes").GetComponent<AllNodes>().NodeList;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire1"))
        {
            GameObject start = nodes[0];
            GameObject dest = nodes[0];

            foreach (GameObject node in nodes)
            {
                if (Vector3.Distance(node.transform.position, player.transform.position) < Vector3.Distance(dest.transform.position, player.transform.position))
                {
                    dest = node;
                }

            }

            foreach (GameObject node in nodes)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, Vector3.Normalize(node.transform.position-transform.position), out hit) && hit.collider == node.GetComponent<Collider>())
                {
                    if (Vector3.Distance(node.transform.position, dest.transform.position) < Vector3.Distance(start.transform.position, dest.transform.position))
                    {
                        start = node;
                    }
                }

            }
         
            
            GetComponent<FindPathTest>().BeginMovement(GetComponent<FindPath>().BreadthFirst(start, dest), start);
        }
	}
}
