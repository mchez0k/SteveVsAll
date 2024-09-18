using UnityEngine;

public class MobDeath : MonoBehaviour, IDeathObserver
{
    public void OnDeath(int coins, float experience)
    {
        PlayerProgress.UpdateData(coins, experience);
        Debug.Log("���� ��� " + gameObject.name);
        EnemySpawner.mobsCount--;
        Destroy(gameObject);
    }
}
