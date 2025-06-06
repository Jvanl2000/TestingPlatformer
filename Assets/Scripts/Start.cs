using UnityEngine;

public class Start : MonoBehaviour
{
    public void StartLevel1()
    {
        // Load the main game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
    }

    public void StartLevel2()
    {
        // Load the main game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2");
    }
}
