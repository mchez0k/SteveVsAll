using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI coins;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject steveModel;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject shopMenu;

    [SerializeField] private WeaponSO hand;

    private void Awake()
    {
        PlayerProgress.SetHand(hand);
    }

    public void GameStart()
    {
        audioSource.Play();
        YG.YandexGame.GameplayStart();
        SceneManager.LoadScene(1);
    }

    private void FixedUpdate()
    {
        coins.text = YG.YandexGame.savesData.Coins.ToString();
    }

    public void OpenShop()
    {
        steveModel.SetActive(false);
        mainMenu.SetActive(false);
        shopMenu.SetActive(true);
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        mainMenu.SetActive(true);
        steveModel.SetActive(true);
    }

    public void OpenTG()
    {
        audioSource.Play();
        Application.OpenURL("https://t.me/jawbreaker_ofc");
    }
}
