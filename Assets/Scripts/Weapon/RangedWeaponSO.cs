public class RangedWeaponSO : WeaponSO, IRangedWeapon
{
    public float range = 10f;
    public int ammoCapacity = 30;
    public float reloadTime = 2f;

    public float Range => range;
    public int AmmoCapacity => ammoCapacity;
    public float ReloadTime => reloadTime;
}
