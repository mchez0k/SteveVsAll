using UnityEngine;

public interface IPhysicsObserver
{
    /// <summary>
    /// Физическое изменение при атаке на персонажа
    /// </summary>
    /// <param name="entity">Сущность, которая двигает персонажа</param>
    /// <param name="kickForce">Сила, с которой персонаж оттолкнётся</param>
    void OnHealthChanged(Transform entity, float kickForce) { }
}