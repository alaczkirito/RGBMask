``` c#
public int width  = 64;  //Width of canvas
public int height = 64; //Height of canvas
public Color32 brushColor = new Color32(0, 0, 0, 255); //black
public Texture2D maskTex; //The mask texture
public float reportInterval = 2f; //Console output interval 

private Texture2D  _tex;          // the texture displayed on screen  
private Color32[]  _buffer;       // CPU copy of every pixel colour  
private Color32[]  _maskPixels;   // mask sampled once at startup  
private bool[]     _painted;      // true if that pixel has been painted at least once  
private int        _paintedCount; // running total of painted pixels  
private bool       _dirty;        // true when buffer has changes not yet pushed to GPU  
  
private Camera         _cam;  
private SpriteRenderer _sr;
```



