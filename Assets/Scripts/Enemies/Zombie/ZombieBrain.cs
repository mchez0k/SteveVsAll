using UnityEngine;

public class ZombieBrain : MonoBehaviour
{
    [SerializeField] private float visionAngle = 90f;
    [SerializeField] private float viewDistance = 20f;
    [SerializeField] private float hearDistance = 40f;

    [SerializeField] private float damage = 1f;
    [SerializeField] private float kickForce = 10f;

    [SerializeField] private Transform eyes;

    public int CoinsReward;
    public int ExpirienceReward;

    private MobMovement movement;

    private float cooldown = 0f;

    private Transform player;

    private void Awake()
    {
        movement = GetComponent<MobMovement>();

        player = FindObjectOfType<Movement>().transform;

        if (player == null)
        {
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
        Move();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (cooldown > 0f || other.gameObject.layer == LayerMask.NameToLayer("Enemy") || !other.gameObject.TryGetComponent(out Health playerHealth)) return;
        ApplyDamage(playerHealth);
    }

    private void OnCollisionStay(Collision other)
    {
        if (cooldown > 0f || other.gameObject.layer == LayerMask.NameToLayer("Enemy") || !other.gameObject.TryGetComponent(out Health playerHealth)) return;
        ApplyDamage(playerHealth);
    }

    private void Move()
    {
        if (Vision() || Hear())
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

    private bool Hear()
    {
        return Vector3.Distance(player.position, transform.position) < hearDistance;
    }

    void ApplyDamage(Health playerHealth)
    {
        playerHealth.TakeDamage(transform.position, damage, kickForce);
        cooldown = 1f;
    }
}
