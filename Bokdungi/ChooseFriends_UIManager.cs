
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChooseFriends_UIManager : MonoBehaviour
{

    public GameObject[] Friends;
    public Button[] Friends_btn;
    public Button PauseBtn;

    public GameObject FindPopup_Ob;  //Suro, Bihwa , Hwangok, Daero, Goro, malro
    public Button FindPopup_CloseBtn;

    public TextMeshProUGUI FindPopup_text;
    public Button FindPopup_HelpBtn;

    string HelpFriend_name;
    void Awake()
    {
        PauseBtn.onClick.AddListener(() => PauseBtn_sound());
        Friends_btn[0].onClick.AddListener(() =>OnClick_Friend("Suro"));
        Friends_btn[1].onClick.AddListener(() =>OnClick_Friend("Bihwa"));
        Friends_btn[2].onClick.AddListener(() =>OnClick_Friend("Hwangok"));
        Friends_btn[3].onClick.AddListener(() =>OnClick_Friend("Daero"));
        Friends_btn[4].onClick.AddListener(() =>OnClick_Friend("Goro"));
        Friends_btn[5].onClick.AddListener(() => OnClick_Friend("Malro"));

        //FInd BTN
        FindPopup_CloseBtn.onClick.AddListener(() => FindBtn_sound());
        FindPopup_HelpBtn.onClick.AddListener(() => Load_Game(HelpFriend_name)); //도와줄 친구, 

        if (!PlayerPrefs.HasKey("TL_Suro_Clear")) { PlayerPrefs.SetString("TL_Suro_Clear", "false"); }
        if (!PlayerPrefs.HasKey("TL_Bihwa_Clear")) { PlayerPrefs.SetString("TL_Bihwa_Clear", "false"); }
        if (!PlayerPrefs.HasKey("TL_Hwangok_Clear")) { PlayerPrefs.SetString("TL_Hwangok_Clear", "false"); }
        if (!PlayerPrefs.HasKey("TL_Daero_Clear")) { PlayerPrefs.SetString("TL_Daero_Clear", "false"); }
        if (!PlayerPrefs.HasKey("TL_Goro_Clear")) { PlayerPrefs.SetString("TL_Goro_Clear", "false"); }
        if (!PlayerPrefs.HasKey("TL_Malro_Clear")) { PlayerPrefs.SetString("TL_Malro_Clear", "false"); }

        SoundManager.Instance.PlayBGM("Bokdungi_Main");

    }
    //사운드
    void PauseBtn_sound()
    {
        SoundFunction.Instance.Click_sound();
        GameManager.instance.PausePopup.SetActive(true);
    }
    void FindBtn_sound()
    {
        SoundFunction.Instance.Click_sound();
        FindPopup_Ob.SetActive(false);
    }

    void Start()
    {
        Change_ClearState(); //캐릭터 셋팅
    }
    void Change_ClearState()
    {
        string[] stateString = new string[6];
        bool[] stateBool = new bool[6];
        stateString[0] = PlayerPrefs.GetString("TL_Suro_Clear");
        stateString[1] = PlayerPrefs.GetString("TL_Bihwa_Clear");
        stateString[2] = PlayerPrefs.GetString("TL_Hwangok_Clear");
        stateString[3] = PlayerPrefs.GetString("TL_Daero_Clear");
        stateString[4] = PlayerPrefs.GetString("TL_Goro_Clear");
        stateString[5] = PlayerPrefs.GetString("TL_Malro_Clear");
      
        for (int i = 0; i < stateBool.Length; i++) {
            stateBool[i] = System.Convert.ToBoolean(stateString[i]);
        }
        Clear(stateBool[0], stateBool[1], stateBool[2], stateBool[3], stateBool[4], stateBool[5]);
        Debug.Log(stateBool[0]);
    }
    public void OnClick_Friend(string name)
    {
        SoundFunction.Instance.Click_sound();
        FindPopup_Ob.SetActive(true);
        FindPopup_text.text = "";//초기화

        if (name.Equals("Suro")) { FindPopup_text.text = "'수로' 의 물건을 함께\n찾아줄까요?";  }
        else if (name.Equals("Bihwa")) { FindPopup_text.text = "'비화' 의 물건을 함께\n찾아줄까요?";  }
        else if (name.Equals("Hwangok")) { FindPopup_text.text = "'황옥' 의 물건을 함께\n찾아줄까요?"; }
        else if (name.Equals("Daero")) { FindPopup_text.text = "'대로' 의 물건을 함께\n찾아줄까요?"; }
        else if (name.Equals("Goro")) { FindPopup_text.text = "'고로' 의 물건을 함께\n찾아줄까요?"; }
        else if (name.Equals("Malro")) { FindPopup_text.text = "'말로' 의 물건을 함께\n찾아줄까요?"; }

        HelpFriend_name = name; 
 
    }
    void Load_Game(string name)
    {
        SoundFunction.Instance.Click_sound();
        PlayerPrefs.SetString("TL_FriendName", name);
        SceneManager.LoadScene("02Game");
    }
    public void Clear(bool suro, bool bihwa, bool hwangok, bool daero, bool goro, bool malro )
    {
        Friends[0].transform.GetChild(0).gameObject.SetActive(suro);
        Friends[1].transform.GetChild(0).gameObject.SetActive(bihwa);
        Friends[2].transform.GetChild(0).gameObject.SetActive(hwangok);
        Friends[3].transform.GetChild(0).gameObject.SetActive(daero);
        Friends[4].transform.GetChild(0).gameObject.SetActive(goro);
        Friends[5].transform.GetChild(0).gameObject.SetActive(malro);
    }
}
