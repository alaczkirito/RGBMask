https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Texture2D.GetPixels32.html

Returns
Color32[] An array that contains the pixel colors.

Description
Gets the pixel color data for a mipmap level as Color32 structs.

This method gets pixel data from the texture in CPU memory. Texture.isReadable must be true.

The array contains the pixels row by row, starting at the bottom left of the texture. The size of the array is the width × height of the mipmap level.

Each pixel is a Color32 struct. GetPixels32 might be slower than some other texture methods because it converts the format the texture uses into Color32. To get pixel data more quickly, use GetPixelData instead.

A single call to GetPixels32 is usually faster than multiple calls to GetPixel, especially for large textures.

If GetPixels32 fails, Unity throws an exception. GetPixels32 might fail if the array contains too much data. Use GetPixelData or GetRawTextureData instead for very large textures. 

Code : 
``` c#
public class Texture2DExample : MonoBehaviour
{
    public Texture2D source;
    public Texture2D destination;

    void Start()
    {
        // Get a copy of the color data from the source Texture2D, in lower-precision int format.
        // Each element in the array represents the color data for an individual pixel.
        int sourceMipLevel = 0;
        Color32[] pixels = source.GetPixels32(sourceMipLevel);

        // If required, manipulate the pixels before applying them to the destination Texture2D.
        // This example code reverses the array, which rotates the image 180 degrees.
        System.Array.Reverse(pixels, 0, pixels.Length);

        // Set the pixels of the destination Texture2D.
        int destinationMipLevel = 0;
        destination.SetPixels32(pixels, destinationMipLevel);

        // Apply changes to the destination Texture2D, which uploads its data to the GPU.
        destination.Apply();
    }
}
```