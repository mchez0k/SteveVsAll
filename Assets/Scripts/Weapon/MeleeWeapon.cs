using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private MeleeWeaponSO data;

    private float currentCoolDown;

    private void Awake()
    {
        var render = GetComponent<SpriteRenderer>();
        render.sprite = data.sprite;
    }

    public override void Attack()
    {
        if (currentCoolDown > 0) return;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, data.attackRange, LayerMask.GetMask("Enemy"));
        if (hitColliders.Length < 1) return;
        for (int i = 0; i < Mathf.Min(hitColliders.Length, data.maxEnemies); ++i)
        {
            var enemyHealth = hitColliders[i].GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(transform.position, data.damage, data.kickForce);
            }
        }
        currentCoolDown = data.coolDown;
    }

    public override void DecreaseCooldown()
    {
        currentCoolDown -= Time.deltaTime;
    }
}

