using UnityEngine;

public class SkeletonBrain : MonoBehaviour
{
    [SerializeField] private float visionAngle = 90f;
    [SerializeField] private float viewDistance = 20f;
    [SerializeField] private float hearDistance = 40f;

    [SerializeField] private float shootDistance = 10f;

    [SerializeField] private Transform eyes;
    [SerializeField] private RangedWeapon bow; 


    public int CoinsReward;
    public int ExpirienceReward;

    private SkeletonMovement movement;
    private SoundManager soundManager;

    private float cooldown = 0f;

    private Transform player;

    private void Awake()
    {
        movement = GetComponent<SkeletonMovement>();
        soundManager = GetComponent<SoundManager>();

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
        Attack();
    }

    private void Move()
    {
        if ((Vision() || Hear()) && Vector3.Distance(transform.position, player.position) > shootDistance)
        {
            movement.MoveTowards(player.transform);
        }
        else
        {
            movement.Stop();
        }
    }

    private void Attack()
    {
        transform.LookAt(player.position);
        if (cooldown <= 0 && Vector3.Distance(transform.position, player.position) < shootDistance)
        {
            soundManager.OnAttack();
            bow.Attack();
            cooldown = bow.WeaponData.coolDown;
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
}
