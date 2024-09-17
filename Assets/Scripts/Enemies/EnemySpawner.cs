using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] mobsPrefabs;
    [SerializeField] private float[] weights;
    [SerializeField] private int maxMobs;

    [SerializeField] private float spawnerTime = 2f;
    private float currentTime;

    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float maxDistance = 15f;

    public static int mobsCount;

    private void Start()
    {
        mobsCount = 0;
    }

    private void FixedUpdate()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0 && mobsCount < maxMobs)
        {
            TrySpawnMob();
        }
        if (spawnerTime < 1.5f) return;
        spawnerTime -= Time.deltaTime / 600;

    }

    public void TrySpawnMob()
    {
        Vector3 randomPlane = Random.onUnitSphere * Random.Range(minDistance, maxDistance);
        Vector3 randomOffset = new Vector3(randomPlane.x, 0f, randomPlane.z);
        float totalWeight = weights.Sum();
        float randomValue = Random.Range(0, totalWeight);
        if (RandomPoint(randomOffset, 2f, out Vector3 spawnPoint))
        {
            float weightSum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                weightSum += weights[i];
                if (randomValue <= weightSum)
                {
                    Instantiate(mobsPrefabs[i], spawnPoint, Quaternion.identity);
                    mobsCount++;
                    currentTime = spawnerTime;
                    break;
                }
            }

        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        for (int i = 0; i < 9; i++)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas) && Vector3.Distance(transform.position, hit.position) >= minDistance)
            {
                result = hit.position;
                return true;
            }
            randomPoint = center + Random.insideUnitSphere * range;

        }
        result = Vector3.zero;
        return false;
    }
}
