using UnityEngine;
using UnityEngine.AI;

public class ZombieBrain : MonoBehaviour
{
    [SerializeField] private float visionAngle = 90f;
    [SerializeField] private float viewDistance = 16f;

    [SerializeField] private float rotationSpeed = 3f;

    [SerializeField] private float damage = 1f;
    [SerializeField] private float kickForce = 10f;

    [SerializeField] private Transform eyes;

    private float cooldown = 0f;


    private NavMeshAgent navMeshAgent;
    private Transform player;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        player = FindObjectOfType<Movement>().transform;

        if (player == null)
        {
            enabled = false;
            return;
        }
    }

    private void FixedUpdate()
    {
        cooldown -= Time.deltaTime;
        if (Vision())
        {
            navMeshAgent.SetDestination(player.position);
            transform.forward = Vector3.Lerp(transform.forward, player.position - transform.position, rotationSpeed);
        }
        else
        {
            navMeshAgent.ResetPath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cooldown > 0f || !other.TryGetComponent(out Health playerHealth)) return;
        ApplyDamage(playerHealth);
    }

    private void OnTriggerStay(Collider other)
    {
        if (cooldown > 0f || !other.TryGetComponent(out Health playerHealth)) return;
        ApplyDamage(playerHealth);
    }

    private bool Vision()
    {
        var realAngle = Vector3.Angle(transform.forward, player.position - transform.position);
        var direction = player.position - transform.position;
        if (realAngle > visionAngle / 2 || direction.magnitude > viewDistance) return false;
        Debug.DrawRay(eyes.position, direction);
        if (Physics.Raycast(eyes.position, direction, out RaycastHit hit, viewDistance))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }
        return false;
    }

    void ApplyDamage(Health playerHealth)
    {
        playerHealth.Attack(transform, damage, kickForce);
        cooldown = 1f;
    }
}
