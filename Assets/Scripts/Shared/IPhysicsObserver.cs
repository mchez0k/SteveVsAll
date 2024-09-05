using UnityEngine;

public interface IPhysicsObserver
{
    /// <summary>
    /// ���������� ��������� ��� ����� �� ���������
    /// </summary>
    /// <param name="entity">��������, ������� ������� ���������</param>
    /// <param name="kickForce">����, � ������� �������� ����������</param>
    void OnHealthChanged(Transform entity, float kickForce) { }
}