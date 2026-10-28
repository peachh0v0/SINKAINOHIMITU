
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("表示設定")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image nameImage;
    [SerializeField] private Image dialogueImage;

    private DialogueInteractable currentTarget;
    private Sprite[] currentImages;
    private int imageIndex;
    private bool isTalking;
    private GirlController girlController;

    public bool IsTalking => isTalking;

    private void Awake()
    {
        Instance = this;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        // 会話中はスペースキーで次へ
        if (isTalking)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
                NextImage();

            return;
        }

        // 絵画の近くでEキーを押すと会話開始
        if (currentTarget != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            StartDialogue(currentTarget);
        }
    }

    public void SetNearby(DialogueInteractable target)
    {
        if (isTalking) return;

        currentTarget = target;
    }

    public void ClearNearby(DialogueInteractable target)
    {
        if (currentTarget == target)
            currentTarget = null;
    }

    private void StartDialogue(DialogueInteractable target)
    {
        if (target == null ||
            target.DialogueImages == null ||
            target.DialogueImages.Length == 0)
        {
            Debug.LogWarning("会話画像が設定されていません。", target);
            return;
        }

        currentTarget = target;
        currentImages = target.DialogueImages;
        imageIndex = 0;
        isTalking = true;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        GameObject girl = GameObject.FindGameObjectWithTag("Player");

        if (girl != null)
            girlController = girl.GetComponent<GirlController>();

        if (girlController != null)
            girlController.enabled = false;

        if (nameImage != null)
        {
            nameImage.sprite = target.SpeakerNameImage;
            nameImage.preserveAspect = true;
            nameImage.enabled = target.SpeakerNameImage != null;
        }

        ShowImage();
    }

    private void NextImage()
    {
        imageIndex++;

        if (currentImages == null ||
            imageIndex >= currentImages.Length)
        {
            EndDialogue();
            return;
        }

        ShowImage();
    }

    private void ShowImage()
    {
        if (dialogueImage == null ||
            currentImages == null ||
            imageIndex >= currentImages.Length)
            return;

        dialogueImage.sprite = currentImages[imageIndex];
        dialogueImage.preserveAspect = true;
        dialogueImage.enabled = currentImages[imageIndex] != null;
    }

    private void EndDialogue()
    {
        isTalking = false;

        // 会話枠・名前・セリフをまとめて非表示
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (nameImage != null)
            nameImage.enabled = false;

        if (dialogueImage != null)
        {
            dialogueImage.sprite = null;
            dialogueImage.enabled = false;
        }

        if (girlController != null)
            girlController.enabled = true;

        currentImages = null;
        currentTarget = null;
        girlController = null;
    }
}
