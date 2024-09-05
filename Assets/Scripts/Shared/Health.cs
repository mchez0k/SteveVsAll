using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 20f;
    public bool isPlayer;

    private List<IPhysicsObserver> physicsObservers = new List<IPhysicsObserver>();
    private List<IUIObserver> uiObservers = new List<IUIObserver>();

    public void RegisterPhysicsObserver(IPhysicsObserver observer)
    {
        physicsObservers.Add(observer);
    }

    public void RegisterUIObserver(IUIObserver observer)
    {
        uiObservers.Add(observer);
    }

    private void Awake()
    {
        if (isPlayer)
        {
            RegisterPhysicsObserver(GetComponent<Movement>());
        } else
        {
            RegisterPhysicsObserver(GetComponent<ZombieMovement>());
        }

        //RegisterUIObserver(this);
    }

    public void TakeDamage(Vector3 entityPosition, float damage, float kickForce)
    {
        int maxCount = Mathf.Max(physicsObservers.Count, uiObservers.Count);
        health -= damage;
        for (int i = 0; i < maxCount; i++)
        {
            if (i < physicsObservers.Count)
            {
                physicsObservers[i].OnHealthChanged(entityPosition, kickForce);
            }

            if (i < uiObservers.Count)
            {
                uiObservers[i].OnHealthChanged(health);
            }
        }
    }
}
