using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Found_UIManager : MonoBehaviour
{
    public GameObject[] Character_ob; //캐릭터 오브젝트
    public Image Background_img;
    public Sprite[] Background_sp;

    //대화창 이름
    public Image DiaLogName_img;
    public Sprite[] DiaLogName_sp;

    public Button PauseBtn;

    public string HelpFriend_name;

    // Start is called before the first frame update
    void Start()
    {
        PauseBtn.onClick.AddListener(() => GameManager.instance.PausePopup.SetActive(true));
        HelpFriend_name = PlayerPrefs.GetString("TL_FriendName");
        Start_Setting_Chareacter();
    }
    //2021-11-17 MissionScene 이동
    public void Start_Setting_Chareacter()
    {
        int charecter_num = 0;
        if (HelpFriend_name.Equals("Suro"))
        {
            charecter_num = 0;
        }
        else if (HelpFriend_name.Equals("Bihwa"))
        {
            charecter_num = 1;
        }
        else if (HelpFriend_name.Equals("Hwangok"))
        {
            charecter_num = 2;
        }
        else if (HelpFriend_name.Equals("Daero"))
        {
            charecter_num = 3;
        }
        else if (HelpFriend_name.Equals("Goro"))
        {
            charecter_num = 4;
        }
        else if (HelpFriend_name.Equals("Malro"))
        {
            charecter_num = 5;
        }
        else if (HelpFriend_name.Equals("Bokdungi"))
        {
            charecter_num = 6;
        }
        Character_ob[charecter_num].SetActive(true);    //캐릭터 셋팅
        Background_img.sprite = Background_sp[charecter_num];   //배경 이미지 셋팅
        DiaLogName_img.sprite = DiaLogName_sp[charecter_num]; //대화창 이름
    }
}
