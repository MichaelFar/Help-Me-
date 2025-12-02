using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Button Level1Button;
    public UnityEngine.UI.Button Level2Button;
    public UnityEngine.UI.Button quitButton;

    public string Level1Name;
    public string Level2Name;
    void Start()
    {
        Level1Button.onClick.AddListener(() => LoadLevel1(Level1Name));
        Level2Button.onClick.AddListener(() => LoadLevel2(Level2Name));
        quitButton.onClick.AddListener(Quitting);
    }

    // Update is called once per frame
    
    public void LoadLevel1(string level_1_name)
    {
        SceneManager.LoadScene(level_1_name);
        //Debug.Log("Worked!");
    }
    public void LoadLevel2(string level_2_name)
    {
        SceneManager.LoadScene(level_2_name);
        //Debug.Log("Worked!");
    }

    public void Quitting()
    {
        Application.Quit();
        Debug.Log("Successfully Quit.");
    }
}
