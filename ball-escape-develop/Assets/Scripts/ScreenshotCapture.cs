using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotCapture : MonoBehaviour
{
    string ssName = "SS_";
    int index = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("screenshot");
            index = Random.Range(1, 100000);
            ScreenCapture.CaptureScreenshot(ssName + Screen.width + "x" + Screen.height + "_" + index + ".png");
        }
    }
}
