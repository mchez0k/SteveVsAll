using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Game : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI coins;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject steveModel;

    //[SerializeField] private GameObject[] menus;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private Transform itemGrid;

    [SerializeField] private WeaponSO hand;

    private void Awake()
    {
        PlayerProgress.SetHand(hand);
        for (int i = 0; i < itemGrid.childCount; i++)
        {
            var item = itemGrid.GetChild(i).GetComponent<ShopItem>();
            if (item.GetId() == YandexGame.savesData.currentWeaponId)
            {
                PlayerProgress.SetWeapon(item.GetWeapon(), item.GetId());
            }
        }
    }

    public void GameStart()
    {
        audioSource.Play();
        YandexGame.GameplayStart();
        SceneManager.LoadScene(1);
    }

    private void FixedUpdate()
    {
        coins.text = YandexGame.savesData.Coins.ToString();
    }

    public void OpenShop(GameObject shop)
    {
        audioSource.Play();
        steveModel.SetActive(false);
        mainMenu.SetActive(false);
        shop.SetActive(true);
        YandexGame.FullscreenShow();
    }

    public void CloseShop(GameObject shop)
    {
        audioSource.Play();
        shop.SetActive(false);
        mainMenu.SetActive(true);
        steveModel.SetActive(true);
        YandexGame.FullscreenShow();
    }

    public void OpenTG()
    {
        audioSource.Play();
        Application.OpenURL("https://t.me/jawbreaker_ofc");
    }
}
