using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    // Start is called before the first frame update

    public float baseLineScore = 100.0f;
    public float maxPenaltyForDestroyedLimbs = 25.0f;
    public float penaltyForDeadLimb = 5.0f;
    public float penaltyForDeadHead = 20.0f;
    public float maxScoreMult = 2.0f;
    [HideInInspector]
    public List<CollisionDamageLerper> limbLerpers = new List<CollisionDamageLerper>();
    [HideInInspector]
    public CollisionDamageLerper headObject;
    [HideInInspector]
    float currentFinalScore = 0.0f;
    public Timer timer;

    public TextMeshProUGUI gameOverScoreTextLabel;
    public TextMeshProUGUI victoryScoreTextLabel;
    public TextMeshProUGUI victoryHighScoreTextLabel;
    public TextMeshProUGUI gameOverHighScoreTextLabel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateHealthLerpers(DraggableBody[] list_of_limbs)
    {
        foreach (DraggableBody i in list_of_limbs)
        {
            CollisionDamageLerper this_lerper = i.GetComponent<CollisionDamageLerper>();
            limbLerpers.Add(this_lerper);
        }
    }

    public void CalculateScore()
    {
        float modified_score = baseLineScore;

        float current_score_mult = maxScoreMult;
        //float limb_penalty_total = 0.0f;

        //float current_score = 0.0f;
        float final_time = timer.currentTime;
        float minutes_passed = final_time / 60.0f;
        float final_score = 0.0f;
        foreach (CollisionDamageLerper i in limbLerpers)
        {
            
            float limb_health_percentage = (1.0f - (i.GetCurrentHealth() / i.GetMaxHealth())) * 10.0f;
            modified_score -= (Mathf.Floor(limb_health_percentage));
            
                print("Modified score after accounting damage is " + modified_score);
            if (i.isDead && !i.Equals(headObject))
            {
                modified_score -= penaltyForDeadLimb;
            }
            else if (i.isDead && i.Equals(headObject))
            {
                modified_score -= penaltyForDeadHead;
            }
            
        }

        if(!headObject.isDead)
        {
            final_score = Math.Clamp((maxScoreMult - (Mathf.Floor(minutes_passed) / 10.0f)), 0.2f, maxScoreMult) * modified_score;
        }
        else
        {
            final_score = modified_score;
        }

        
        currentFinalScore = final_score;
        SaveScoreToFile(currentFinalScore.ToString());
    }

    public void SetScoreLabelText()
    {
        GetScoreAndPopulateLabels();
        gameOverScoreTextLabel.text = "Your score: "+ currentFinalScore.ToString();
        victoryScoreTextLabel.text = "Your score: " + currentFinalScore.ToString();
    }
    private void SaveScoreToFile(string score)
    {
        Scene scene = SceneManager.GetActiveScene();
        string path = Application.dataPath + "\\" + scene.name + "ScoreSave.txt";
        bool is_higher = false;
        if(!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {

                sw.WriteLine("0");
            }
        }
            
        using (StreamReader sr = File.OpenText(path))
        {
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                is_higher = (float)Convert.ToDouble(s) < (float)Convert.ToDouble(score);
            }
        }
        if (is_higher)
        {
            using (StreamWriter sw = File.CreateText(path))
            {

                sw.WriteLine(score);
            }
        }

        
    }
    private void GetScoreAndPopulateLabels()
    {
        Scene scene = SceneManager.GetActiveScene();


        int scene_count = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        
        string path = Application.dataPath + "\\" + scene.name + "ScoreSave.txt";
        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("0");
            }
        }
        using (StreamReader sr = File.OpenText(path))
        {
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                victoryHighScoreTextLabel.text = "Best Score: " + s;
                gameOverHighScoreTextLabel.text = "Best Score: " + s;
            }
        }
    }
    
}
