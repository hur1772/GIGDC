using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerPos : MonoBehaviour
{
    public GameObject Tiger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            SoundMgr.Instance.PlayGUISound("Tiger_Gen", 1.5f);
            Tiger.SetActive(true);

        }
    }
 
}
