using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class PlayerDeath : MonoBehaviour, IDeathObserver
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject reviveButton;
    [SerializeField] private GameObject doubleButton;
    [SerializeField] private TextMeshProUGUI kills;


    private GameObject player;

    void Awake()
    {
        player = FindObjectOfType<Movement>().gameObject;
    }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += AdsReward;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= AdsReward;
    }

    public void OnDeath(int coins, float experience)
    {
        Death();
    }

    void Death()
    {
        kills.text = PlayerProgress.GetKills().ToString();
        player.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Respawn()
    {
        player.transform.position = new Vector3(0f, 2f, 0f);

        reviveButton.SetActive(false);
        mainMenu.SetActive(false);
        player.SetActive(true);

        player.GetComponent<Health>().Heal(20);
    }

    public void DoubleCoins()
    {
        PlayerProgress.DoubleCoins();
        Exit();
    }

    public void Exit()
    {
        PlayerProgress.SaveProgress();
        YandexGame.FullscreenShow();
        SceneManager.LoadScene(0);
    }

    public void AdsReward(int id)
    {
        switch (id)
        {
            case 0:
                Respawn();
                break;
            case 1:
                DoubleCoins();
                break;
            default:
                break;

        }
    }

    public void WatchRespawnAd()
    {
        YandexGame.RewVideoShow(0);
    }

    public void WatchDoubleAd()
    {
        YandexGame.RewVideoShow(1);
    }
}
