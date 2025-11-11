using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField]
    public PathManager pathManager;

    List<Waypoint> thePath;
    Waypoint target;

    public float MoveSpeed = 3f;
    public float RotateSpeed = 5f;

    public Animator animator;
    bool isWalking;

    int currentIndex = 0;

    void Start()
    {
        thePath = pathManager.GetPath();

        if (thePath != null && thePath.Count > 0)
        {
            currentIndex = 0;
            target = thePath[currentIndex];
        }

        isWalking = false;
        if (animator != null)
            animator.SetBool("IsWalking", isWalking);
            animator.SetBool("Idle", !isWalking);
    }

    void Update()
    {
        // Toggle walking with any key press (keep if you like this behavior)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isWalking = !isWalking;
            if (animator != null)
                animator.SetBool("IsWalking", isWalking);
                animator.SetBool("Idle", !isWalking);
        }

        if (!isWalking || target == null) return;

        RotateTowardsTarget();
        MoveTowardsTarget();
    }

    void RotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;

        Vector3 targetDir = (target.pos - transform.position);
        if (targetDir.sqrMagnitude < 0.0001f) return;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir.normalized, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void MoveTowardsTarget()
    {
        float stepSize = MoveSpeed * Time.deltaTime;
        
        transform.position = Vector3.MoveTowards(transform.position, target.pos, stepSize);

        float distanceToTarget = Vector3.Distance(transform.position, target.pos);
        if (distanceToTarget <= 0.05f)
        {
            AdvanceToNextWaypoint();
        }
    }

    void AdvanceToNextWaypoint()
    {
        if (thePath == null || thePath.Count == 0) return;

        currentIndex = (currentIndex + 1) % thePath.Count;
        target = thePath[currentIndex];
    }
}
