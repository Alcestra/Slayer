using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoFinish : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;

    private void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            winScreen.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }    
    }


    public void OnButtonClick()
    {
        Replay();
    }

    void Replay()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
