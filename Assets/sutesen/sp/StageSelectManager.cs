
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem.UI;
#endif

public class StageSelectManager : MonoBehaviour
{
    [Header("背景")]
    [SerializeField] private Sprite backgroundSprite;

    [Header("4種類のクラゲ画像")]
    [SerializeField] private Sprite[] jellyfishSprites = new Sprite[4];

    [Header("移動先のステージ名")]
    [SerializeField]
    private string[] stageSceneNames =
    {
        "Stage1",
        "Stage2",
        "Stage3",
        "Stage4"
    };

    private Image[] jellyfishImages;
    private Outline[] outlines;

    private Font uiFont;

    private void Start()
    {
        uiFont = Resources.GetBuiltinResource<Font>("Arial.ttf");

        CreateEventSystem();
        CreateCanvas();
    }

    private void CreateEventSystem()
    {
        if (FindObjectOfType<EventSystem>() != null)
            return;

        GameObject eventObject = new GameObject("EventSystem");
        eventObject.AddComponent<EventSystem>();

#if ENABLE_INPUT_SYSTEM
        eventObject.AddComponent<InputSystemUIInputModule>();
#else
        eventObject.AddComponent<StandaloneInputModule>();
#endif
    }

    private void CreateCanvas()
    {
        GameObject canvasObject = new GameObject("StageSelectCanvas");

        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 10;

        CanvasScaler scaler =
            canvasObject.AddComponent<CanvasScaler>();

        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;

        canvasObject.AddComponent<GraphicRaycaster>();

        RectTransform canvasRect =
            canvasObject.GetComponent<RectTransform>();

        // 背景
        GameObject bg = CreateUIObject("Background", canvasRect);

        Image bgImage = bg.AddComponent<Image>();
        bgImage.sprite = backgroundSprite;
        bgImage.color = Color.white;
        bgImage.preserveAspect = false;
        bgImage.raycastTarget = false;

        StretchFull(bg.GetComponent<RectTransform>());

        // タイトル
        CreateText(
            "Title",
            "STAGE SELECT",
            canvasRect,
            new Vector2(0.5f, 1f),
            new Vector2(0f, -105f),
            new Vector2(700f, 100f),
            64,
            new Color(1f, 0.86f, 1f)
        );

        jellyfishImages = new Image[4];
        outlines = new Outline[4];

        // クラゲ4種類のボタン
        for (int i = 0; i < 4; i++)
        {
            CreateStageButton(canvasRect, i);
        }

        // 下部の説明
        CreateText(
            "Hint",
            "SELECT YOUR STAGE",
            canvasRect,
            new Vector2(0.5f, 0f),
            new Vector2(0f, 75f),
            new Vector2(600f, 60f),
            28,
            Color.white
        );

        SelectStage(0);
    }

    private void CreateStageButton(RectTransform parent, int index)
    {
        GameObject buttonObject =
            CreateUIObject("StageButton" + (index + 1), parent);

        RectTransform rect =
            buttonObject.GetComponent<RectTransform>();

        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);

        // 4個を横一列に配置
        rect.anchoredPosition =
            new Vector2((index - 1.5f) * 330f, 10f);

        rect.sizeDelta = new Vector2(240f, 300f);

        Image image = buttonObject.AddComponent<Image>();
        image.sprite = jellyfishSprites != null &&
                       index < jellyfishSprites.Length
                       ? jellyfishSprites[index] : null;

        image.preserveAspect = true;
        image.color = Color.white;

        // 画像が未設定でもボタンを押せるようにする
        if (image.sprite == null)
            image.color = new Color(0.35f, 0.45f, 0.75f, 0.8f);

        Button button = buttonObject.AddComponent<Button>();
        button.targetGraphic = image;
        button.transition = Selectable.Transition.None;

        int stageIndex = index;
        button.onClick.AddListener(
            () => SelectStage(stageIndex)
        );

        Outline outline = buttonObject.AddComponent<Outline>();
        outline.effectColor = new Color(1f, 0.55f, 0.95f, 1f);
        outline.effectDistance = new Vector2(7f, 7f);
        outline.enabled = false;

        jellyfishImages[index] = image;
        outlines[index] = outline;

        // ステージ番号
        CreateText(
            "StageNumber" + (index + 1),
            "STAGE " + (index + 1),
            rect,
            new Vector2(0.5f, 0f),
            new Vector2(0f, -15f),
            new Vector2(240f, 55f),
            30,
            Color.white
        );
    }

    private void SelectStage(int index)
    {
        for (int i = 0; i < outlines.Length; i++)
        {
            outlines[i].enabled = (i == index);

            if (jellyfishImages[i] != null)
            {
                jellyfishImages[i].color =
                    i == index ? Color.white :
                    new Color(0.75f, 0.8f, 1f, 0.8f);
            }
        }

        // 選択したステージへ移動
        if (stageSceneNames != null &&
            index < stageSceneNames.Length &&
            !string.IsNullOrEmpty(stageSceneNames[index]))
        {
            SceneManager.LoadScene(stageSceneNames[index]);
        }
        else
        {
            Debug.LogWarning(
                "ステージ " + (index + 1) +
                " のシーン名を設定してね！"
            );
        }
    }

    private GameObject CreateUIObject(
        string objectName,
        RectTransform parent)
    {
        GameObject obj = new GameObject(
            objectName,
            typeof(RectTransform)
        );

        obj.transform.SetParent(parent, false);
        return obj;
    }

    private void StretchFull(RectTransform rect)
    {
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    private void CreateText(
        string objectName,
        string content,
        RectTransform parent,
        Vector2 anchor,
        Vector2 position,
        Vector2 size,
        int fontSize,
        Color color)
    {
        GameObject obj = CreateUIObject(objectName, parent);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchorMin = anchor;
        rect.anchorMax = anchor;
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;

        Text text = obj.AddComponent<Text>();
        text.text = content;
        text.font = uiFont;
        text.fontSize = fontSize;
        text.color = color;
        text.alignment = TextAnchor.MiddleCenter;
        text.fontStyle = FontStyle.Bold;
        text.raycastTarget = false;

        // 文字に影をつける
        Shadow shadow = obj.AddComponent<Shadow>();
        shadow.effectColor = new Color(0.1f, 0.05f, 0.3f, 0.9f);
        shadow.effectDistance = new Vector2(3f, -3f);
    }
}
