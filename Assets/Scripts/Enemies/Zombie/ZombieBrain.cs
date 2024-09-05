using UnityEngine;
using UnityEngine.AI;

public class ZombieBrain : MonoBehaviour
{
    [SerializeField] private float visionAngle = 90f;
    [SerializeField] private float viewDistance = 16f;

    [SerializeField] private float damage = 1f;
    [SerializeField] private float kickForce = 10f;

    [SerializeField] private Transform eyes;

    private ZombieMovement movement;

    private float cooldown = 0f;

    private Transform player;

    private void Awake()
    {
        movement = GetComponent<ZombieMovement>();

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
        Move();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (cooldown > 0f || !other.gameObject.TryGetComponent(out Health playerHealth)) return;
        ApplyDamage(playerHealth);
    }

    private void OnCollisionStay(Collision other)
    {
        if (cooldown > 0f || !other.gameObject.TryGetComponent(out Health playerHealth)) return;
        ApplyDamage(playerHealth);
    }

    private void Move()
    {
        if (Vision())
        {
            movement.MoveTowards(player.transform);

        }
        else
        {
            movement.Stop();
        }
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
        playerHealth.TakeDamage(transform.position, damage, kickForce);
        cooldown = 1f;
    }
}
