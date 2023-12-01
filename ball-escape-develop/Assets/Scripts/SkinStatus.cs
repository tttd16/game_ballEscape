using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinStatus : MonoBehaviour
{
   
    [SerializeField] Image chosePanel;
    [SerializeField] Image blackPanel;
    [SerializeField] string skinName;
    [SerializeField] TextMeshProUGUI skinNameText;
    public int skinID;
    public bool unlocked;
    public bool release;
   
    // Start is called before the first frame update
    void Start()
    {
        if (skinID != 0)
        {
            foreach (int i in SaveManager.instance.saveData.ListSkinUnlocked)
            {
                if (i == skinID)
                {
                    unlocked = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (unlocked)
        {
            blackPanel.gameObject.SetActive(false);
        }
    }
    public void SkinSelect()
    {
        skinNameText.gameObject.SetActive(true);
        skinNameText.text = skinName;
        chosePanel.transform.position = gameObject.transform.position;
        SkinManager.instance.skinSelected = skinID;
       
    }
    public void UnlockSkin()
    {
        unlocked = true;
        
    }
}