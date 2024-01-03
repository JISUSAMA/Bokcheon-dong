using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Bokdungi : MonoBehaviour
{
    public GameObject BokdungiOb;
    public GameObject[] Body_Grup;
    public GameObject Find_Grup;
    public GameObject X_Grup;
    public GameObject Found_Grup;
    public GameObject[] Face_Grup;

  //  public Button fail_btn;
  //  public Button success_btn;

    public Animator Bokdungi_Anl;
    private void Awake()
    {
        Bokdungi_Default_motion();
        if (SceneManager.GetActiveScene().name.Equals("01Tutorial"))
        {
            Bokdungi_SetDialog();
            //StartCoroutine(_Bokdungi_Evnet());
        }
        else if (SceneManager.GetActiveScene().name.Equals("04Mission"))
        {
            BokdungiOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1);
        }
        else if (SceneManager.GetActiveScene().name.Equals("05Found"))
        {
            StartCoroutine(_Bokdungi_CompleteDialog());
        }
    }

    //튜토리얼 복둥이!
    public void Bokdungi_Evnet()
    {
        PlayerPrefs.SetString("TL_FriendName", "Bokdungi");
        StartCoroutine(_Bokdungi_Evnet());//복둥이 이벤트
    }
    //복둥이 튜토리얼 시작!
    IEnumerator _Bokdungi_Evnet()
    {
        Bokdungi_SetDialog(); //작성된 다이어로그 String 배열안에 저장
        DialogManager.instance.Dialog_count = 0; //처음 시작 할 때 다이얼로그 초기화 시킴
        int nextNum = DialogManager.instance.Dialog_count + 1; //다음 번호를 저장 함
        DialogManager.instance.Dialog_ing = true; //대사가 나오는 중 
        StartCoroutine(_Bukdungi_FaceChange()); //복둥이 얼굴이 변하도록 스크립트 돌림
        DialogManager.instance.DialogAnimation.SetTrigger("DialogOpenT"); //대화창 열림
        yield return new WaitForSeconds(1);
        DialogManager.instance.Dialog(DialogManager.instance.Dialog_count); //처음 대사 ㄱ;
        StartCoroutine(_Bokdungi_Dialog()); //사운드 나레이션
        while (DialogManager.instance.Dialog_ing)
        {
           DialogManager.instance. Dialog_TM.text = " "; //초기화
            nextNum = DialogManager.instance.Dialog_count + 1;
            //3번째 대화일 경우,사진 수집하는 집하는 방법
            if (DialogManager.instance.Dialog_count.Equals(3))
            {
                SoundManager.Instance.StopBGM();
                DialogManager.instance.Typing_speed = 6f;
                Tutorial_UIManager.instance.ExplainVideo.SetActive(true);
                Tutorial_UIManager.instance.ExplainVideoFinishTime();
               DialogManager.instance.LongDialogStr("네가 들고 있는 이 탭을 우리 세계에서는 시간 수집기라 부르고 있어.\n시간 수집기의 카메라가 켜지면 ",
                   "친구들이 잃어버린 유물과 같은 그림을 찾아서 카메라로 촬영하면 돼.\n아주 간단하지? 먼저 내가 잃어버린 물건을 찾아주겠니?");
               // DialogManager.instance.LongDialogStr();
                yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false); //대화가 끝나고 나면
                 yield return new WaitUntil(() => DialogManager.instance.Dialog_count.Equals(nextNum));
                Tutorial_UIManager.instance.ExplainVideo.SetActive(false);
            }
            else
            {
                //복둥이 이동하고 배경 변경
                if (DialogManager.instance.Dialog_count.Equals(4))
                {
                    DialogManager.instance.Typing_speed = 6f;
                    Tutorial_UIManager.instance.BackgroundIMG[0].SetActive(false);
                    Tutorial_UIManager.instance.BackgroundIMG[1].SetActive(true);
                    ////////////// 찾아줘! //////////////////////////
                    Bokdungi_body(false, true, false);
                    Bokdungi_Gesture(true, false, false);
                    ////////////////////////////////////////////////////
                    BokdungiOb.transform.DOLocalMove(new Vector3(-500, -197, 0), 1);
                    // Bokdungi_Anl.SetBool("Find_b",true);
                    Bokdungi_Anl.SetTrigger("Find");
                }
                if (DialogManager.instance.Dialog_count.Equals(6))
                {
                    Bokdungi_Default_motion();
                    DialogManager.instance.Dialog_ing = false;
                    break;
                }
                yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false);
                DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true);
               // Debug.Log("0---------------------------------------------------4");
                yield return new WaitUntil(() => DialogManager.instance.Dialog_count.Equals(nextNum));
               // Debug.Log("0---------------------------------------------------1");

            }

            yield return null;
        }
        yield return new WaitUntil(() => DialogManager.instance.Dialog_ob.activeSelf.Equals(false));
        Tutorial_UIManager.instance.CollectionOb.SetActive(true);
        yield return new WaitForSeconds(1);
        BokdungiOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1); //안보이게 이동
        SoundFunction.Instance.OpenMissionWindow_sound();
        Tutorial_UIManager.instance.CollectionBtn.gameObject.SetActive(true);
        GameManager.instance.Ongoing = false; //표정 멈춰!
        Debug.Log(BokdungiOb.transform.position);
        yield return null;
    }
    IEnumerator _Bokdungi_Dialog()
    {
        SoundManager.Instance.PlayCharacterDialog("복둥이 1",8);
        yield return new WaitUntil(() => DialogManager.instance.Dialog_count.Equals(1) || DialogManager.instance.Dialog_nextBtn == false);
        SoundManager.Instance.PlayCharacterDialog("복둥이 2", 8);
        yield return new WaitUntil(() => DialogManager.instance.Dialog_count.Equals(2) || DialogManager.instance.Dialog_nextBtn == false);
        SoundManager.Instance.PlayCharacterDialog("복둥이 3", 8);
        yield return new WaitUntil(() => DialogManager.instance.Dialog_count.Equals(3) || DialogManager.instance.Dialog_nextBtn == false);
        //영상 나레이션
        yield return new WaitUntil(() => DialogManager.instance.Dialog_count.Equals(4) || DialogManager.instance.Dialog_nextBtn == false);
        SoundManager.Instance.PlayCharacterDialog("복둥이 5", 5);
        yield return new WaitUntil(() => DialogManager.instance.Dialog_count.Equals(5) || DialogManager.instance.Dialog_nextBtn == false);
        SoundManager.Instance.PlayCharacterDialog("복둥이 6", 5);
        yield return null;
    }
    IEnumerator _Bukdungi_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
          //  Debug.Log("--------------------------------------------facechange2");
            if (DialogManager.instance.Dialog_count.Equals(1))
            {
                Panic_FaceChange();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                Defalt_FaceChange();
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        Bokdungi_Face(true, false, false, false, false);
        yield return null;
    }

    public void Bokdungi_SetDialog()
    {
        DialogManager.instance.Dialog_str = new string[DialogManager.instance.bokdungi_dialog]; //캐릭터가 가지고있는 나레이션 갯수
        DialogManager.instance.Dialog_str[0] = "아 참, 놀랬지? 우선 내 소개부터 할게.\n내 이름은 복둥이라고 해. 우리는 시간버스를 타고 여행하던 중에\n갑자기 버스가 고장 나서 이 곳에 떨어지게 되었어.";
        DialogManager.instance.Dialog_str[1] = "그런데 같이 온 친구 몇 명이 물건을 잃어버렸지 뭐야.\n물건을 찾지 못하면 다시 가야로 돌아갈 수 없어 흑흑.";
        DialogManager.instance.Dialog_str[2] = "혼자서 모든 물건을 찾으려니 힘들어.\n혹시 네가 들고있는 시간 수집기로 나를 도와줄 수 있을까?";
        //사진수집기
        DialogManager.instance.Dialog_str[3] = "";
        DialogManager.instance.Dialog_str[4] = "정말 고마워! 너와 함께 친구들의 물건을 찾기 위해서는\n‘신발 모양 토기'가 꼭 필요해!";
        DialogManager.instance.Dialog_str[5] = "말풍선 안에 있는 물건이 보이지?\n이 물건을 잘 기억하고 한 번 찾아볼래?";
        //찾으러가기!
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void Bokdungi_CompleteDialog()
    {
        StartCoroutine(_Bokdungi_CompleteDialog());
    }
    IEnumerator _Bokdungi_CompleteDialog()
    {
        Bokdungi_Default_motion();//기본 동작
         //   GameManager.instance.Mission_Complete = true;
        DialogManager.instance.Succese_ob.SetActive(true);
        yield return new WaitForSeconds(1f);
        DialogManager.instance.Succese_ob.SetActive(false);
   
        DialogManager.instance.DialogStr("훌륭해! 덕분에 가야로 다시 돌아갈 수 있겠어!\n다른 친구들도 도와주겠니?");
     
        ////////////// 맞았어!!! //////////////////////////
        Bokdungi_body(false, false, true);
        Bokdungi_Gesture(false, false, true);
        StartCoroutine(_Bukdungi_Complete_FaceChange());
        Bokdungi_Anl.SetTrigger("Clear");
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == true); //다음 버튼 눌렀을 경우
        yield return new WaitForSeconds(1.5f);
        DialogManager.instance.ClearStamp_ob.SetActive(true);
        SoundManager.Instance.PlayCharacterDialog("복둥이 7", 6.3f);
        yield return new WaitUntil(() => DialogManager.instance.Dialog_ob.activeSelf.Equals(false));
        PlayerPrefs.SetString("TL_HavePlayBefore", "true");
        SceneManager.LoadScene("03ChooseFriends");

    }
    IEnumerator _Bukdungi_Complete_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Happy_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Bokdungi_Default_motion();
        yield return null;
    }
    public void Bokdungi_FailDialog()
    {
        StartCoroutine(_Bokdungi_FailDialog());
    }
    IEnumerator _Bokdungi_FailDialog()
    {
        DialogManager.instance.Fail_ob.SetActive(true);
        yield return new WaitForSeconds(1f);
        DialogManager.instance.Fail_ob.SetActive(false);
        BokdungiOb.transform.DOLocalMove(new Vector3(787, -197, 0), 1);
        //Bokdungi_Anl.SetBool("X_b",true);
        ////////////// 틀렸어//////////////////////////
        Bokdungi_body(false, false, true); //x
        Bokdungi_Gesture(false, true, false);

        StartCoroutine(_Bukdungi_Fail_FaceChange());
        ///////////////////////////////////////////////////
        DialogManager.instance.MissionDialogStr("열심히 찾아줬는데 내가 찾고 있는 물건은 아니야.\n신발 모양을 다시 한 번 잘 생각해봐!");
        Bokdungi_Anl.SetTrigger("X");
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayCharacterDialog("복둥이 8", 7.7f);
        yield return new WaitUntil(() => DialogManager.instance.MissionDialog_ob.activeSelf.Equals(false));
        Bokdungi_Default_motion();
        BokdungiOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 0.3f); //안보이게 이동
        GameManager.instance.Ongoing = false;
        Mission_UIManager.instance.MissionCheck_sc.noRelicOneShow = false;
        yield return new WaitForSeconds(1f); //4초 후에 활성화 시키기
        Mission_UIManager.instance.TargetGrup.SetActive(true);
    }
    IEnumerator _Bukdungi_Fail_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Panic_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Bokdungi_Default_motion();
        yield return null;
    }
    //얼굴
    public void Bokdungi_Face(bool basice, bool happy1, bool happy2, bool panic1, bool panic2)
    {
        Face_Grup[0].SetActive(basice);
        Face_Grup[1].SetActive(happy1);
        Face_Grup[2].SetActive(happy2);
        Face_Grup[3].SetActive(panic1);
        Face_Grup[4].SetActive(panic2);
        //   Debug.Log(basice+ " "+ happy1 +" "+ happy2 + " "+ panic1+" "+ panic2);
    }
    public void Bokdungi_body(bool basice, bool find, bool x)
    {
        Body_Grup[0].SetActive(basice);
        Body_Grup[1].SetActive(find);
        Body_Grup[2].SetActive(x);
    }
    public void Bokdungi_Gesture(bool find, bool x, bool found)
    {
        Find_Grup.SetActive(find);
        X_Grup.SetActive(x);
        Found_Grup.SetActive(found);
    }
    public void Bokdungi_Default_motion()
    {
        Bokdungi_Face(true, false, false, false, false);
        Bokdungi_Gesture(false, false, false);
        Bokdungi_body(true, false, false);

        //  Bokdungi_Anl.SetBool("X_b", false);
        //  Bokdungi_Anl.SetBool("Clear_b", false);
        //  Bokdungi_Anl.SetBool("Find_b", false);
        //  Bokdungi_Anl.SetBool("Found_b", false);

    }
    int Panic_CurrentRange = -1;
    int Defalt_CurrentRange = -1;
    int Happy_CurrentRange = -1;
    void Panic_FaceChange()
    {
        int Range = Random.Range(0, 2);
        //Debug.Log(Range);
        if (Panic_CurrentRange.Equals(Range))
        {
            if (Range.Equals(0)) { Range = 1; }
            else if (Range.Equals(1)) { Range = 0; }
        }
        if (Range.Equals(0))
        {
            Bokdungi_Face(false, false, false, true, false);
            Panic_CurrentRange = 0;
        }
        else
        {
            Bokdungi_Face(false, false, false, false, true);
            Panic_CurrentRange = 1;
        }
    }
    void Defalt_FaceChange()
    {
        int Range = Random.Range(0, 2);
    //    Debug.Log(Range);
        if (Defalt_CurrentRange.Equals(Range))
        {
            if (Range.Equals(0)) { Range = 1; }
            else if (Range.Equals(1)) { Range = 0; }
        }
        if (Range.Equals(0))
        {
            Bokdungi_Face(true, false, false, false, false);
            Defalt_CurrentRange = 0;
        }
        else
        {
            Bokdungi_Face(false, true, false, false, false);
            Defalt_CurrentRange = 1;
        }
    }
    void Happy_FaceChange()
    {
        int Range = Random.Range(0, 2);
        Debug.Log(Range);
        if (Happy_CurrentRange.Equals(Range))
        {
            if (Range.Equals(0)) { Range = 1; }
            else if (Range.Equals(1)) { Range = 0; }
        }
        if (Range.Equals(0))
        {
            Bokdungi_Face(false, true, false, false, false);
            Happy_CurrentRange = 0;
        }
        else
        {
            Bokdungi_Face(false, false, true, false, false);
            Happy_CurrentRange = 1;
        }
    }
}
