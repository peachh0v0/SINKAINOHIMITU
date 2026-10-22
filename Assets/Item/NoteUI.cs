
using UnityEngine;
using TMPro;

public class NoteUI : MonoBehaviour
{
    public static NoteUI Instance;

    [SerializeField] private GameObject notePanel;
    [SerializeField] private TMP_Text noteTitle;
    [SerializeField] private TMP_Text noteBody;

    private ItemData currentNote;

    // 手紙が開いているか確認できる
    public bool IsOpen
    {
        get
        {
            return notePanel != null && notePanel.activeSelf;
        }
    }

    private void Awake()
    {
        Instance = this;

        if (notePanel != null)
        {
            notePanel.SetActive(false);
        }
    }

    // 手紙を表示する
    public void ShowNote(ItemData item)
    {
        if (item == null || notePanel == null)
            return;

        currentNote = item;

        if (noteTitle != null)
            noteTitle.text = item.itemName;

        if (noteBody != null)
            noteBody.text = item.noteText;

        notePanel.SetActive(true);
    }

    // 手紙を閉じる
    public void CloseNote()
    {
        if (notePanel != null)
        {
            notePanel.SetActive(false);
        }

        // 消費設定がONならインベントリから削除
        if (currentNote != null &&
            currentNote.consumeOnUse &&
            Inventory.Instance != null)
        {
            Inventory.Instance.RemoveItem(currentNote);
        }

        currentNote = null;
    }
}
