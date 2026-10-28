
using UnityEngine;
using UnityEngine.SceneManagement;

public class JellyfishStageButton : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void OnClickStage()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("ˆÚ“®æ‚ÌƒV[ƒ“–¼‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñI");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
