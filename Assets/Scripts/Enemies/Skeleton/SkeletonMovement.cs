using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMovement : MonoBehaviour, IPhysicsObserver
{
    [SerializeField] private float rotationSpeed = 3f;

    [SerializeField] private float delay = 0.5f;

    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    private AnimationsManager animationsManager;

    private Transform player;

    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<Movement>().transform;
        animationsManager = GetComponent<AnimationsManager>();

        if (player == null)
        {
            enabled = false;
        }
    }

    public void MoveTowards(Transform target)
    {
        if (navMeshAgent != null && navMeshAgent.enabled)
        {
            ReturnToMesh();
            navMeshAgent.SetDestination(player.position);
            animationsManager.OnMove(navMeshAgent.velocity.magnitude);
            transform.forward = Vector3.Lerp(transform.forward, player.position - transform.position, rotationSpeed);
        }
    }

    public void Stop()
    {
        if (navMeshAgent != null && navMeshAgent.enabled)
        {
            ReturnToMesh();
            navMeshAgent.ResetPath();
            animationsManager.OnMove(navMeshAgent.velocity.magnitude);
        }
    }

    public void OnHealthChanged(Vector3 attackerPosition, float kickForce)
    {
        Stop();
        SwitchMode(false);
        var kickDirection = new Vector3(
            (transform.position.x - attackerPosition.x),
            1f,
            (transform.position.z - attackerPosition.z)
        ).normalized * kickForce;
        rb.AddForce(kickDirection, ForceMode.Impulse);
        StartCoroutine(RestoreNavMeshAgentAndRigidbody(delay));
    }

    private IEnumerator RestoreNavMeshAgentAndRigidbody(float delay)
    {
        yield return new WaitForSeconds(delay);

        SwitchMode(true);
    }


    private void ReturnToMesh()
    {
        if (!navMeshAgent.isOnNavMesh)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(navMeshAgent.transform.position, out hit, 5.0f, NavMesh.AllAreas))
            {
                navMeshAgent.Warp(hit.position);
            }
        }
    }
    private void SwitchMode(bool mode)
    {
        rb.isKinematic = mode;
        navMeshAgent.enabled = mode;
    }
}
