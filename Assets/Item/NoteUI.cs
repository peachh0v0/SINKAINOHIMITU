
using UnityEngine;
using TMPro;

public class NoteUI : MonoBehaviour
{
    public static NoteUI Instance;

    [SerializeField] private GameObject notePanel;
    [SerializeField] private TMP_Text noteTitle;
    [SerializeField] private TMP_Text noteBody;

    private void Awake()
    {
        Instance = this;

        if (notePanel != null)
            notePanel.SetActive(false);
    }

    public void ShowNote(ItemData item)
    {
        if (item == null) return;

        noteTitle.text = item.itemName;
        noteBody.text = item.noteText;
        notePanel.SetActive(true);
    }

    public void CloseNote()
    {
        notePanel.SetActive(false);
    }
}
