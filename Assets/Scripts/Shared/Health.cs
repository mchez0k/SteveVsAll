using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 20f;
    private float maxHealth;

    [Space(2)]
    [Header("Награды")]
    public int Coins;
    public int Experience;

    private List<IPhysicsObserver> physicsObservers = new List<IPhysicsObserver>();
    private List<IUIObserver> uiObservers = new List<IUIObserver>();
    private IDeathObserver deathObserver;

    private SoundManager soundManager;

    public void RegisterPhysicsObserver(IPhysicsObserver observer)
    {
        physicsObservers.Add(observer);
    }

    public void RegisterUIObserver(IUIObserver observer)
    {
        uiObservers.Add(observer);
    }

    public void RegisterDeathObserver(IDeathObserver observer)
    {
        if (observer != null)
        {
            deathObserver = observer;
        }
        else
        {
            Debug.LogWarning("GameOverObserver не найден.");
        }
    }

    private void Awake()
    {
        maxHealth = health;

        soundManager = GetComponent<SoundManager>();

        if (TryGetComponent(out Movement movement)) RegisterPhysicsObserver(movement);

        if (TryGetComponent(out PlayerUI playerUI)) RegisterUIObserver(playerUI);

        if (TryGetComponent(out MobMovement mobMovement)) RegisterPhysicsObserver(mobMovement);

        if (TryGetComponent(out DamageBlink blink)) RegisterPhysicsObserver(blink);

        if (TryGetComponent(out MobDeath mob))
        {
            RegisterDeathObserver(mob);
        } 
        else
        {
            RegisterDeathObserver(FindAnyObjectByType<PlayerDeath>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Debug.Log(name + " попал в KillZone");
            deathObserver.OnDeath(Coins, Experience);
        }
    }

    public void TakeDamage(Vector3 entityPosition, float damage, float kickForce)
    {
        int maxCount = Mathf.Max(physicsObservers.Count, uiObservers.Count);
        health -= damage;
        soundManager.OnHurt();
        for (int i = 0; i < maxCount; i++)
        {
            if (i < physicsObservers.Count)
            {
                physicsObservers[i].OnHealthChanged(entityPosition, kickForce);
            }

            if (i < uiObservers.Count)
            {
                uiObservers[i].OnHealthChanged(health + damage, damage);
            }
        }
        if (health <= 0)
        {
            soundManager.OnDeath();
            StartCoroutine(KillAfterSound(Coins, Experience));
        }
    }

    private IEnumerator KillAfterSound(int Coins, float Experience)
    {
        yield return new WaitForSeconds(0.5f);
        deathObserver.OnDeath(Coins, Experience);
    }

    public void Heal(float heal)
    {
        health = Mathf.Min(maxHealth, health + heal);
        for (int i = 0; i < uiObservers.Count; i++)
        {
            uiObservers[i].OnHealthChanged(health - heal, - heal);
        }
    }
}
