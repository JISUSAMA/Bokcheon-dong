using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Title_UIManager : MonoBehaviour
{
    [Header("Title")]
    public GameObject [] GameTitleOb;
    public GameObject [] GameTitle_img;
    public Button []startBtn; //게임 시작 버틑 두개
    public Button ContinueBtn; //이어하기 버튼
    [Header("Video")]
    public GameObject introVideo_ob;
    public VideoPlayer IntroVideo;
    public RenderTexture IntroVideoRTexture;
    public Button SkipBtn;
   
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.Change_ClearState();
        IntroVideoRTexture.Release();
        GameTitleOb[2].SetActive(true);
    //    introVideo_ob.gameObject.SetActive(false);
        if (!PlayerPrefs.HasKey("TL_HavePlayBefore"))
        {
            GameTitleOb[0].gameObject.SetActive(true);
            GameTitle_img[0].transform.DOLocalMoveY(260, 0.8f).SetLoops(-1, LoopType.Yoyo);
           startBtn[0].onClick.AddListener(() => IntroVideoSrart());
        }
        else
        {
            GameTitleOb[1].gameObject.SetActive(true);
            GameTitle_img[1].transform.DOLocalMoveY(25, 0.8f).SetLoops(-1, LoopType.Yoyo);
            startBtn[1].onClick.AddListener(() => IntroVideoSrart());
            ContinueBtn.onClick.AddListener(() => ConinueBtn());
        }
        SoundManager.Instance.PlayBGM("Bokdungi_Main2", 0.5f);
    }

    //시작버튼 누르면 인트로 영상시작!
    void IntroVideoSrart()
    {
        GameTitleOb[2].SetActive(false);
        SoundManager.Instance.StopBGM();
         SoundFunction.Instance.Click_sound();
        PlayerPrefs.DeleteAll();
        GameTitleOb[0].SetActive(false);
        introVideo_ob.gameObject.SetActive(true);
        IntroVideo.Play();
        IntroVideoFinishTime(); //튜토리얼 시작!
        SkipBtn.onClick.AddListener(() => Click_TitleSkipBtn());
   
    }
    void Click_TitleSkipBtn()
    {
        SoundFunction.Instance.Click_sound();
        SceneManager.LoadScene("01Tutorial");
    }
    public  void IntroVideoFinishTime()
    {
        StartCoroutine(_IntroVideoFinishTime());
    }
   IEnumerator _IntroVideoFinishTime()
    {
        while (true)
        {
            long playerCurrentFrame = IntroVideo.GetComponent<VideoPlayer>().frame;
            long playerFrameCount = Convert.ToInt64(IntroVideo.GetComponent<VideoPlayer>().frameCount);
            if (playerCurrentFrame < playerFrameCount - 50)
            {
                print("VIDEO IS PLAYING");
            }
            else
            {
                print("VIDEO IS OVER");
                //Do w.e you want to do for when the video is done playing.
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("01Tutorial");

            }
            yield return null;
        }
    }
    void ConinueBtn()
    {
        
        if (GameManager.instance.Mission_Complete)
        {
            SoundFunction.Instance.Click_sound();
            SceneManager.LoadScene("06Ending");
        }
        else
        {
            SoundFunction.Instance.Click_sound();
            SceneManager.LoadScene("03ChooseFriends");
        }
    
    }
}
