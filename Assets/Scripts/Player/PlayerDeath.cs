using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour, IDeathObserver
{
    private GameObject player;
    private bool firstDeath;

    void Awake()
    {
        player = FindObjectOfType<Movement>().gameObject; // TODO: Поменять
    }
    public void OnDeath(int coins, float experience)
    {
        if (firstDeath)
        {
            player.SetActive(false);
            StartCoroutine(RespawnPlayer());
        } else
        {
            EnemySpawner.mobsCount = 0;
            PlayerProgress.SaveProgress();
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3f);

        Respawn();
    }

    public void Respawn()
    {
        player.SetActive(true);
        player.GetComponent<Health>().Heal(5);
    }
}
