using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//미션 성공, 실패, AR기능 3개 구현
public class Mission_UIManager : MonoBehaviour
{
    public GameObject[] Character_ob;
    public Button PauseBtn;

    public Image DiaLogName_img;
    public Sprite[] DiaLogName_sp;

    public GameObject Mission_Ob;
    public Image Mission_img;
    public Sprite[] Mission_sp;
    public Image MissionDiaLogName_img;

    [Header("In_Mission_Relic")]
    public Image Mission_Relic_img;
    public Sprite[] Mission_Relic_sp;

    public Image Mission_gps_img;
    public Sprite[] Mission_gps_sp;

    public string HelpFriend_name;

    public bool mBackgroundWasSwitchedOff = true;

    public GameObject TargetGrup;
    public MissionCheck MissionCheck_sc;
    public static Mission_UIManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        PauseBtn.onClick.AddListener(() => GameManager.instance.PausePopup.SetActive(true));
        HelpFriend_name = PlayerPrefs.GetString("TL_FriendName"); //게임이 끝나면 지워줘야함
    }
    // Start is called before the first frame update
    void Start()
    {
        Start_Setting_Chareacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
        else if (HelpFriend_name.Equals("Bokdungi")){
            charecter_num = 6;
        }

        Character_ob[charecter_num].SetActive(true);    //캐릭터 셋팅
        DiaLogName_img.sprite = DiaLogName_sp[charecter_num]; //대화창 이름
        Mission_img.sprite = Mission_sp[charecter_num]; //미션 판넬 이미지
        MissionDiaLogName_img.sprite = DiaLogName_sp[charecter_num];    //미션 대화창 이름
        Mission_Relic_img.sprite = Mission_Relic_sp[charecter_num];
        Mission_gps_img.sprite = Mission_gps_sp[charecter_num];
    }
}
