using YG;

public static class PlayerProgress
{
    private static int currentCoins;
    private static float currentExpirience;
    private static int currentKills;

    public static void UpdateData(int coins, float exp)
    {
        currentCoins += coins;
        currentExpirience += exp;
        currentKills++;
        UnityEngine.Debug.Log("Обновились данные: " + currentCoins + " " + currentExpirience + " " + currentKills);
    }

    public static void DoubleCoins()
    {
        currentCoins *= 2;
    }

    public static void SaveProgress()
    {
        YandexGame.savesData.Coins += currentCoins;
        YandexGame.savesData.Expirience += currentExpirience;
        YandexGame.savesData.Kills += currentKills;

        UnityEngine.Debug.Log("Сохранение данных");
        YandexGame.SaveProgress();
    }

}
