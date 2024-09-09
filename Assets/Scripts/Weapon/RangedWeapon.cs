using UnityEngine;

public class RangedWeapon : Weapon
{
    private IRangedWeapon rangedData;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private void Awake()
    {
        rangedData = (IRangedWeapon)WeaponData;
        GetComponent<SpriteRenderer>().sprite = WeaponData.sprite;
    }

    public override void Attack()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody>()
            .AddForce(transform.root.forward * rangedData.StartSpeed, ForceMode.Impulse);
        Destroy(projectile, rangedData.StartSpeed);
    }

    public void Attack(Vector3 target)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody>()
            .AddForce(target - firePoint.position * rangedData.StartSpeed, ForceMode.Impulse);
        Destroy(projectile, rangedData.StartSpeed);
    }
}