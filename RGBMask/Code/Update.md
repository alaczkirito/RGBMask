``` c#
void Update()  
{  
    HandleInput();  
  
    // Only upload to GPU when something actually changed — one Apply() per frame max  
    if (_dirty)  
    {  
        _tex.SetPixels32(_buffer);  
        _tex.Apply(false); // false = skip mipmap recalc  
        _dirty = false;  
    }  
}
```