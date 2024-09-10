using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using YG;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private WeaponSO item;
    [SerializeField] private Image icon;
    [SerializeField] private Button priceButton;
    [SerializeField] private TextMeshProUGUI priceButtonText;

    [SerializeField] private int price;

    private int balance = 0;

    [SerializeField] private int id;
    [SerializeField] private bool isPurchased;

    private static List<ShopItem> shopItems = new List<ShopItem>();

    private void Awake()
    {
        shopItems.Add(this);
        name = item.name;

        isPurchased = YandexGame.savesData.buyIds.Contains(id);

        icon.sprite = item.sprite;
        balance = YandexGame.savesData.Coins;
        if (!isPurchased)
        {
            priceButton.onClick.AddListener(Buy);
            priceButtonText.text = price.ToString();
        }
        else if (id == YandexGame.savesData.currentWeaponId)
        {
            ChangeText("�������!");
        } else
        {
            ChangeText("�������!");

        }
    }

    private void OnDisable()
    {
        priceButton.onClick.RemoveListener(Buy);
        shopItems.Remove(this);  // ������� �� ������ ��� ���������� �������
    }

    public void Buy()
    {
        if (balance < price) return;
        Debug.Log($"������� {name}!");
        balance -= price;
        YandexGame.savesData.Coins = balance;
        YandexGame.savesData.buyIds.Add(id);
        YandexGame.savesData.currentWeaponId = id;
        isPurchased = true;

        ChangeText("�������!");

        SelectWeapon();

        YandexGame.SaveProgress();

    }

    private void ChangeText(string text)
    {
        priceButton.interactable = false;
        priceButtonText.fontSize = 36f;
        priceButtonText.text = text;
    }

    public void SelectWeapon()
    {
        foreach (var shopItem in shopItems)
        {
            Debug.Log("������ ��� " + shopItem.name + " � Id: " +  shopItem.id + " ��� �������: " + shopItem.isPurchased);
            if (shopItem == null || !shopItem.isPurchased) continue;
            if (shopItem.id == id)
            {
                shopItem.ChangeText("�������!");
                Debug.Log(shopItem.gameObject.name + " ������");
                PlayerProgress.SetWeapon(item, id);
            }
            else
            {
                shopItem.ChangeText("�������!");
            }
        }
    }
}