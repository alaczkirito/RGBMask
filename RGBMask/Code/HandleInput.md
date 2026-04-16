``` c#
void HandleInput()  
{  
    // Guard: input device not ready yet (common on first frame)  
    if (Mouse.current == null) return;  
  
    if (!Mouse.current.leftButton.isPressed) return;  
  
    Vector2 screenPos = Mouse.current.position.ReadValue();  
  
    // Guard: NaN or zero screen position means the mouse hasn't reported yet  
    if (float.IsNaN(screenPos.x) || float.IsNaN(screenPos.y)) return;  
    if (screenPos == Vector2.zero) return;  
  
    // ScreenToWorldPoint needs a Z = distance from camera to the canvas plane  
    // For a 2D camera this is the difference along the Z axis    float distToCanvas = Mathf.Abs(_cam.transform.position.z - transform.position.z);  
  
    Vector3 worldPos = _cam.ScreenToWorldPoint(  
        new Vector3(screenPos.x, screenPos.y, distToCanvas)  
    );  
  
    TryPaint(worldPos);  
}
```