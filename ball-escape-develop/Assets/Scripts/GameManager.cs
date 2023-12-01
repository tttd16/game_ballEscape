using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int numPlay;
    [SerializeField] List<GameObject> ListLevel = new List<GameObject>();
    public int mylevel = 0;
    public static GameManager instance;
    public GameObject NowLevel;
    public bool gameover;
    [SerializeField] ParticleSystem deadParticle;
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    void Start()
    {
        //Debug.Log(LevelBtn.levelSelected);
        mylevel = LevelBtn.levelSelected - 1;
        if (mylevel < 0)
            mylevel = 0;
        if (mylevel <= ListLevel.Count - 1)
        {

           NowLevel = Instantiate(ListLevel[mylevel]);
        }
        else
        {
            ButtonManager.instance.LevelCoomingSoon();
        }
        
    }

    public void CompleteLevel()
    {
        
        numPlay++;
        if (numPlay == Random.Range(3, 4))
        {
            //Debug.Log(numPlay);
            numPlay = 0;
            
            GoogleAdmob.Instance.ShowInterstitial();
        }
        if (SaveManager.instance.saveData.ListLevelUnlocked.Count == 0  || SaveManager.instance.saveData.ListLevelUnlocked[SaveManager.instance.saveData.ListLevelUnlocked.Count-1]<mylevel+2)
        {
            SaveManager.instance.SaveLevel(mylevel + 2);
            SaveManager.instance.Load();
        }
        SoundManager.instance.PlayWinSound();
        ButtonManager.instance.CompleteLevelUI();
    }
    public void GameOver()
    {
        if (PlayerPrefs.GetInt("Vibrate") == 1)
        {
            Handheld.Vibrate();
        }
        gameover = true;
        numPlay++;

        if (numPlay == Random.Range(3, 4))
        {
            GoogleAdmob.Instance.ShowInterstitial();
            numPlay = 0;

        }
        SoundManager.instance.PlayFailSound();
        ButtonManager.instance.GameOverUI();
    }
    public void ContinueNextLevel()
    {
        mylevel++;
        Destroy(NowLevel);
        if (mylevel <= ListLevel.Count-1)
        {
            NowLevel = Instantiate(ListLevel[mylevel]);
        }
        else
        {
            ButtonManager.instance.LevelCoomingSoon();
        }
        
    }
    public void ResetLevel()
    {
        Destroy(NowLevel);
        if (mylevel <= ListLevel.Count - 1)
        {
           
            NowLevel = Instantiate(ListLevel[mylevel]);
        }
        else
        {
            ButtonManager.instance.LevelCoomingSoon();
            
        }
        
    }
    IEnumerator DeadParticalOn(Transform transform)
    {
        deadParticle.transform.position = transform.position;
        deadParticle.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        deadParticle.gameObject.SetActive(false);
        
    }
    public void PlayParticle(Transform transform)
    {
        StartCoroutine(DeadParticalOn(transform));
    }
}
