using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HlepBoxCtrl : MonoBehaviour
{
    public GameObject go_Base;

    public Text txt_ItemName;
    public Text txt_ItemDesc;
    public Text txt_ItemHowtoUsed;

    public void ShowToolTip(int a_itemType, Vector3 pos)
    {
        go_Base.SetActive(true);
        pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 0.8f,
                            -go_Base.GetComponent<RectTransform>().rect.height * 0.5f,
                            0);
        go_Base.transform.position = pos;

        txt_ItemName.text = GlobalUserData.m_ItemDataList[a_itemType].m_Name;
        txt_ItemDesc.text = GlobalUserData.m_ItemDataList[a_itemType].m_ItemExp;

        if (txt_ItemHowtoUsed != null)
            txt_ItemHowtoUsed.text = GlobalUserData.m_ItemDataList[a_itemType].m_Help;
    }

    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
