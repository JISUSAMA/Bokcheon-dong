using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Game_UIManager : MonoBehaviour
{

    //Suro, Bihwa , Hwangok, Daero, Goro, malro

    public static Game_UIManager instance { get; private set; }
    public string HelpFriend_name; //이번 게임의 주인공 이름
    public GameObject[] Character_ob;
    public Image Background_img;
    public GameObject Bubble_ob;
    public Sprite[] Background_sp;

    [Header("In_Bubble_Relic")]
    public Image Relic_img; 
    public Sprite []Relic_sp; 

    public Button PauseBtn;


     [Header("Collection")]
     public GameObject Collection_ob;
     public Image CollectionTitle;
     public Sprite[] CollectionTitle_sp;
     public Image Collection_Relic_img;
     public Sprite[] Collection_Relic_sp;
     public Button CollectionStart_Btn;
     public Sequence mySequence;

    ////// MissionScene으로 이동 ///////////////
    public Image DiaLogName_img;
    public Sprite[] DiaLogName_sp;

    public GameObject Mission_Ob;
    public Image Mission_img;
    public Sprite[] Mission_sp;
    public Image MissionDiaLogName_img;

    [Header("In_Mission_Relic")]
    public Image Mission_Relic_img;
    public Sprite []Mission_Relic_sp;

    ////////////////////////////////////////////
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        PauseBtn.onClick.AddListener(() => GameManager.instance.PausePopup.SetActive(true));
        CollectionStart_Btn.onClick.AddListener(() => OnClick_CollectionStartBtn());
        HelpFriend_name = PlayerPrefs.GetString("TL_FriendName"); //게임이 끝나면 지워줘야함
      

    }

    private void Start()
    {
        Start_Setting_Chareacter(); // 캐릭터 배경, 오브젝트 활성화
    }

    void OnEnable()
    {
        mySequence.Restart();
    }

    //2021-11-17 MissionScene 이동
    public void Start_Setting_Chareacter()
    {
        int charecter_num =0;
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
        Relic_img.sprite = Relic_sp[charecter_num]; //말풍선 안 이미지 변경
        CollectionTitle.sprite = CollectionTitle_sp[charecter_num]; //찾아야하는 유물 타이틀
        Collection_Relic_img.sprite = Collection_Relic_sp[charecter_num]; //찾아야하는 유물 이미지 
        DiaLogName_img.sprite = DiaLogName_sp[charecter_num]; //대화창 이름
  
    }
    public void OnClick_CollectionStartBtn()
    {
        //2021-11-17
        SoundFunction.Instance.Click_sound();
        SceneManager.LoadScene("04Mission");
   
    }
}
