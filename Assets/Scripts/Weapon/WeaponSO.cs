using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponSO : ScriptableObject
{
    public Sprite sprite;

    public float coolDown = 0.5f;
    public float damage = 1f;
    public float kickForce = 1f;
}
