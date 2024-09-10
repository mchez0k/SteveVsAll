using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileSO data;

    private bool isOnGround = false;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = data.sprite;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Skeleton" || isOnGround) return;
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(transform.position, data.damage, data.kickForce);
            Destroy(gameObject, 5f);
        }
        transform.parent = collision.transform;
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<CapsuleCollider>());
        isOnGround = true;
    }
}
