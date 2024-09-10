using UnityEngine;
using YG;

public static class PlayerProgress
{
    private static int currentCoins;
    private static float currentExpirience;
    private static int currentKills;

    private static WeaponSO currentWeapon;

    public static void UpdateData(int coins, float exp)
    {
        currentCoins += coins;
        currentExpirience += exp;
        currentKills++;
        Debug.Log("Обновились данные: " + currentCoins + " " + currentExpirience + " " + currentKills);
    }

    public static void DoubleCoins()
    {
        currentCoins *= 2;
    }

    public static int GetCoins()
    {
        return currentCoins;
    }

    public static int GetKills()
    {
        return currentKills;
    }

    public static void SetHand(WeaponSO hand)
    {
        if (currentWeapon != null) return;
        currentWeapon = hand;
    }

    public static void SetWeapon(WeaponSO newWeapon, int id)
    {
        Debug.Log("Старое оружие: " + currentWeapon.name);
        YandexGame.savesData.currentWeaponId = id;
        currentWeapon = newWeapon;
        Debug.Log("Новое оружие: " + newWeapon.name);
    }
    public static WeaponSO GetWeapon()
    {
        Debug.Log("Получаем оружие: " + currentWeapon.name);
        return currentWeapon;
    }

    public static void SaveProgress()
    {
        YandexGame.savesData.Coins += currentCoins;
        YandexGame.savesData.Expirience += currentExpirience;
        YandexGame.savesData.Kills += currentKills;

        Debug.Log("Сохранение данных");
        YandexGame.SaveProgress();
    }

}
