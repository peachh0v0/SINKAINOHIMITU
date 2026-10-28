
using UnityEngine;

public class DialogueInteractable : MonoBehaviour
{
    [Header("˜b‚µŽè‚Ì–¼‘O‰æ‘œ")]
    [SerializeField] private Sprite speakerNameImage;

    [Header("‰ï˜b‰æ‘œ")]
    [SerializeField] private Sprite[] dialogueImages;

    public Sprite SpeakerNameImage => speakerNameImage;
    public Sprite[] DialogueImages => dialogueImages;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (DialogueManager.Instance != null)
            DialogueManager.Instance.SetNearby(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (DialogueManager.Instance != null)
            DialogueManager.Instance.ClearNearby(this);
    }
}
