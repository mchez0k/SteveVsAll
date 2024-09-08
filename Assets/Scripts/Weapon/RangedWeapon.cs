using UnityEngine;

public class RangedWeapon : Weapon
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;
    private float coolDown = 1f;

    public override void Attack()
    {
        Debug.Log("Perform ranged attack");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * projectileSpeed;
    }

    public override void DecreaseCooldown()
    {
        coolDown -= Time.deltaTime;
    }
}