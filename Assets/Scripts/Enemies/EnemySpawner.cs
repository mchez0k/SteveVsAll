using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] mobsPrefabs;
    [SerializeField] private int maxMobs;

    [SerializeField] private float spawnerTime = 2f;
    private float currentTime;

    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float maxDistance = 15f;

    public static int mobsCount;

    private void FixedUpdate()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0 && mobsCount < maxMobs)
        {
            TrySpawnMob();
        }
    }

    public void TrySpawnMob()
    {
        Vector3 randomPlane = Random.onUnitSphere * Random.Range(minDistance, maxDistance);
        Vector3 randomOffset = new Vector3(randomPlane.x, 0f, randomPlane.z);
        if (RandomPoint(randomOffset, 2f, out Vector3 spawnPoint))
        {
            Instantiate(mobsPrefabs[Random.Range(0, mobsPrefabs.Length)], spawnPoint, Quaternion.identity);
            mobsCount++;
            currentTime = spawnerTime;
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 9; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas) && Vector3.Distance(transform.position, hit.position) < minDistance)
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
