using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    // Start is called before the first frame update

    public CheckPointVolume[] checkpointVolumes;

    public DeathVictoryButtons victoryScreen;

    private bool reached_all_points = false;

    bool reachedAllPoints
    {
                get { return reached_all_points; }
                set {
                        reached_all_points = value;
                        if(reached_all_points)
                        {
                        //CODE THAT WINS THE GAME
                        victoryScreen.SetShown(true);
                        }
                    }
               
    }
    void Start()
    {
        foreach (CheckPointVolume i in checkpointVolumes)
        {
            i.myManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckTheList()
    {
        bool temp_reached_all_points = true;
        foreach (CheckPointVolume i in checkpointVolumes)
        {
            if(!i.hasReached)
            {
                temp_reached_all_points = false;
                break;
            }
        }
        reachedAllPoints = temp_reached_all_points;
    }
}
