using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class MissionCheck : MonoBehaviour
{
    public bool findRelicState;    //유물을 맞게 찾았는지
    public bool noRelicOneShow;    //아니라는 텍스트 한번만 실행
    public TrackableBehaviour[] imgTarget;
    /// <summary>
    /// 복둥이 : 하늘을 나는 신발
    /// 수로 : 고리자루칼
    /// 비화 : 굽다리 접시
    /// 황옥 : 목걸이 
    /// 대로 :  재갈
    /// 고로 : 투구
    /// 말로 : 화로 모양 그릇 받침
    /// </summary>
    /// 
    private void Awake()
    {
        StartCoroutine(_Mission_Start());
    }
    IEnumerator _Mission_Start()
    {
        findRelicState = false;
        //물건을 찾지 못했을 경우,
        while (!findRelicState)
        {
            if (noRelicOneShow == false)
            {
                RelicFindDistinction(); //해당 유물 찾았는지 판별하기 위한 함수
            }
            yield return null;
        }
        yield return null;
    }
    //AR오브젝트가 떳는지 상태확인
    private bool IsTrackingMarker(string imageTargetName)
    {
        var imageTarget = GameObject.Find(imageTargetName);
        var trackable = imageTarget.GetComponent<TrackableBehaviour>();
        var status = trackable.CurrentStatus;
        Debug.Log(status);
        return status == TrackableBehaviour.Status.TRACKED;
    }
    //해당씬에서 해당 유물을 찾았는지의 판별하기 위한 함수
    void RelicFindDistinction()
    {
        //    Debug.Log(PlayerPrefs.GetString("TL_FriendName"));
        //return imgTarget[0].CurrentStatus == TrackableBehaviour.Status.TRACKED;
        if (noRelicOneShow == false)
        {
            if (PlayerPrefs.GetString("TL_FriendName").Equals("Bokdungi"))
            {
                // 복둥이 : 하늘을 나는 신발
                if ((imgTarget[0].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    findRelicState = true;  //해당 씬의 찾은 유물이 맞다
                    RelicAcquirePopup();
                }
                // 수로 : 고리자루칼
                else if ((imgTarget[1].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(1, "고리자루칼"));
                }
                //비화 : 굽다리 접시
                else if ((imgTarget[2].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    Debug.Log("찾앗다");
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(2, "굽다리 접시"));
                }
                //황옥 : 목걸이 
                else if ((imgTarget[3].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(3, "목걸이"));
                }
                //대로 :  재갈
                else if ((imgTarget[4].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))    //해당 씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(4, "재갈"));
                }
                //고로 : 투구
                else if ((imgTarget[5].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(5, "투구"));
                }
                //말로 : 화로 모양 그릇 받침
                else if ((imgTarget[6].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(6, "화로 모양 그릇 받침"));
                }
                //등잔 모양 토기
                else if ((imgTarget[7].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(7, "등잔 모양 토기"));
                }
                //짧은 목 항아리 
                else if ((imgTarget[8].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(8, "짧은 목 항아리"));
                }
            }
            else if (PlayerPrefs.GetString("TL_FriendName").Equals("Suro"))
            {

                // 복둥이 : 하늘을 나는 신발
                if ((imgTarget[0].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))    //해당 씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(0, "하늘을 나는 신발"));
                }
                // 수로 : 고리자루칼
                else if ((imgTarget[1].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    Debug.Log("+++++++++++ =============  ");
                    findRelicState = true;  //해당 씬의 찾은 유물이 맞다
                    RelicAcquirePopup();
                }
                //비화 : 굽다리 접시
                else if ((imgTarget[2].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(2, "굽다리 접시"));
                }
                //황옥 : 목걸이 
                else if ((imgTarget[3].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    Debug.Log("찾앗다");

                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(3, "목걸이"));
                }
                //대로 :  재갈
                else if ((imgTarget[4].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(4, "재갈"));
                }
                //고로 : 투구
                else if ((imgTarget[5].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(5, "투구"));
                }
                //말로 : 화로 모양 그릇 받침
                else if ((imgTarget[6].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(6, "화로 모양 그릇 받침"));
                }
                //등잔 모양 토기
                else if ((imgTarget[7].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(7, "등잔 모양 토기"));
                }
                //짧은 목 항아리 
                else if ((imgTarget[8].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(8, "짧은 목 항아리"));
                }
            }
            else if (PlayerPrefs.GetString("TL_FriendName").Equals("Bihwa"))
            {
                // 복둥이 : 하늘을 나는 신발
                if ((imgTarget[0].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))    //해당 씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(0, "하늘을 나는 신발"));
                }
                // 수로 : 고리자루칼
                else if ((imgTarget[1].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(1, "고리자루칼"));
                }
                //비화 : 굽다리 접시
                else if ((imgTarget[2].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    findRelicState = true;  //해당 씬의 찾은 유물이 맞다
                    RelicAcquirePopup();
                }
                //황옥 : 목걸이 
                else if ((imgTarget[3].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    Debug.Log("찾앗다");
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(3, "목걸이"));
                }
                //대로 :  재갈
                else if ((imgTarget[4].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(4, "재갈"));
                }
                //고로 : 투구
                else if ((imgTarget[5].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(5, "투구"));
                }
                //말로 : 화로 모양 그릇 받침
                else if ((imgTarget[6].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(6, "화로 모양 그릇 받침"));
                }
                //등잔 모양 토기
                else if ((imgTarget[7].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(7, "등잔 모양 토기"));
                }
                //짧은 목 항아리 
                else if ((imgTarget[8].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(8, "짧은 목 항아리"));
                }
            }
            else if (PlayerPrefs.GetString("TL_FriendName").Equals("Hwangok"))
            {
                // 복둥이 : 하늘을 나는 신발
                if ((imgTarget[0].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))    //해당 씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(0, "하늘을 나는 신발"));
                }
                // 수로 : 고리자루칼
                else if ((imgTarget[1].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(1, "고리자루칼"));
                }
                //비화 : 굽다리 접시
                else if ((imgTarget[2].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    Debug.Log("찾앗다");
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(2, "굽다리 접시"));
                }
                //황옥 : 목걸이 
                else if ((imgTarget[3].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    findRelicState = true;  //해당 씬의 찾은 유물이 맞다
                    RelicAcquirePopup();
                }
                //대로 :  재갈
                else if ((imgTarget[4].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(4, "재갈"));
                }
                //고로 : 투구
                else if ((imgTarget[5].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(5, "투구"));
                }
                //말로 : 화로 모양 그릇 받침
                else if ((imgTarget[6].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(6, "화로 모양 그릇 받침"));
                }
                //등잔 모양 토기
                else if ((imgTarget[7].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(7, "등잔 모양 토기"));
                }
                //짧은 목 항아리 
                else if ((imgTarget[8].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(8, "짧은 목 항아리"));
                }
            }
            else if (PlayerPrefs.GetString("TL_FriendName").Equals("Daero"))
            {
                // 복둥이 : 하늘을 나는 신발
                if ((imgTarget[0].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))    //해당 씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(0, "하늘을 나는 신발"));
                }
                // 수로 : 고리자루칼
                else if ((imgTarget[1].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(1, "고리자루칼"));
                }
                //비화 : 굽다리 접시
                else if ((imgTarget[2].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    Debug.Log("찾앗다");
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(2, "굽다리 접시"));
                }
                //황옥 : 목걸이 
                else if ((imgTarget[3].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(3, "목걸이"));
                }
                //대로 :  재갈
                else if ((imgTarget[4].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    findRelicState = true;  //해당 씬의 찾은 유물이 맞다
                    RelicAcquirePopup();
                }
                //고로 : 투구
                else if ((imgTarget[5].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(5, "투구"));
                }
                //말로 : 화로 모양 그릇 받침
                else if ((imgTarget[6].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(6, "화로 모양 그릇 받침"));
                }
                //등잔 모양 토기
                else if ((imgTarget[7].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(7, "등잔 모양 토기"));
                }
                //짧은 목 항아리 
                else if ((imgTarget[8].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(8, "짧은 목 항아리"));
                }
            }
            else if (PlayerPrefs.GetString("TL_FriendName").Equals("Goro"))
            {
                // 복둥이 : 하늘을 나는 신발
                if ((imgTarget[0].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))    //해당 씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(0, "하늘을 나는 신발"));
                }
                // 수로 : 고리자루칼
                else if ((imgTarget[1].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(1, "고리자루칼"));
                }
                //비화 : 굽다리 접시
                else if ((imgTarget[2].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    Debug.Log("찾앗다");
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(2, "굽다리 접시"));
                }
                //황옥 : 목걸이 
                else if ((imgTarget[3].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(3, "목걸이"));
                }
                //대로 :  재갈
                else if ((imgTarget[4].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(4, "재갈"));
                }
                //고로 : 투구
                else if ((imgTarget[5].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    findRelicState = true;  //해당 씬의 찾은 유물이 맞다
                    RelicAcquirePopup();
                }
                //말로 : 화로 모양 그릇 받침
                else if ((imgTarget[6].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(6, "화로 모양 그릇 받침"));
                }
                //등잔 모양 토기
                else if ((imgTarget[7].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(7, "등잔 모양 토기"));
                }
                //짧은 목 항아리 
                else if ((imgTarget[8].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(8, "짧은 목 항아리"));
                }
            }
            else if (PlayerPrefs.GetString("TL_FriendName").Equals("Malro"))
            {
                // 복둥이 : 하늘을 나는 신발
                if ((imgTarget[0].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))    //해당 씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(0, "하늘을 나는 신발"));
                }
                // 수로 : 고리자루칼
                else if ((imgTarget[1].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(1, "고리자루칼"));
                }
                //비화 : 굽다리 접시
                else if ((imgTarget[2].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    Debug.Log("찾앗다");
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(2, "굽다리 접시"));
                }
                //황옥 : 목걸이 
                else if ((imgTarget[3].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(3, "목걸이"));
                }
                //대로 :  재갈
                else if ((imgTarget[4].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(4, "재갈"));
                }
                //고로 : 투구
                else if ((imgTarget[5].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(5, "투구"));
                }
                //말로 : 화로 모양 그릇 받침
                else if ((imgTarget[6].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    findRelicState = true;  //해당 씬의 찾은 유물이 맞다
                    RelicAcquirePopup();
                }
                //등잔 모양 토기
                else if ((imgTarget[7].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(7, "등잔 모양 토기"));
                }
                //짧은 목 항아리 
                else if ((imgTarget[8].CurrentStatus == TrackableBehaviour.Status.TRACKED).Equals(true))
                {
                    if (noRelicOneShow.Equals(false))   //해당씬의 찾은 유물이 아니다
                        StartCoroutine(RelicNoFind(8, "짧은 목 항아리"));
                }
            }
        }

    }
    //유물획득 시 획득 팝업
    public void RelicAcquirePopup()
    {
        StartCoroutine(_RelicAcquirePopup());
    }
    IEnumerator _RelicAcquirePopup()
    {
        findRelicState = true;
        Mission_UIManager.instance.mBackgroundWasSwitchedOff = true;
        yield return new WaitForSeconds(3f); //4초 후에 활성화 시키기
        DialogManager.instance.Succese_ob.SetActive(true);
        SoundFunction.Instance.Clear_sound();
        yield return new WaitForSeconds(1f);
        DialogManager.instance.Succese_ob.SetActive(false);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("05Found");

        yield return null;
    }

    //해당씬의 해당 유물이 아닌 다른 유물을 찾았을 경우 텍스트 함수
    IEnumerator RelicNoFind(int num, string name)
    {
        if (Mission_UIManager.instance.TargetGrup.activeSelf.Equals(true))
        {
            noRelicOneShow = true;
            yield return new WaitForSeconds(4f); //4초 후에 활성화 시키기
            //업데이트에 들어가 있어서 한번만 적용하기 위해서 ture로 변경
            Mission_UIManager.instance.TargetGrup.SetActive(false);
            yield return new WaitForSeconds(1f); //4초 후에 활성화 시키기
            SoundFunction.Instance.Fail_sound();
            if (PlayerPrefs.GetString("TL_FriendName").Equals("Bokdungi")) { GameManager.instance.Bokdungi_sc.Bokdungi_FailDialog(); }
            else if (PlayerPrefs.GetString("TL_FriendName").Equals("Suro")) { GameManager.instance.Suro_sc.Suro_FailDialog(); }
            else if (PlayerPrefs.GetString("TL_FriendName").Equals("Bihwa")) { GameManager.instance.Bihwa_sc.Bihwa_FailDialog(); }
            else if (PlayerPrefs.GetString("TL_FriendName").Equals("Hwangok")) { GameManager.instance.Hwangok_sc.Hwangok_FailDialog(); }
            else if (PlayerPrefs.GetString("TL_FriendName").Equals("Daero")) { GameManager.instance.Daero_sc.Daero_FailDialog(); }
            else if (PlayerPrefs.GetString("TL_FriendName").Equals("Goro")) { GameManager.instance.Goro_sc.Goro_FailDialog(); }
            else if (PlayerPrefs.GetString("TL_FriendName").Equals("Malro")) { GameManager.instance.Malro_sc.Malro_FailDialog(); }

        }
        yield return null;
    }

}
