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
        Debug.Log("Обновились данные:\nCoins: " + currentCoins + "\nExp: " + currentExpirience + "\nKills: " + currentKills);
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
        CharacterController car;
    }

    public static void SetWeapon(WeaponSO newWeapon, int id)
    {
        YandexGame.savesData.currentWeaponId = id;
        currentWeapon = newWeapon;
        YandexGame.SaveProgress();
        Debug.Log("Новое оружие: " + newWeapon.name);
    }

    public static void BuyWeapon(WeaponSO newWeapon, int id, int balance)
    {
        currentWeapon = newWeapon;
        YandexGame.savesData.Coins = balance;
        YandexGame.savesData.buyIds.Add(id);
        YandexGame.savesData.currentWeaponId = id;
        YandexGame.SaveProgress();
        Debug.Log("Новое оружие: " + newWeapon.name);
    }

    public static WeaponSO GetWeapon()
    {
        Debug.Log("Получаем оружие: " + currentWeapon.name);
        return currentWeapon;
    }

    public static void ClearCurrentStats()
    {
        currentCoins = 0;
        currentExpirience = 0;
        currentKills = 0;
    }

    public static void UpdateScoreBoards(int newKills)
    {
        YandexGame.NewLeaderboardScores("Kills", newKills);
    }

    public static void SaveProgress() //с проблемами при сохранении обрати внимание на этот метод
    {
        YandexGame.savesData.Coins += currentCoins;
        YandexGame.savesData.Expirience += currentExpirience;
        YandexGame.savesData.Kills += currentKills;

        UpdateScoreBoards(YandexGame.savesData.Kills);

        Debug.Log("Сохранение данных");
        ClearCurrentStats();

        YandexGame.SaveProgress();
    }

}
