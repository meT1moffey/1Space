using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShowData
{
    public GameObject num;
    public Vector3 coord;
    public Sprite[] nums;
    public char side = 'l';

    GameObject[] show;

    public void Init()
    {
        show = new GameObject[0];
    }

    public void Clear()
    {
        for (int i = 0; i < show.Length; i++)
        {
            MonoBehaviour.Destroy(show[i]);
        }
    }

    public void Show(int value)
    {
        Clear();

        string strval = value.ToString();
        show = new GameObject[strval.Length];

        for (int i = 0; i < strval.Length; i++)
        {
            show[i] = GameObject.Instantiate(num);
            show[i].transform.position = coord;

            if (side == 'r')
            {
                show[i].transform.position += new Vector3(1.5f * i - 1.5f * (strval.Length - 1), 0, 0);
            }
            else if (side == 'l')
            {
                show[i].transform.position += new Vector3(1.5f * i, 0, 0);
            }
            else if(side == 'c')
            {
                show[i].transform.position += new Vector3(1.5f * i - 0.75f * (strval.Length - 1), 0, 0);
            }
            else
            {
                Debug.LogErrorFormat("{0} is invalid side. Only 'l', 'r' and 'c' are OK", side);
            }

            show[i].GetComponent<SpriteRenderer>().sprite = nums[strval[i] - '0'];
            show[i].tag = "Number";
        }
    }
}
