using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossgirlSpawn : MonoBehaviour
{
    public GameObject Bossgirl;
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
            SoundMgr.Instance.PlayEffSound("BossGirlSpawn", 1.0f);
            Bossgirl.SetActive(true);
        }
    }
}
