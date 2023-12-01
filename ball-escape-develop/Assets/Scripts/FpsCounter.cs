using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsCounter : MonoBehaviour
{
    private int fps = 0;
    private float timeCounter = 0,  frameCounter = 0;
    public float refreshTime = 1;
    [SerializeField] TextMeshProUGUI FPS;
    

    private void Start()
    {
       // Screen.sleepTimeout = SleepTimeout.SystemSetting;
        //Screen.SetResolution(960, 540, true);
        // QualitySettings.vSyncCount = 0;
       Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    void Update()
    {
        if (timeCounter < refreshTime)
        {
            timeCounter += Time.unscaledDeltaTime;
            frameCounter++;
        }
        else
        {
            fps = (int) (frameCounter / timeCounter);
            frameCounter = 0;
            timeCounter = 0;
           
        }
       // FPS.text = "" + fps;
       }
}
