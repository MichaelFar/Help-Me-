using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathVictoryButtons : MonoBehaviour
{
    public UnityEngine.UI.Button resumeButton;
    public UnityEngine.UI.Button quitButton;
    private bool isButton = true;
    private bool game_paused;
    public bool gamePaused
    {
        get { return game_paused; }
        set
        {
            game_paused = value;

            //Cursor.visible = value;
            if(!isButton)
            {
                if (game_paused)
                {

                    UnityEngine.Cursor.lockState = CursorLockMode.Confined;
                    UnityEngine.Cursor.visible = true;
                    Time.timeScale = 0;
                }
                else
                {
                    UnityEngine.Cursor.lockState = CursorLockMode.Locked;

                    UnityEngine.Cursor.visible = false;
                    Time.timeScale = 1;

                }
                GetComponent<Canvas>().enabled = value;
            }
                

        }
    }
    void Start()
    {
        
        isButton = gameObject.GetComponent<UnityEngine.UI.Button>();
        
        gamePaused = false;

    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Replaying(string kinLevel)
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Worked!");
    }

    public void Quitting()
    {
        Application.Quit();
        Debug.Log("Successfully Quit.");
    }

    public void SetShown(bool new_value)
    {
        gamePaused = new_value;
    }

}
