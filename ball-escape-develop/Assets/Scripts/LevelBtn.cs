using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelBtn : MonoBehaviour
{
    public int level;
    [SerializeField] TextMeshProUGUI levelNum;
    [SerializeField] Image Lock;
    public static LevelBtn instance;
    public static int levelSelected;
    public bool unlocked;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (level != 1)
        {
            foreach (int i in SaveManager.instance.saveData.ListLevelUnlocked)
            {
                if (i == level)
                {
                    unlocked = true;
                }
            }
        }

        if (unlocked)
        {
            Lock.gameObject.SetActive(false);
            levelNum.gameObject.SetActive(true);
            levelNum.text = "" + level;
        }
        else
        {
            levelNum.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectLevel()
    {
        if (unlocked)
        {
            levelSelected = level;
            SceneManager.LoadScene("Game");
            DOTween.KillAll();

        }

    }
}
