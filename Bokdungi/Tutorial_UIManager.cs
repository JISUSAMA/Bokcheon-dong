using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
public class Tutorial_UIManager : MonoBehaviour
{
    public static Tutorial_UIManager instance { get; private set;  }
    public GameObject[] BackgroundIMG; //캐릭터 배경화면 
    public GameObject ExplainVideo; //시간수집기 이용방법 영상
    public GameObject Stamp;
 //   public GameObject DialogOb;
    [Header("Collection")]
    public GameObject CollectionOb;
    public Button CollectionBtn;
    public GameObject MissionOb; 
    [Header("Video")]
    public VideoPlayer ExplainVideo_vp;
    public RenderTexture ExplainRTexture;

    public Button PauseBtn;
    public Bokdungi bokdungi_sc;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        SoundManager.Instance.PlayBGM("Bokdungi_Main");
        ExplainRTexture.Release(); //영상 초기화
        CollectionBtn.onClick.AddListener(() => OnClick_Collection_StartBtn()); //'찾으러가기' 버튼
        PauseBtn.onClick.AddListener(() =>GameManager.instance.PausePopup.SetActive(true)); //일시정지 버튼
    }
    private void Start()
    {

        if(SceneManager.GetActiveScene().name.Equals("01Tutorial"))
            TutorialStart(); ///안녕!
    }
    public void TutorialStart()
    {
        StartCoroutine(_TutorialStart());
    }
    IEnumerator _TutorialStart()
    { 
        if (SceneManager.GetActiveScene().name.Equals("01Tutorial"))
        {
            DialogManager.instance.Dialog_ob.SetActive(true);
            bokdungi_sc.Bokdungi_Evnet(); //복둥이 이벤트 시작
        }
        else if (SceneManager.GetActiveScene().name.Equals("04Mission"))
        {
            bokdungi_sc.transform.DOLocalMove(new Vector3(1660, -197, 0), 1);
        }
        yield return null;
    
    }
   //시간 수집기 영상
    public void ExplainVideoFinishTime()
    {
        StartCoroutine(_ExplainVideoFinishTime());
    }
    IEnumerator _ExplainVideoFinishTime()
    {
        while (true)
        {
            long playerCurrentFrame = ExplainVideo_vp.GetComponent<VideoPlayer>().frame;
            long playerFrameCount = Convert.ToInt64(ExplainVideo_vp.GetComponent<VideoPlayer>().frameCount);
            if (playerCurrentFrame < playerFrameCount - 10)
            {
           //     print("VIDEO IS PLAYING");
            }
            else
            {
                //   print("VIDEO IS OVER");
                yield return new WaitForSeconds(1f);
                DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true);
                SoundManager.Instance.PlayBGM("Bokdungi_Main");
                break;
            }
            yield return null;
        }
    }
    public void OnClick_Collection_StartBtn()
    {
        /*    CollectionOb.SetActive(false);
            MissionOb.SetActive(true);
            for(int i =0; i<BackgroundIMG.Length; i++)
            {
                BackgroundIMG[i].SetActive(false);
            }
            GameManager.instance.mBackgroundWasSwitchedOff = false;*/
        //2021-11-17
        SoundFunction.Instance.Click_sound();
        PlayerPrefs.SetString("TL_FriendName", "Bokdungi");
        SceneManager.LoadScene("04Mission");

    }
}
