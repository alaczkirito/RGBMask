using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class TestDraw : MonoBehaviour
{
    [Header("Canvas Size")]
    public int width  = 64;
    public int height = 64;

    [Header("Painting")]
    public Color32 brushColor = new Color32(0, 0, 0, 255);

    [Header("Mask")]
    // Greyscale texture — white = paintable, black = locked
    // Must match width x height of the canvas
    public Texture2D maskTex;

    [Header("Reporting")]
    public float reportInterval = 2f;

    // ── Private state ──────────────────────────────────────────────────────

    private Texture2D  _tex;          // the texture displayed on screen
    private Color32[]  _buffer;       // CPU copy of every pixel colour
    private Color32[]  _maskPixels;   // mask sampled once at startup
    private bool[]     _painted;      // true if that pixel has been painted at least once
    private int        _paintedCount; // running total of painted pixels
    private bool       _dirty;        // true when buffer has changes not yet pushed to GPU

    private Camera         _cam;
    private SpriteRenderer _sr;

    // ── Lifecycle ──────────────────────────────────────────────────────────

    void Awake()
    {
        _cam = Camera.main;
        _sr  = GetComponent<SpriteRenderer>();

        InitTexture();
        InitMask();

        StartCoroutine(ReportLoop());
    }

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

    // ── Setup ──────────────────────────────────────────────────────────────

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

    // ── Input ──────────────────────────────────────────────────────────────

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
        // For a 2D camera this is the difference along the Z axis
        float distToCanvas = Mathf.Abs(_cam.transform.position.z - transform.position.z);

        Vector3 worldPos = _cam.ScreenToWorldPoint(
            new Vector3(screenPos.x, screenPos.y, distToCanvas)
        );

        TryPaint(worldPos);
    }

    // ── Paint ──────────────────────────────────────────────────────────────

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

    // ── Reporting ──────────────────────────────────────────────────────────

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

    // Call this directly when you need the count immediately (e.g. on level end)
    public int GetPaintedCount() => _paintedCount;
}