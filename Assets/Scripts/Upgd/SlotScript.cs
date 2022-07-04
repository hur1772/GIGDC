using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
{
    public RawImage SlotBackImg = null;
    public RawImage[] ItemImg = null;
    public RawImage[] ItemResultImg;

    DragAndDrapMgr m_DrMgr = null;

    // Start is called before the first frame update
    void Start()
    {
        m_DrMgr = null;
        GameObject a_drObj = GameObject.Find("UpGdPanel");
        if (a_drObj != null)
        {
            m_DrMgr = a_drObj.GetComponent<DragAndDrapMgr>();
        }

        Debug.Log(SlotBackImg.name);

    }

    // Update is called once per frame
    void Update()
    {
        if (m_DrMgr != null)
        {
            if (IsCollSlot(SlotBackImg.gameObject) == true)
            {
                if(SlotBackImg.name == "Slot_1")
                {
                    if (ItemImg[0].gameObject.activeSelf == true || ItemImg[1].gameObject.activeSelf == true || ItemImg[2].gameObject.activeSelf == true)
                    {
                        m_DrMgr.ShowToolTip(0, transform.position);
                    }
                }
                if (SlotBackImg.name == "Slot_2")
                {
                    if (ItemImg[0].gameObject.activeSelf == true || ItemImg[1].gameObject.activeSelf == true || ItemImg[2].gameObject.activeSelf == true)
                    {
                        m_DrMgr.ShowToolTip(1, transform.position);
                    }
                }
            }
        }
    }

    bool IsCollSlot(GameObject a_CkObj)  //마우스가 UI 슬롯 오브젝트 위에 있느냐? 판단하는 함수
    {
        Vector3[] v = new Vector3[4];
        a_CkObj.GetComponent<RectTransform>().GetWorldCorners(v);
        if (v[0].x <= Input.mousePosition.x && Input.mousePosition.x <= v[2].x &&
           v[0].y <= Input.mousePosition.y && Input.mousePosition.y <= v[2].y)
        {
            return true;
        }

        return false;
    }

}
