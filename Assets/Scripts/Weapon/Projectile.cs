using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileSO data;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = data.sprite;
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Projectile"));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Skeleton") return;
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(transform.position, data.damage, data.kickForce);
            Destroy(gameObject);
        }
    }
}
