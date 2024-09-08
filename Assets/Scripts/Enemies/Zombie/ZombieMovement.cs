using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour, IPhysicsObserver
{
    [SerializeField] private float rotationSpeed = 3f;

    //[SerializeField] private float groundDistance = 0.02f;
    [SerializeField] private float delay = 0.5f;

    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;

    private Transform player;

    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<Movement>().transform;

        if (player == null)
        {
            enabled = false;
        }
    }

    public void MoveTowards(Transform target)
    {
        if (navMeshAgent.enabled)
        {
            navMeshAgent.SetDestination(player.position);
            transform.forward = Vector3.Lerp(transform.forward, player.position - transform.position, rotationSpeed);
        }
    }

    public void Stop()
    {
        if (navMeshAgent.enabled)
        {
            navMeshAgent.ResetPath();
        }
    }

    public void OnHealthChanged(Vector3 attackerPosition, float kickForce)
    {
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

        navMeshAgent.enabled = true;
        rb.isKinematic = true;
    }

    //private IEnumerator RestoreNavMeshWhenGrounded()
    //{
    //    // Ожидание, пока объект не будет на земле
    //    while (!IsGrounded())
    //    {
    //        yield return null; // Продолжать ожидание
    //    }

    //    // Возвращает управление NavMeshAgent и делает Rigidbody кинематическим
    //    SwitchMode(true);
    //}

    //private bool IsGrounded()
    //{
    //    Debug.DrawRay(transform.position, Vector3.down * groundDistance);
    //    return Physics.CheckSphere(transform.position, groundDistance, groundMask);
    //}

    private void SwitchMode(bool mode)
    {
        rb.isKinematic = mode;
        navMeshAgent.enabled = mode;
    }
}
