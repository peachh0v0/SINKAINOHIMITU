using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public static float SEVolume
    {
        get => PlayerPrefs.GetFloat("SEVolume", 1.0f);
        set
        {
            PlayerPrefs.SetFloat("SEVolume", value);
            PlayerPrefs.Save();
        }
    }

    public static float BGMVolume
    {
        get => PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        set
        {
            PlayerPrefs.SetFloat("BGMVolume", value);
            PlayerPrefs.Save();
        }
    }

    public static bool FullScreen
    {
        get => PlayerPrefs.GetInt("FullScreen", 1) == 1;
        set
        {
            PlayerPrefs.SetInt("FullScreen", value ? 1 : 0);
            PlayerPrefs.Save();

            Screen.fullScreen = value;
        }
    }

    public void SetSEVolume(float value)
    {
        SEVolume = value;
    }

    public void SetBGMVolume(float value)
    {
        BGMVolume = value;
    }

    public void SetFullScreen()
    {
        FullScreen = true;
    }

    public void SetWindowed()
    {
        FullScreen = false;
    }
}