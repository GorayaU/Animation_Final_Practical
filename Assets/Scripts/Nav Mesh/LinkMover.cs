using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// This script adds animation when you jump via navmesh link,
// and switches the camera to a cinematic view during the jump.
[RequireComponent(typeof(NavMeshAgent))]
public class LinkMover : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    [Header("Cameras")]
    public Camera normalCamera;
    public Camera cinematicCamera;

    IEnumerator Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;

        // Make sure we start in normal view
        SetCinematic(false);

        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                // Enter cinematic mode for the jump
                SetCinematic(true);

                // Do the jump
                yield return StartCoroutine(Parabola(agent, 2.0f, 1.0f));
                agent.CompleteOffMeshLink();

                // Exit cinematic mode after the jump
                SetCinematic(false);
            }

            yield return null;
        }
    }

    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;

        while (normalizedTime < 1.0f)
        {
            // Trigger jump animation
            animator.SetBool("Jumping", true);

            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        // Ensure we end exactly at the end position
        agent.transform.position = endPos;
        animator.SetBool("Jumping", false);
    }

    void Update()
    {
        // Safety: if somehow we're not on a link, make sure jump anim is off
        if (agent != null && !agent.isOnOffMeshLink)
        {
            animator.SetBool("Jumping", false);
        }
    }

    private void SetCinematic(bool enabled)
    {
        if (normalCamera != null)
            normalCamera.enabled = !enabled;

        if (cinematicCamera != null)
            cinematicCamera.enabled = enabled;
    }
}
