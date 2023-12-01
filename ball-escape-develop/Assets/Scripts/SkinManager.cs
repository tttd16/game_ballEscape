using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SkinManager : MonoBehaviour
{
    [SerializeField] GameObject SkinUI;
    [SerializeField] GameObject MenuUI;
    [SerializeField] GameObject NotificationPanel;
    [SerializeField] TextMeshProUGUI Notification;
    public static SkinManager instance;
    [SerializeField] List<GameObject> ListSkin = new List<GameObject>();
    public int skinSelected;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UnlockSkinBtn()
    {
        if (skinSelected < ListSkin.Count)
        {
            if (!ListSkin[skinSelected].GetComponent<SkinStatus>().unlocked)
            {
                Notification.text = "You can unlock this skin by watching a video ad. Watch it now?";
                NotificationPanel.transform.DOScale(1f, 0.5f).SetEase(Ease.Linear);
            }
        }
        else
        {
            Notification.text = "Skin is comming soon";
            NotificationPanel.transform.DOScale(1f, 0.5f).SetEase(Ease.Linear);
        }
         
    }
    public void AcceptBtn()
    {
        if (skinSelected < ListSkin.Count)
        {
            NotificationPanel.transform.DOScale(0f, 0.5f).SetEase(Ease.Linear);
            GoogleAdmob.Instance.ShowRewarded(true);
        }
        else
        {
            CancelBtn();
        }
       
    }
    public void CancelBtn()
    {
        NotificationPanel.transform.DOScale(0f, 0.5f).SetEase(Ease.Linear);
    }
    public void SelectSkinBtn()
    {
        if (skinSelected < ListSkin.Count)
        {
            if (ListSkin[skinSelected].GetComponent<SkinStatus>().unlocked)
        {
            PlayerPrefs.SetInt("Skin Use", skinSelected);
            SkinUI.transform.DOScale(0f, 0.5f);
            DOVirtual.DelayedCall(0.5f, () =>
            {
                MenuUI.transform.DOScale(1.2f, 0.5f);
            });
        }
        else
        {
            Notification.text = "This skin's not unlocked yet. You can unlock this skin by watching a video ad. Watch it now?";
            NotificationPanel.transform.DOScale(1f, 0.5f).SetEase(Ease.Linear);
        }
        }
        else
        {
            Notification.text = "Skin is comming soon";
            NotificationPanel.transform.DOScale(1f, 0.5f).SetEase(Ease.Linear);
        }

    }
    public void RewardSkin()
    {
        ListSkin[skinSelected].GetComponent<SkinStatus>().UnlockSkin();
        SaveManager.instance.SaveSkin(skinSelected);
    }
}
