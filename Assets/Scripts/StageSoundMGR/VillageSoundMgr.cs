using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageSoundMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundMgr.Instance.PlayBGM("Village_BGM", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
