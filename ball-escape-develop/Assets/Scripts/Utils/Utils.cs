using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    //Animations
    public const string ANIM_TRIGGER_DEAD = "dead";
    public const string ANIM_TRIGGER_MAGIC = "magic";
    //Sounds
    public const string TAKE_DAMAGE_SOUND = "TakeDamageSound";
    public const string PRESS_BUTTON_SOUND = "ButtonClickSound";
    public const string GAMEOVER_SOUND = "Gameover";
    public const string LEVEL_COMPLETED_SOUND = "LevelCompleted";
    public const string COLLECT_ITEM_SOUND = "ItemCollect";

    const string FIRST_PLAY = "PLAYED";
    const string SOUND_KEY = "SOUND";
    const string MUSIC_KEY = "KEY";
    const string PLAYING_LEVEL_KEY = "PLAYING LEVEL";

    const string LEVEL_UNLOCKED = "LEVEL UNLOCKED";
    const string LEVEL_COMPLETED = "LEVEL";

    const string ADS_REMOVE_KEY = "ADS REMOVE";
    const string SHIELD_KEY = "SHIELD";

    private static bool adRemove = false;
    public static bool ADREMOVE
    {
        get
        {
            return adRemove;
        }
    }

    public static void RemoveAds()
    {
        adRemove = true;
        PlayerPrefs.SetInt(ADS_REMOVE_KEY, 1);
    }

    private static int shield = 0;
    public static int SHIELD
    {
        get
        {
            return shield;
        }
        set
        {
            shield = value;
            PlayerPrefs.SetInt(SHIELD_KEY, shield);
        }
    }

    private static bool music = true;
    public static bool MUSIC
    {
        get
        {
            return music;
        }
        set
        {
            music = value;
            PlayerPrefs.SetInt(MUSIC_KEY, music ? 1 : 0);   
        }
    }

    private static bool sound = true;
    public static bool SOUND
    {
        get
        {
            return sound;
        }
        set
        {
            sound = value;
            PlayerPrefs.SetInt(SOUND_KEY, sound ? 1 : 0);

        }
    }

    private static int playingLevel = 1;
    public static int PlayingLevel
    {
        get { return playingLevel; }
        set
        {
            playingLevel = value;
            PlayerPrefs.SetInt(PLAYING_LEVEL_KEY, playingLevel);
        }
    }

    public static int GetStarLevel(int _level)
    {
        return PlayerPrefs.GetInt(LEVEL_COMPLETED + _level, 0);
    }

    public static void SetStarLevel(int _level, int star)
    {
        if(PlayerPrefs.GetInt(LEVEL_COMPLETED + _level, 0) < star)
        {
            PlayerPrefs.SetInt(LEVEL_COMPLETED + _level, star);
        }
    }

    public static void UnlockLevel(int _level)
    {
        PlayerPrefs.SetInt(LEVEL_UNLOCKED, _level);
        PlayerPrefs.SetInt(LEVEL_COMPLETED + _level, 0);
    }

    public static bool CheckUnlockLevel(int _level)
    {
        return PlayerPrefs.HasKey(LEVEL_COMPLETED + _level);
    }

    public static int GetLevelUnlock()
    {
        return PlayerPrefs.GetInt(LEVEL_UNLOCKED);
    }

    public static void LoadData()
    {
        if (!PlayerPrefs.HasKey(FIRST_PLAY))
        {
            MUSIC = true;
            SOUND = true;
            adRemove = false;

            UnlockLevel(1);
            PlayerPrefs.SetInt(FIRST_PLAY, 1);
        }

        SOUND = PlayerPrefs.GetInt(SOUND_KEY) == 1 ? true : false;
        MUSIC = PlayerPrefs.GetInt(MUSIC_KEY) == 1 ? true : false;

        adRemove = PlayerPrefs.GetInt(ADS_REMOVE_KEY) == 1 ? true : false;
        SHIELD = PlayerPrefs.GetInt(SHIELD_KEY);
    }
}
