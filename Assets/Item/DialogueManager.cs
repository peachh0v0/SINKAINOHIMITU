
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("ï\é¶ê›íË")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image nameImage;
    [SerializeField] private Image dialogueImage;
    [SerializeField] private Image promptImage;

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

        if (promptImage != null)
            promptImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        if (isTalking)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
                NextImage();

            return;
        }

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

        if (promptImage != null)
            promptImage.gameObject.SetActive(true);
    }

    public void ClearNearby(DialogueInteractable target)
    {
        if (currentTarget != target) return;

        currentTarget = null;

        if (!isTalking && promptImage != null)
            promptImage.gameObject.SetActive(false);
    }

    private void StartDialogue(DialogueInteractable target)
    {
        if (target == null ||
            target.DialogueImages == null ||
            target.DialogueImages.Length == 0)
        {
            Debug.LogWarning(
                "âÔòbâÊëúÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB",
                target
            );
            return;
        }

        currentTarget = target;
        currentImages = target.DialogueImages;
        imageIndex = 0;
        isTalking = true;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        if (promptImage != null)
            promptImage.gameObject.SetActive(false);

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
            imageIndex < 0 ||
            imageIndex >= currentImages.Length)
            return;

        dialogueImage.sprite = currentImages[imageIndex];
        dialogueImage.preserveAspect = true;
        dialogueImage.enabled = currentImages[imageIndex] != null;
    }

    private void EndDialogue()
    {
        isTalking = false;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (girlController != null)
            girlController.enabled = true;

        if (promptImage != null && currentTarget != null)
            promptImage.gameObject.SetActive(true);

        currentImages = null;
        girlController = null;
    }
}
