using UnityEngine;
using UnityEngine.InputSystem;

public class LightSourceTap : MonoBehaviour
{
    [SerializeField] private LightRayController lightRayController;

    private static Transform firstSource;
    private static LightSourceTap firstTap;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Vector3 originalScale;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        // 最初の大きさを保存
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Mouse.current == null)
            return;

        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Vector2 mousePosition =
            Mouse.current.position.ReadValue();

        Vector3 worldPosition =
            Camera.main.ScreenToWorldPoint(mousePosition);

        worldPosition.z = 0;

        Collider2D hit =
            Physics2D.OverlapPoint(worldPosition);

        if (hit == null)
            return;

        if (hit.gameObject != gameObject)
            return;


        // =====================================
        // 選択中の光源をもう一度クリック
        // =====================================
        if (firstSource == transform)
        {
            SetSelected(false);

            firstSource = null;
            firstTap = null;

            // 光を消す
            lightRayController.ClearConnection();

            Debug.Log("光源の選択を解除");

            return;
        }


        // =====================================
        // 1個目の光源
        // =====================================
        if (firstSource == null)
        {
            firstSource = transform;
            firstTap = this;

            SetSelected(true);

            Debug.Log("1個目の光源を選択");

            return;
        }


        // =====================================
        // 2個目の光源
        // =====================================
        lightRayController.Connect(
            firstSource,
            transform
        );

        Debug.Log("2個の光源を接続");

        // 選択状態を解除
        firstTap.SetSelected(false);
        SetSelected(false);

        firstSource = null;
        firstTap = null;
    }


    // =====================================
    // 選択状態の見た目
    // =====================================
    private void SetSelected(bool selected)
    {
        if (spriteRenderer == null)
            return;

        if (selected)
        {
            // 選択中：5%だけ大きくする
            spriteRenderer.color = Color.white;
            transform.localScale = originalScale * 1.05f;
        }
        else
        {
            // 元の状態に完全に戻す
            spriteRenderer.color = originalColor;
            transform.localScale = originalScale;
        }
    }
}