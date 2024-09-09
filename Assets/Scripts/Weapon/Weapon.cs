using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponSO WeaponData;
    public abstract void Attack();
}