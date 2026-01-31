using UnityEngine;
using UnityEngine.InputSystem;

public class Eye : MonoBehaviour
{
    public Camera camera;
    
    void Update()
    {
        Vector2 mousePos = GetMouseWorldPos2D(camera);
        Vector2 targetDirection = mousePos - (Vector2)transform.position;
        float angle = (Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg) - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    Vector2 GetMouseWorldPos2D(Camera cam)
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector3 world = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));
        return world;
    }
}
