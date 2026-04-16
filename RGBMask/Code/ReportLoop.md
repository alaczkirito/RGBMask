``` c#
IEnumerator ReportLoop()  
{  
    var wait = new WaitForSeconds(reportInterval);  
    while (true)  
    {  
        yield return wait;  
        // Replace Debug.Log with however your game consumes this data  
        Debug.Log($"[PixelCanvas] Painted: {_paintedCount} / {width * height} pixels");  
    }  
}
```