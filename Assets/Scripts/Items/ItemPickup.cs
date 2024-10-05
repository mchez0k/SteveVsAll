using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ValueItem itemData;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.sprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ��������� ���������� ���������
            itemData.IncrementItemCount();
            // ���������� �������
            Destroy(gameObject);
        }
    }
}