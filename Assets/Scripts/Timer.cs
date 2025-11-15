using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public float currentTime = 0.0f;
    public TextMeshProUGUI textLabel;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        textLabel.text = currentTime.ToString("0.00");
    }
}
