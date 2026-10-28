
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ClearSceneMove : MonoBehaviour
{
    [Header("移動先のシーン名")]
    [SerializeField] private string stageSelectScene = "StageSelect";
    [SerializeField] private string nextStageScene = "Stage4";

    private void Update()
    {
        if (Keyboard.current == null) return;

        // Aキー：ステージ選択画面へ
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(stageSelectScene);
        }

        // Dキー：次のステージへ
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(nextStageScene);
        }
    }
}
