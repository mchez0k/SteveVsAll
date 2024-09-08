using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "ScriptableObjects/MeleeWeapon", order = 1)]
public class MeleeWeaponSO : ScriptableObject
{
    public Sprite sprite;

    public float attackRange = 1.5f;
    public int maxEnemies = 1;

    public float damage = 1f;
    public float kickForce = 1f;

    public float coolDown = 0.5f;
}
