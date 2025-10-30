using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartManager : MonoBehaviour
{
    // Start is called before the first frame update
    public CollisionDamageLerper headDamageObject;

    public DeathVictoryButtons deathScreen;

    private int numDied = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(headDamageObject.isDead && numDied < 1)
        {
            print("Head has died and you lose");
            numDied += 1;
            ///Put death function here 
            deathScreen.SetShown(true);
        }
    }
}
