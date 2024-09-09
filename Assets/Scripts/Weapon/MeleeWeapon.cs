using UnityEngine;

public class MeleeWeapon : Weapon
{
    private IMeleeWeapon meleeData;

    private void Awake()
    {
        meleeData = (IMeleeWeapon)WeaponData;
        GetComponent<SpriteRenderer>().sprite = WeaponData.sprite;
    }

    public override void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, meleeData.AttackRange, LayerMask.GetMask("Enemy"));
        if (hitColliders.Length < 1) return;
        for (int i = 0; i < Mathf.Min(hitColliders.Length, meleeData.MaxEnemies); ++i)
        {
            var enemyHealth = hitColliders[i].GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(transform.position, WeaponData.damage, meleeData.KickForce);
            }
        }
    }
}

