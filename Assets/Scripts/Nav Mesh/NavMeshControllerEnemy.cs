using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshControllerEnemy : MonoBehaviour
{
    public static Vector3 Target;
    private NavMeshAgent agent;
    private bool TrackPlayer = false;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (TrackPlayer)
        {
            animator.SetBool("IsChasing", true);
            agent.destination = GameObject.Find("Player").transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)      //if it hits the target
    {
        if (other.name == "Player")
        {
            TrackPlayer = true;

            GetComponent<PathController>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = true;
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