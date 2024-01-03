using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public  class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameObject PausePopup;
    public GameObject ExitPopup;
    public bool Ongoing = false;

    [Header("Charactoer_Script")]
    public Bokdungi Bokdungi_sc;
    public Game_Suro Suro_sc;
    public Game_Bihwa Bihwa_sc;
    public Game_Hwangok Hwangok_sc;
    public Game_Daero Daero_sc;
    public Game_Goro Goro_sc;
    public Game_Malro  Malro_sc;


    public string beforeSceneName;
    bool bPaused = false;
    public bool Mission_Complete = false; //모든 미션이 끝났을 경우, 엔딩씬으로 이동
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //2021-11-23
        if (!PlayerPrefs.HasKey("SceneNameCheck"))
        {
            PlayerPrefs.SetString("SceneNameCheck", SceneManager.GetActiveScene().name);
        }
        else
        {
            beforeSceneName = PlayerPrefs.GetString("SceneNameCheck");
            //현재 SceneNameCheck 와 기존 SceneNameChec
            if (!beforeSceneName.Equals(SceneManager.GetActiveScene().name))
            {
                SoundManager.Instance.dialog.Stop(); //대화 정지
                PlayerPrefs.SetString("SceneNameCheck", SceneManager.GetActiveScene().name); //기존에 있던 SceneNameCheck을 현재 SceneNameCheck으로 바꿈
            }
        }

        Debug.Log("SceneNameCheck  : " + PlayerPrefs.GetString("SceneNameCheck"));
        Debug.Log("beforeSceneName  : " + beforeSceneName);
        Debug.Log("currentSceneName  : " + SceneManager.GetActiveScene().name);
    }

    public void Change_ClearState()
    {
        string[] stateString = new string[6];
        bool[] stateBool = new bool[6];
        stateString[0] = PlayerPrefs.GetString("TL_Suro_Clear");
        stateString[1] = PlayerPrefs.GetString("TL_Bihwa_Clear");
        stateString[2] = PlayerPrefs.GetString("TL_Hwangok_Clear");
        stateString[3] = PlayerPrefs.GetString("TL_Daero_Clear");
        stateString[4] = PlayerPrefs.GetString("TL_Goro_Clear");
        stateString[5] = PlayerPrefs.GetString("TL_Malro_Clear");

        if(stateString[0].Equals("true") && stateString[1].Equals("true") &&stateString[2].Equals("true")
            && stateString[3].Equals("true") && stateString[4].Equals("true") && stateString[5].Equals("true"))
        {
            Mission_Complete = true;
        }
    }
    //2021-11-23
 /*   void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // 종료 하시겠습니까? 팝업
                ExitPopup.SetActive(true);
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // 종료 하시겠습니까? 팝업
                ExitPopup.SetActive(true);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            SoundFunction.Instance.TouchScreen_sound();

        }
#if UNITY_ANDROID || UNITY_IOS

        // Touch myTouch = Input.GetTouch(0);
        Touch[] myTouches = Input.touches;
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 0)
            {
                SoundFunction.Instance.TouchScreen_sound();
            }
        }
#endif  

    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            bPaused = true;
            // todo : 어플리케이션을 내리는 순간에 처리할 행동들 /
        }
        else
        {
            if (bPaused)
            {
                bPaused = false;
                //todo : 내려놓은 어플리케이션을 다시 올리는 순간에 처리할 행동들 
                if (SceneManager.GetActiveScene().name.Equals("00TitleScreen"))
                {
                //    ExitPopup.SetActive(true);
                }
                else
                {
                   PausePopup.SetActive(true);
                }
            }
        }
    }
*/
}
