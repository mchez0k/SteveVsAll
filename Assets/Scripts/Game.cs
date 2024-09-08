using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public void GameStart()
    {
        YG.YandexGame.GameplayStart();
        SceneManager.LoadScene(1);
    }

    public void OpenTG()
    {
        Application.OpenURL("https://t.me/jawbreaker_ofc");
    }
}
