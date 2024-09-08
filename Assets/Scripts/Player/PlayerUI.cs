using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour, IUIObserver
{
    [SerializeField] private GridLayoutGroup Hearts;

    [SerializeField] private Image[] hearts;

    [Space(2)]
    [Header("Ссылки на спрайты")]
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite fullHeartBlink;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite halfHeartBlink;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite emptyHeartBlink;

    void Awake()
    {
        if (hearts == null || hearts.Length == 0)
        {
            hearts = Hearts.GetComponentsInChildren<Image>();
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            SetHeartSprite(i, fullHeart); // Адрес полный сердце
        }

    }

    public void OnHealthChanged(float oldHealth, float damage)
    {
        UpdateHearts(oldHealth, damage);

        StartCoroutine(StartCoroutineWithDelay(0.1f, oldHealth, damage));
    }

    private IEnumerator StartCoroutineWithDelay(float delay, float oldHealth, float damage)
    {
        yield return new WaitForSeconds(delay);

        yield return StartCoroutine(UpdateHeartSpritesWithBorder(oldHealth, damage));
    }

    private IEnumerator UpdateHeartSpritesWithBorder(float oldHealth, float damage)
    {
        // Обновляем все спрайты на версии с обводкой
        float totalHealthPoints = Mathf.Max(0, oldHealth - damage);
        int fullHearts = (int)totalHealthPoints / 2;
        bool hasHalfHeart = (totalHealthPoints % 2 != 0);

        int i = 0;

        for (; i < fullHearts; ++i)
        {
            SetHeartSprite(i, fullHeartBlink);
        }

        if (hasHalfHeart && i < hearts.Length)
        {
            SetHeartSprite(i, halfHeartBlink);
            ++i;
        }

        for (; i < hearts.Length; ++i)
        {
            SetHeartSprite(i, emptyHeartBlink);
        }

        yield return new WaitForSeconds(0.1f);

        UpdateHearts(oldHealth, damage);
    }

    void UpdateHearts(float oldHealth, float damage)
    {
        float totalHealthPoints = Mathf.Max(0, oldHealth - damage);
        int fullHearts = (int)totalHealthPoints / 2;
        bool hasHalfHeart = (totalHealthPoints % 2 != 0);

        int i = 0;

        for (; i < fullHearts; ++i)
        {
            SetHeartSprite(i, fullHeart);
        }

        if (hasHalfHeart && i < hearts.Length)
        {
            SetHeartSprite(i, halfHeart);
            ++i;
        }

        for (; i < hearts.Length; ++i)
        {
            SetHeartSprite(i, emptyHeart);
        }
    }


    private void SetHeartSprite(int index, Sprite sprite)
    {
        //if (sprite == null) return;
        hearts[index].sprite = sprite;
    }

}
