``` c#
void Awake()  
{  
    _cam = Camera.main;  
    _sr  = GetComponent<SpriteRenderer>();  
  
    InitTexture();  // 
    InitMask();  // 
  
    StartCoroutine(ReportLoop());  
}
```

[[InitTexture]]
[[InitMask]]
