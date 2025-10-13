using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEngine.UI.Button resumeButton;
    public UnityEngine.UI.Button quitButton;
    private bool game_paused;
    public bool gamePaused
        {
            get { return game_paused; }
            set 
            { 
                game_paused = value;

                //Cursor.visible = value;
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
                print(value);
                GetComponent<Canvas>().enabled = value;
                
            }
        }
    void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
        gamePaused = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
            
        }
    }
    void ResumeGame()
    {
        gamePaused = false;
    }
    void QuitGame()
    {
        Application.Quit();
    }
    
}
