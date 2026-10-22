using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectButton : MonoBehaviour
{
    public string stageName;

    public void SelectStage()
    {
        SceneManager.LoadScene(stageName);
    }

    public void OpenOption()
    {
        SceneManager.LoadScene("op");
    }
}