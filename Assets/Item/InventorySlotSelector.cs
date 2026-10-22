
using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySlotSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] selectedFrames = new GameObject[4];

    private int selectedIndex = 0;

    private void Start()
    {
        UpdateSelection();
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame ||
            Keyboard.current.numpad1Key.wasPressedThisFrame)
            SelectSlot(0);

        if (Keyboard.current.digit2Key.wasPressedThisFrame ||
            Keyboard.current.numpad2Key.wasPressedThisFrame)
            SelectSlot(1);

        if (Keyboard.current.digit3Key.wasPressedThisFrame ||
            Keyboard.current.numpad3Key.wasPressedThisFrame)
            SelectSlot(2);

        if (Keyboard.current.digit4Key.wasPressedThisFrame ||
            Keyboard.current.numpad4Key.wasPressedThisFrame)
            SelectSlot(3);
    }

    private void SelectSlot(int index)
    {
        selectedIndex = index;
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < selectedFrames.Length; i++)
        {
            if (selectedFrames[i] != null)
                selectedFrames[i].SetActive(i == selectedIndex);
        }
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }
}
