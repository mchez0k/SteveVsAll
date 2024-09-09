using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Projectile", order = 4)]
public class ProjectileSO : ScriptableObject
{
    public Sprite sprite;
    public float damage = 1f;
    public float kickForce = 1f;
}
