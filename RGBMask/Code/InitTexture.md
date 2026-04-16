``` c#
void InitTexture()  
{  
    // FilterMode.Point keeps pixels crisp with no blending between them  
    _tex = new Texture2D(width, height, TextureFormat.RGBA32, false)  
    {  
        filterMode = FilterMode.Point  
    };  
  
    // Start fully transparent  
    _buffer  = new Color32[width * height];  
    _painted = new bool[width * height];  
  
    for (int i = 0; i < _buffer.Length; i++)  
        _buffer[i] = new Color32(0, 0, 0, 0);  
  
    _tex.SetPixels32(_buffer);  
    _tex.Apply(false);  
  
    // Attach texture to sprite renderer  
    // pixelsPerUnit = 1 means 1 pixel = 1 world unit; adjust to fit your scene 
    _sr.sprite = Sprite.Create(  
        _tex,  
        new Rect(0, 0, width, height),  
        new Vector2(0.5f, 0.5f),  
        1f  
    );  
}
```
![[InitTextureExplain]]