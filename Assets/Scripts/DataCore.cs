using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class DataCore : MonoBehaviour
{
    public string filename;
    public Data data;
    public bool save = false, load = false, delete = false;

    string path;
    XmlSerializer serial;
    FileStream fstream;

    void Start()
    {
        serial = new XmlSerializer(typeof(Data));
        path = Application.persistentDataPath;

        if(System.IO.File.Exists(path + "/" + filename))
        {
            Load();
        }
        else
        {
            data = new Data();
        }
    }

    void Update()
    {
        if(save)
        {
            Save();

            save = false;
        }
        if(load)
        {
            Load();

            load = false;
        }
        if(delete)
        {
            Delete();

            delete = false;
        }
    }

    public void Save()
    {
        fstream = new FileStream(path + "/" + filename, FileMode.Create);
        serial.Serialize(fstream, data);
        fstream.Close();

        Debug.Log("saved");
    }
    public void Load()
    {
        if(System.IO.File.Exists(path + "/" + filename))
        {

            fstream = new FileStream(path + "/" + filename, FileMode.Open);
            data = (Data)serial.Deserialize(fstream);
            fstream.Close();

            Debug.Log("loaded");
        }
        else
        {
            Debug.Log("save file does not exist");
        }
    }
    public void Delete()
    {
        if (System.IO.File.Exists(path + "/" + filename))
        {
            File.Delete(path + "/" + filename);

            Debug.Log("deleted");
        }
        else
        {
            Debug.Log("save file does not exist");
        }
    }
}


[System.Serializable]
public class Data
{
    public int score = 0;
    public string[] unlocked = new string[] {"RedPlayer"};
    public string pick = "RedPlayer";
}