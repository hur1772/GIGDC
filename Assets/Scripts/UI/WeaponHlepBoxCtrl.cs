using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHlepBoxCtrl : MonoBehaviour
{
    public GameObject go_Base;

    public Text txt_ItemName;
    public Text txt_ItemDesc;

    int m_WeaponType;

    public void ShowToolTip(int a_WeaponType, Vector3 pos)
    {
        m_WeaponType = a_WeaponType % 2;

        go_Base.SetActive(true);

        pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 0.5f,
                            -go_Base.GetComponent<RectTransform>().rect.height * 0.5f,
                            0);
        go_Base.transform.position = pos;

        if(m_WeaponType == 0)
        {
            txt_ItemName.text = "검 " + (GlobalUserData.SwordTier + 1) + "단계";
        }
        else if (m_WeaponType == 1)
        {
            txt_ItemName.text = "활 " + (GlobalUserData.BowTier + 1) + "단계";
        }

        txt_ItemDesc.text = "데미지 : " + GlobalUserData.m_weaponDataList[a_WeaponType].m_WeaponDamage + "\n크리티컬 확률 : "
            + GlobalUserData.m_weaponDataList[a_WeaponType].m_Critical + "%" + "\n크리티컬 데미지 : " + (GlobalUserData.m_weaponDataList[a_WeaponType].m_CriticalDmg * 100) + "%";
    }

    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
