using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;


public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    [SerializeField] Image fadePanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] RectTransform GameOverPanel;
    [SerializeField] RectTransform CompleteLevelPanel;
    [SerializeField] GameObject NotificationPanel;
    [SerializeField] GameObject GameOverTitle;
    [SerializeField] RectTransform CompleteLevelTitle;
    [SerializeField] GameObject Trophy;
    [SerializeField] TextMeshProUGUI CommingSoon;
    bool gameover;
    bool paused;
    bool completed;

    private bool isPlayingAnim = false;
    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void GameOverUI()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(false);
        GameOverPanel.gameObject.SetActive(true);
        GameOverPanel.transform.localPosition = new Vector3(0, -1000, 0);
        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(0.3f, 1f).SetUpdate(true);
        GameOverTitle.transform.DOScale(0.5f, 1f).SetEase(Ease.InOutElastic).SetUpdate(true).OnComplete(() =>
        {
            GameOverPanel.DOAnchorPos(new Vector3(0, 0, 0), 1f, false).SetEase(Ease.InOutElastic).SetUpdate(true);
            gameover = true;
        });
        
    }
    public void CompleteLevelUI()
    {
        PausePanel.SetActive(false);
        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(0.3f, 1f).SetUpdate(true);
        CompleteLevelPanel.gameObject.SetActive(true);
        CompleteLevelTitle.gameObject.SetActive(true);
        CompleteLevelPanel.transform.localPosition = new Vector3(0, -1000, 0);
        CompleteLevelTitle.transform.localPosition = new Vector3(0, 2000, 0);
        CompleteLevelTitle.DOAnchorPos(new Vector3(0,500,0),1f,false).SetEase(Ease.InOutElastic).SetUpdate(true);
        Trophy.transform.DOScale(4f, 1f).SetEase(Ease.InOutElastic).SetUpdate(true);
        CompleteLevelPanel.DOAnchorPos(new Vector3(0, 0, 0), 1f, false).SetEase(Ease.InOutElastic).SetUpdate(true).OnComplete(() =>
        {
           
            completed = true;
        });
    }
    public void RestartButton()
    {
        if (isPlayingAnim)
            return;
        isPlayingAnim = true;
        GameManager.instance.gameover = false;
        if (paused)
        {
            
            fadePanel.DOFade(0f, 0.5f).SetUpdate(true);
            PausePanel.transform.DOScale(0, 0.5f).SetUpdate(true);
            DOVirtual.DelayedCall(0.5f, () =>
            {
                paused = false;
                Time.timeScale = 1;
                GameManager.instance.ResetLevel();
                fadePanel.gameObject.SetActive(false);
                isPlayingAnim = false;
            });
            
        }
        if (gameover)
        {
          
            fadePanel.DOFade(0f, 1f).SetUpdate(true);
            GameOverTitle.transform.DOScale(0f, 1f).SetEase(Ease.InElastic).SetUpdate(true);
            GameOverPanel.DOAnchorPos(new Vector3(0, -1000, 0), 1f, false).SetEase(Ease.InElastic).SetUpdate(true).OnComplete(() =>
            {
                gameover = false;
                Time.timeScale = 1;
                GameManager.instance.ResetLevel();
                fadePanel.gameObject.SetActive(false);
                GameOverPanel.gameObject.SetActive(false);
                isPlayingAnim = false;
            });
           
        }
        if (completed)
        {
           
            fadePanel.DOFade(0f, 1f).SetUpdate(true);
            CompleteLevelTitle.DOAnchorPos(new Vector3(0, 2000, 0), 1f, false).SetEase(Ease.InOutElastic).SetUpdate(true);
            CompleteLevelPanel.DOAnchorPos(new Vector3(0, -1000, 0), 1f, false).SetEase(Ease.InOutElastic).SetUpdate(true);
            Trophy.transform.DOScale(0f, 1f).SetEase(Ease.InElastic).SetUpdate(true).OnComplete(() =>
            {
                CompleteLevelPanel.gameObject.SetActive(false);
                CompleteLevelTitle.gameObject.SetActive(false);
                completed = false;
                Time.timeScale = 1;
                GameManager.instance.ResetLevel();
                fadePanel.gameObject.SetActive(false);
                isPlayingAnim = false;
            });
        }
    }
    public void ResumeBtn()
    {
        if (paused)
        {
           
            fadePanel.DOFade(0f, 0.5f).SetUpdate(true);
            PausePanel.transform.DOScale(0, 0.5f).SetUpdate(true);
            DOVirtual.DelayedCall(0.5f, () =>
            {
                paused = false;
                Time.timeScale = 1;
                fadePanel.gameObject.SetActive(false);
            });
        }
    }
    public void PauseBtn()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
            fadePanel.gameObject.SetActive(true);
            fadePanel.DOFade(0.3f, 0.5f).SetUpdate(true);
            PausePanel.transform.DOScale(1, 0.5f).SetUpdate(true);
            DOVirtual.DelayedCall(0.5f, () =>
            {
                paused = true;
                
            });
        }
    }
    public void ButtonHome()
    {
        
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }
    public void NextLevelBtn()
    {
        if (completed)
        {
            fadePanel.DOFade(0f, 1f).SetUpdate(true);
            CompleteLevelTitle.DOAnchorPos(new Vector3(0, 2000, 0), 1f, false).SetEase(Ease.InOutElastic).SetUpdate(true);
            CompleteLevelPanel.DOAnchorPos(new Vector3(0, -1000, 0), 1f, false).SetEase(Ease.InOutElastic).SetUpdate(true);
            Trophy.transform.DOScale(0f, 1f).SetEase(Ease.InOutElastic).SetUpdate(true).OnComplete(() =>
            {
                CompleteLevelPanel.gameObject.SetActive(false);
                CompleteLevelTitle.gameObject.SetActive(false);
                completed = false;
                Time.timeScale = 1;
                GameManager.instance.ContinueNextLevel();
                fadePanel.gameObject.SetActive(false);
            });

        }
    }
    public void WatchAdsBtn()
    {
        fadePanel.DOFade(0f, 1f).SetUpdate(true);
        GameOverTitle.transform.DOScale(0f, 1f).SetEase(Ease.InOutElastic).SetUpdate(true);
        GameOverPanel.DOAnchorPos(new Vector3(0, -1000, 0), 1f, false).SetEase(Ease.InOutElastic).SetUpdate(true).OnComplete(() =>
        {
            GoogleAdmob.Instance.ShowRewarded(false);
        });
    }

    public void LevelCoomingSoon()
    {
        CommingSoon.gameObject.SetActive(true);
    }
   
}
