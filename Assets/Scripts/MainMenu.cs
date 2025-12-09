using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEngine.UI.Button[] LevelButtons;
    
    public UnityEngine.UI.Button quitButton;
    public TextMeshProUGUI[] textLabels;


    public string[] LevelNames;
    void Start()
    {
        GetScoreAndPopulateLabels();
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            int temp_i = i;
            print("Looping through buttons to add listeners");
            print(i);
            LevelButtons[temp_i].onClick.AddListener(() => LoadLevel(LevelNames[temp_i]));
        }
        
        quitButton.onClick.AddListener(Quitting);
    }

    // Update is called once per frame
    
    public void LoadLevel(string level_name)
    {
        SceneManager.LoadScene(level_name);
        //Debug.Log("Worked!");
    }
   

    public void Quitting()
    {
        Application.Quit();
        Debug.Log("Successfully Quit.");
    }

    private void GetScoreAndPopulateLabels()
    {
        Scene scene = SceneManager.GetActiveScene();
        
        
        int scene_count = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            string path = Application.dataPath + "\\" + LevelNames[i] + "ScoreSave.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("High Score: 0");
                }
            }
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    textLabels[i].text = "High Score: " + s;
                }
            }
        }
    }

    
}
