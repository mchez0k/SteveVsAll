using UnityEngine;

[CreateAssetMenu(fileName = "ValueItem", menuName = "Inventory/ItemData")]
public class ValueItem : ScriptableObject
{
    public Sprite sprite;
    public int itemCount;

    public void IncrementItemCount()
    {
        itemCount++;
        Debug.Log("Поднят предмет " + name);
    }
}