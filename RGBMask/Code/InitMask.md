``` c#
void InitMask()  
{  
    if (maskTex == null)  
    {  
        // No mask assigned — treat entire canvas as paintable  
        _maskPixels = new Color32[width * height];  
        for (int i = 0; i < _maskPixels.Length; i++)  
            _maskPixels[i] = new Color32(255, 255, 255, 255);  
        return;  
    }  
  
    // Read mask into CPU array once — never touch the mask texture again at runtime  
    _maskPixels = maskTex.GetPixels32();  
}
```
![[InitMaskExplain]]