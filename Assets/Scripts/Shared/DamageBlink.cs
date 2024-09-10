using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageBlink : MonoBehaviour, IPhysicsObserver
{
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float blinkDuration = 0.5f;

    [SerializeField] private Transform mesh;
    [SerializeField] private List<Material> originalMaterials = new List<Material>();

    private void Awake()
    {
        // ��������� ������������ ��������� ���� SkinnedMeshRenderer
        foreach (SkinnedMeshRenderer renderer in mesh.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            foreach (Material mat in renderer.materials)
            {
                originalMaterials.Add(new Material(mat));
            }
        }
    }

    public void OnHealthChanged(Vector3 entityPosition, float kickForce)
    {
        StartCoroutine(BlinkEffect());
    }

    private IEnumerator BlinkEffect()
    {
        // �������� ���� �� damageColor
        foreach (SkinnedMeshRenderer renderer in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            foreach (Material mat in renderer.materials)
            {
                mat.color = damageColor;
            }
        }

        // �������� �� ����� �������� �������
        yield return new WaitForSeconds(blinkDuration);

        // ���������� ������������ �����
        int index = 0;
        foreach (SkinnedMeshRenderer renderer in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            foreach (Material mat in renderer.materials)
            {
                mat.color = originalMaterials[index].color;
                index++;
            }
        }
    }
}