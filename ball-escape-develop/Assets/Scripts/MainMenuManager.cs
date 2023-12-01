using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject PlayButton;
    [SerializeField] GameObject MenuUI;
    [SerializeField] GameObject LevelUI;
    [SerializeField] GameObject SkinUI;
    [SerializeField] GameObject SettingUI;
    [SerializeField] Toggle toggleVibrate;
    [SerializeField] Toggle toggleMusic;
    [SerializeField] AudioMixer bgMixer;
    [SerializeField] AudioMixer sfxMixer;
    [SerializeField] TextMeshProUGUI skinNameText;
    // Start is called before the first frame update
    private void Awake()
    {
#if UNITY_IOS
        if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
        }
#endif
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            toggleMusic.isOn = true;
            bgMixer.SetFloat("volume", 0);
            sfxMixer.SetFloat("volume", 0);
        }
        else
        {
            toggleMusic.isOn = false;
            bgMixer.SetFloat("volume",-80);
            sfxMixer.SetFloat("volume", -80);
        }

        if (PlayerPrefs.GetInt("Vibrate") == 1)
        {
            toggleVibrate.isOn = true;
        }
        else
        {
            toggleVibrate.isOn = false;
        }
        PlayButton.transform.DOScale(1.2f, 1f).SetLoops(-1, loopType: LoopType.Yoyo);
        GoogleAdmob.Instance.ShowBanner();
    }

    public void PlayBtn()
    {
        MenuUI.transform.DOScale(0f, 0.5f);
        DOVirtual.DelayedCall(0.5f, () =>
        {
            LevelUI.transform.DOScale(1f, 0.5f);
        });
    }
    public void MenuBtn()
    {
        skinNameText.gameObject.SetActive(false);
        LevelUI.transform.DOScale(0f, 0.5f);
        SkinUI.transform.DOScale(0f, 0.5f);
        DOVirtual.DelayedCall(0.5f, () =>
        {
           MenuUI.transform.DOScale(1.2f, 0.5f);
        });
    }
    public void ShopSkin()
    {
        MenuUI.transform.DOScale(0f, 0.5f);
        DOVirtual.DelayedCall(0.5f, () =>
        {
            SkinUI.transform.DOScale(1f, 0.5f);
        });
    }

    public void OpenStoreURL()
    {
        #if UNITY_ANDROID
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.vfx.BallEscape");
        #elif UNITY_IOS
        
        #endif
    }
    public void SettingButton()
    {
        SettingUI.transform.DOScale(1f, 0.5f).SetEase(Ease.InOutElastic);
    }
    public void ExitSetting()
    {
        SettingUI.transform.DOScale(0f, 0.5f).SetEase(Ease.InOutElastic);
    }
    public void SettingVibrate()
    {
        if (toggleVibrate.isOn)
        {
            PlayerPrefs.SetInt("Vibrate", 1 );
        }
        else
        {
            PlayerPrefs.SetInt("Vibrate", 0);
        }
    }
    public void SettingMusic()
    {
        if (toggleMusic.isOn)
        {
            PlayerPrefs.SetInt("Music", 1);
            bgMixer.SetFloat("volume", 0);
            sfxMixer.SetFloat("volume", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            bgMixer.SetFloat("volume", -80);
            sfxMixer.SetFloat("volume", -80);

        }
    }
}
