using UnityEngine;

public class MobDeath : MonoBehaviour, IDeathObserver
{
    [SerializeField] private GameObject dropPrefab;

    public void OnDeath(int coins, float experience)
    {
        PlayerProgress.UpdateData(coins, experience);
        Debug.Log("Убит моб " + gameObject.name);
        EnemySpawner.mobsCount--;

        //SpawnDrop();

        Destroy(gameObject);
    }

    private void SpawnDrop()
    {
        if (dropPrefab != null)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab для дропа не назначен!");
        }
    }
}