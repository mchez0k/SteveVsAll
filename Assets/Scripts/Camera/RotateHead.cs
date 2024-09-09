using UnityEngine;

public class RotateHead : MonoBehaviour
{
    public RectTransform canvasRect;
    public Transform objectToRotate;
    public Camera uiCamera;

    public float range;

    void Update()
    {
        Vector2 screenPoint = Input.mousePosition;

        Ray ray = uiCamera.ScreenPointToRay(screenPoint);
        Plane plane = new Plane(canvasRect.forward, canvasRect.position);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 worldPoint = ray.GetPoint(distance);

            worldPoint.z += range;

            Debug.DrawRay(transform.position, worldPoint);

            objectToRotate.LookAt(worldPoint);

            objectToRotate.Rotate(0, 90, 0, Space.Self);
        }
    }
}