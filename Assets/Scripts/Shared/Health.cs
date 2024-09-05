using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 20f;

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
        RegisterPhysicsObserver(GetComponent<Movement>());
        //RegisterUIObserver(this);
    }

    public void Attack(Transform entity, float damage, float kickForce)
    {
        int maxCount = Mathf.Max(physicsObservers.Count, uiObservers.Count);
        health -= damage;
        for (int i = 0; i < maxCount; i++)
        {
            if (i < physicsObservers.Count)
            {
                Debug.Log("PhysObs");
                physicsObservers[i].OnHealthChanged(entity, kickForce);
            }

            if (i < uiObservers.Count)
            {
                uiObservers[i].OnHealthChanged(health);
            }
        }
    }
}
