using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Image healthBarImage;
    void Start()
    {
        
    }

    public void SetHealthBarValue(float new_health)
    {
        healthBarImage.fillAmount = new_health;
    }

}
