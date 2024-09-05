using UnityEngine;

public interface IPhysicsObserver
{
    /// <summary>
    /// Физическое изменение при атаке на персонажа
    /// </summary>
    /// <param name="entityPosition">Позиция сущности, которая ударила</param>
    /// <param name="kickForce">Сила, с которой персонаж оттолкнётся</param>
    void OnHealthChanged(Vector3 entityPosition, float kickForce) { }
}