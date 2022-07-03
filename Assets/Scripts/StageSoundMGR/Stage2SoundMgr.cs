using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2SoundMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundMgr.Instance.PlayBGM("1_2_BGM", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
