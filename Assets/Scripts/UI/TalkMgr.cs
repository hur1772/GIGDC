using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkMgr : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitDate;

    public Sprite[] portraitSprite;
    // Start is called before the first frame update
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        portraitDate = new Dictionary<int, Sprite>();
        MakeData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeData()
    {
        talkData.Add(1000, new string[] { "안녕?", "이 곳에 처음 왔구나" });
        talkData.Add(2000, new string[] { "처음 보는 얼굴인데", "누구야??" });

        portraitDate.Add(1000, portraitSprite[0]);
        portraitDate.Add(2000, portraitSprite[1]);
    }


    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetSprite(int id)
    {
        return portraitDate[id];
    }
}
