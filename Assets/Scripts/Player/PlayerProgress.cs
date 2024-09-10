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
        Debug.Log("���������� ������: " + currentCoins + " " + currentExpirience + " " + currentKills);
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
        Debug.Log("������ ������: " + currentWeapon.name);
        YandexGame.savesData.currentWeaponId = id;
        currentWeapon = newWeapon;
        Debug.Log("����� ������: " + newWeapon.name);
    }
    public static WeaponSO GetWeapon()
    {
        Debug.Log("�������� ������: " + currentWeapon.name);
        return currentWeapon;
    }

    public static void SaveProgress()
    {
        YandexGame.savesData.Coins += currentCoins;
        YandexGame.savesData.Expirience += currentExpirience;
        YandexGame.savesData.Kills += currentKills;

        Debug.Log("���������� ������");
        YandexGame.SaveProgress();
    }

}
