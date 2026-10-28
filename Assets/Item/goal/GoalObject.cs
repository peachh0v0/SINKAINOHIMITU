
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalObject : MonoBehaviour
{
    [SerializeField] private string clearSceneName = "Clear";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        SceneManager.LoadScene(clearSceneName);
    }
}
