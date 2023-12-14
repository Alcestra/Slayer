using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenu : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Restart();
        }
    }

    public void Restart()
    {
        //get the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //restart current
        SceneManager.LoadScene(currentSceneIndex);
    }
}
