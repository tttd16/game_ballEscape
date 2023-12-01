using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public SaveData saveData;
    private void Awake()
    {

        //if (PlayerPrefs.GetInt("First Play") == 0)
        //{
        //    Debug.Log(PlayerPrefs.GetInt("First Play"));
        //    SaveLevel(1);
        //    PlayerPrefs.SetInt("First Play", 1);
        //}
       
        if (!instance)
        {
            instance = this;
        }
        
        Load();
    }
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SaveLevel(int level)
    {
        saveData.ListLevelUnlocked.Add(level);
        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + ".save", FileMode.Create);
        serializer.Serialize(stream, saveData);
        stream.Close();
    }
    public void Load()
    {
        string dataPath = Application.persistentDataPath;
        Debug.Log(dataPath + ".save");
        if (System.IO.File.Exists(dataPath + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + ".save", FileMode.Open);
            saveData = serializer.Deserialize(stream) as SaveData;
            stream.Close();
        }
    }
    public void SaveSkin(int SkinID)
    {
        saveData.ListSkinUnlocked.Add(SkinID);
        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + ".save", FileMode.Create);
        serializer.Serialize(stream, saveData);
        stream.Close();
    }
    [System.Serializable]
    public class SaveData
    {
        public List<int> ListLevelUnlocked = new List<int>();
        public List<int> ListSkinUnlocked = new List<int>();
    }
}
