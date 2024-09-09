using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI coins;
    [SerializeField] private AudioSource audioSource;
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

    public void OpenTG()
    {
        audioSource.Play();
        Application.OpenURL("https://t.me/jawbreaker_ofc");
    }
}
