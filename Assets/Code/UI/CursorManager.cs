using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

    [SerializeField] RectTransform cursorGroup;

    [Header("Cursor embellishments")]
    [SerializeField] Image fireRing;
    [SerializeField] Image hitCross;

    //Reference
    Camera mainCamera;

    //Status
    Vector3 position;
    float fadeSpeed = 20f;

    //Property
    public Vector3 Position => position;

    void Awake()
    {
        //Reference
        instance = this;
        mainCamera = Camera.main;

        //Hide visibility of unnecessary elements
        SetCursorVisibility(false);
        SetImageAlpha(fireRing, 0f);
        SetImageAlpha(hitCross, 0f);
    }

    void Update()
    {
        //Make cursor follow mouse
        position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = -0.1f;
        cursorGroup.position = position;

        //Fade out embellishments
        FadeOutImageColor(fireRing);
        FadeOutImageColor(hitCross);
    }

    private void OnDestroy()
    {
        SetCursorVisibility(true);
    }

    #region Public methods
    public void SetCursorVisibility(bool isVisible) => Cursor.visible = isVisible;
    public void FlashFiringRing() => SetImageAlpha(fireRing, 1);
    public void FlashHitCross() => SetImageAlpha(hitCross, 1);
    #endregion

    #region Private methods
    void FadeOutImageColor (Image image)
    {
        //Fade out image alpha
        if (image.color.a > 0f)
        {
            Color c = image.color;
            c.a = Mathf.Lerp(c.a, 0f, fadeSpeed * Time.deltaTime);
            image.color = c;
        }
    }

    void SetImageAlpha (Image image, float alpha)
    {
        //Set image alpha
        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }
    #endregion
}
