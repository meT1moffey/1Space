using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class PlayerData : MonoBehaviour
{
    public string filename;
    public Data data;
    public bool save = false, load = false, delete = false;
    public GameObject scorenum;
    public Sprite[] nums;

    string path;
    XmlSerializer serial;
    FileStream fstream;
    GameObject[] shownum;

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

        shownum = new GameObject[1];
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

        for (int i = 0; i < shownum.Length; i++)
        {
            Destroy(shownum[i]);
        }
        shownum = new GameObject[data.score.ToString().Length];
        for (int i = 0; i < data.score.ToString().Length; i++)
        {
            shownum[i] = GameObject.Instantiate(scorenum);
            shownum[i].transform.position = new Vector3(-2 * i + 2 * data.score.ToString().Length - 8, -85, -5);
            shownum[i].GetComponent<SpriteRenderer>().sprite = nums[(int)(data.score / Mathf.Pow(10, i)) % 10];
            shownum[i].tag = "Number";
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
    public string[] unlocked = new string[] {"BasicPlayer"};
}