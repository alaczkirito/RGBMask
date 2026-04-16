``` c#
void TryPaint(Vector2 worldPos)  
{  
    // Convert world position → local position → pixel coordinate  
    Vector2 local = transform.InverseTransformPoint(worldPos);  
  
    // Local space is centred at (0,0); offset by half canvas size to get pixel index  
    int px = Mathf.RoundToInt(local.x + width  * 0.5f);  
    int py = Mathf.RoundToInt(local.y + height * 0.5f);  
  
    // Discard clicks outside the canvas bounds  
    if (px < 0 || px >= width || py < 0 || py >= height) return;  
  
    int idx = py * width + px;  
  
    // Mask check — alpha < 128 means this pixel is locked, do not paint  
    if (_maskPixels[idx].a < 128) return;  
  
    // Write colour to CPU buffer (GPU upload happens once at end of Update)  
    _buffer[idx] = brushColor;  
  
    // Count each pixel only the first time it gets painted  
    if (!_painted[idx])  
    {  
        _painted[idx] = true;  
        _paintedCount++;  
    }  
  
    _dirty = true;  
	}
```