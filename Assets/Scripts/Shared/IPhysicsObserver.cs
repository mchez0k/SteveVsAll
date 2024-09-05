using UnityEngine;

public interface IPhysicsObserver
{
    /// <summary>
    /// ���������� ��������� ��� ����� �� ���������
    /// </summary>
    /// <param name="entityPosition">������� ��������, ������� �������</param>
    /// <param name="kickForce">����, � ������� �������� ����������</param>
    void OnHealthChanged(Vector3 entityPosition, float kickForce) { }
}