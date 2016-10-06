using UnityEngine;
using System.Collections.Generic;

public class FindPathTest : MonoBehaviour {

    public bool isMoving;

    public float speed;

    public int star;
    public int dest;

    public Stack<GameObject> path;
    public GameObject activeNode;


	void Start ()
    {
        path = new Stack<GameObject>();
        GameObject[] nodes = GameObject.Find("Nodes").GetComponent<AllNodes>().NodeList;
        //GameObject s = nodes[star];
        //GameObject d = nodes[dest];
        //path = GetComponent<FindPath>().BreadthFirst(s,d);
        //activeNode = s;

        //isMoving = true;
    }

    public void BeginMovement(Stack<GameObject> stack, GameObject start)
    {
        path = stack;
        activeNode = start;
        isMoving = true; 
    }

    void Update()
    {
        if (isMoving)
        {
            float yVal = transform.position.y;//We want to retain the object's y value, so that only movement along the x and z axis is considered
            Vector3 moveDir = transform.position;
            Vector3 nodeLoc = activeNode.transform.position;
            nodeLoc.y = yVal;//We want to factor out the y value. This prevents the altitude of the AI from adjusting, as well as rubberbanding when close to nodes
            moveDir += (Vector3.Normalize(nodeLoc - transform.position))*Time.deltaTime*speed;
            transform.position = moveDir;
        }

        Vector3 nodeDist = activeNode.transform.position;//We want to consider the distance between the AI and the node, without considering the difference between their respective y values 
        nodeDist.y = transform.position.y;

        if (Vector3.Distance(transform.position, nodeDist) < 0.5f)
        {
            if (path.Count > 0)
            {
                activeNode = path.Pop();
            }
            else
            {
                isMoving = false;
            }
        }
    }
}
