using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public static Vector3 Target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    void Update()
    {

        agent.destination = Target;

    }

    private void OnTriggerEnter(Collider other)      //if it hits the target
    {
        if (other.name == "Target")
        {
            //edit here
        }
    }
        
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Target")
        {           
            //edit here
        }
    }

}
