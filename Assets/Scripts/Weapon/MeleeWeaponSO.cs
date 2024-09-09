using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "ScriptableObjects/MeleeWeapon", order = 2)]
public class MeleeWeaponSO : WeaponSO, IMeleeWeapon
{
    public float attackRange = 1.5f;
    public int maxEnemies = 1;
    public float kickForce = 1f;

    public float AttackRange => attackRange;
    public int MaxEnemies => maxEnemies;
    public float KickForce => kickForce;
}
