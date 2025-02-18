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

    public void Init()
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
            PlayerProgress.SetWeapon(item, id);
        }
        else
        {
            ChangeText("�������!");
        }
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        priceButton.onClick.RemoveListener(Buy);
        shopItems.Remove(this);
    }

    public void Buy()
    {
        Debug.Log($"������� ������ {name} � �������� {balance}");
        if (balance < price || isPurchased) return;
        Debug.Log($"������� {name}!");
        balance -= price;

        isPurchased = true;

        PlayerProgress.BuyWeapon(item, id, balance);

        ChangeText("�������!");
        PlayerProgress.SaveProgress();
    }

    private void ChangeText(string text)
    {
        priceButtonText.fontSize = 36f;
        priceButtonText.text = text;
    }

    public void SelectWeapon()
    {
        Debug.Log("������� ������ ������");
        Buy();
        foreach (var shopItem in shopItems)
        {
            if (shopItem == null || !shopItem.isPurchased) continue;
            if (shopItem.id == id)
            {
                PlayerProgress.SetWeapon(item, id);
                shopItem.ChangeText("�������!");
            }
            else
            {
                shopItem.ChangeText("�������!");
            }
        }
        PlayerProgress.SaveProgress();
    }

    public int GetId()
    {
        return id;
    }

    public WeaponSO GetWeapon()
    {
        return item;
    }
}