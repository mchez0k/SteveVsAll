using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeapon", menuName = "ScriptableObjects/RangedWeapon", order = 3)]

public class RangedWeaponSO : WeaponSO, IRangedWeapon
{
    public float startSpeed = 10f;

    public float StartSpeed => startSpeed;
}
