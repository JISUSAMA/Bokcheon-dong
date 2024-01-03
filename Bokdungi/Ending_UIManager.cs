using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System;
public class Ending_UIManager : MonoBehaviour
{
    public GameObject EndingBtnPanel;
    public GameObject FindCanvas_ob;
    public Button Ending_Btn;
    [Header("Video")]
    public VideoPlayer EndingVideo;
    public RenderTexture EndingRTexture;
    private void Awake()
    {
        Ending_Btn.onClick.AddListener(() => EndingVideo_start());
        EndingRTexture.Release();
    }
    void Start()
    {
        StartCoroutine(_FindAll());
    }
    IEnumerator _FindAll()
    {
        SoundManager.Instance.PlaySFX("13 미션성공");
        yield return new WaitForSeconds(1f);
        Ending_Btn.interactable = true;
        yield return null;

    }
    void EndingVideo_start()
    {
        SoundManager.Instance.StopBGM(); //버튼 누르면 사운드 종료!
        EndingBtnPanel.SetActive(false);
        FindCanvas_ob.SetActive(false);

        EndingVideo.gameObject.SetActive(true);
        EndingVideo.Play();
        StartCoroutine(_EndingVideoFinishTime());
    }
    IEnumerator _EndingVideoFinishTime()
    {
        while (true)
        {
            long playerCurrentFrame = EndingVideo.GetComponent<VideoPlayer>().frame;
            long playerFrameCount = Convert.ToInt64(EndingVideo.GetComponent<VideoPlayer>().frameCount);
            if (playerCurrentFrame < playerFrameCount -10)
            {
                //  print("VIDEO IS PLAYING");
            }
            else
            {
                print("VIDEO IS OVER");
                //Do w.e you want to do for when the video is done playing.
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene("00TitleScreen");
            }
            yield return null;
        }
    }
}
