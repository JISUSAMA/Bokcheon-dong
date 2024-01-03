using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Game_Bihwa : MonoBehaviour
{
    [Header("Bihwa")]
    public GameObject BihwaOb;
    public GameObject[] Body_Grup;
    public GameObject Find_Grup;
    public GameObject X_Grup;
    public GameObject Found_Grup;
    public GameObject[] Face_Grup;

    public Animator Bihwa_Ani;

    public Button fail_btn;
    public Button success_btn;
    // Start is called before the first frame update
    void Start()
    {
        Bihwa_Default_motion();
        if (SceneManager.GetActiveScene().name.Equals("02Game"))
        {
            Bihwa_SetDialog();
            StartCoroutine(_Bihwa_Event_Start());
        }
        else if (SceneManager.GetActiveScene().name.Equals("04Mission"))
        {
            BihwaOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1);
        }
        else if (SceneManager.GetActiveScene().name.Equals("05Found"))
        {
            StartCoroutine(_Bihwa_CompleteDialog());
        }
    }

    //처음 시작 이벤트 
    IEnumerator _Bihwa_Event_Start()
    {
        DialogManager.instance.Dialog_ob.SetActive(true);
        DialogManager.instance.Dialog_count = 0;
        int nextNum = DialogManager.instance.Dialog_count + 1;
        StartCoroutine(_Bihwa_FaceChange());
        DialogManager.instance.DialogAnimation.SetTrigger("DialogOpenT"); //대화창 열림
        yield return new WaitForSeconds(1);
        DialogManager.instance.Dialog(DialogManager.instance.Dialog_count); //처음 대사 ㄱ;
        SoundManager.Instance.PlayCharacterDialog("비화 1",4.4f);
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false);
        DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true); //다음으로 가기 버튼
        yield return new WaitUntil(() => DialogManager.instance.Dialog_count.Equals(nextNum));
        //배경 변경 - 찾아줘
        Game_UIManager.instance.Bubble_ob.SetActive(true);
        BihwaOb.transform.DOLocalMove(new Vector3(-500, -197, 0), 1);
        ////////////// 찾아줘! //////////////////////////
        Bihwa_body(false, true, false, false);
        Bihwa_Gesture(true, false, false);
        Bihwa_Ani.SetTrigger("Find");
        SoundManager.Instance.PlayCharacterDialog("비화 1-1", 6.3f);
        ////////////////////////////////////////////////////
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false);
        DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true); //다음으로 가기 버튼
        yield return new WaitUntil(() => DialogManager.instance.Dialog_ob.activeSelf.Equals(false));

        Game_UIManager.instance.Collection_ob.SetActive(true);
        yield return new WaitForSeconds(1);
        SoundFunction.Instance.OpenMissionWindow_sound();
        BihwaOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1); //안보이게 이동
        Bihwa_Default_motion();
        Game_UIManager.instance.CollectionStart_Btn.gameObject.SetActive(true);
        GameManager.instance.Ongoing = false; //표정 멈춰!

        yield return null;
    }
    //미션 성공 "Found Scene"
    public void Bihwa_CompleteDialog()
    {
        StartCoroutine(_Bihwa_CompleteDialog());
    }
    IEnumerator _Bihwa_CompleteDialog()
    {
        SoundFunction.Instance.Found_sound();
        PlayerPrefs.SetString("TL_Bihwa_Clear", "true");
        GameManager.instance.Change_ClearState();
        DialogManager.instance.DialogStr("어머, 내 굽다리 접시! 찾아줘서 고마워.\n보답으로 내가 만든 요리 좀 먹고 갈래?");

        ////////////// 맞았어!!! //////////////////////////
        Bihwa_body(false, false, false, true);
        Bihwa_Gesture(false, false, true);
        StartCoroutine(_Bihwa_Complete_FaceChange());
        Bihwa_Ani.SetTrigger("Clear");
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == true); //다음 버튼 눌렀을 경우
        yield return new WaitForSeconds(1.5f);
        DialogManager.instance.ClearStamp_ob.SetActive(true);
        SoundManager.Instance.PlayCharacterDialog("비화 2", 7);
        yield return new WaitUntil(() => DialogManager.instance.Dialog_ob.activeSelf.Equals(false));
        if (GameManager.instance.Mission_Complete)
        {
            SceneManager.LoadScene("06Ending");
        }
        else
        {
            SceneManager.LoadScene("03ChooseFriends");
        }
        yield return null;
    }
    public void Bihwa_FailDialog()
    {
        StartCoroutine(_Bihwa_FailDialog());
    }
    //미션 실패 "Mission Scene"
    IEnumerator _Bihwa_FailDialog()
    {
        DialogManager.instance.Fail_ob.SetActive(true);
        yield return new WaitForSeconds(1f);
        DialogManager.instance.Fail_ob.SetActive(false);
        BihwaOb.transform.DOLocalMove(new Vector3(787, -197, 0), 1);
        ////////////// 틀렸어//////////////////////////
        Bihwa_body(false, false, true, false); //x
        Bihwa_Gesture(false, true, false);
        StartCoroutine(_Bihwa_Fail_FaceChange());
        ///////////////////////////////////////////////////
        DialogManager.instance.MissionDialogStr("흠, 내가 찾던 물건이랑 모양이 달라.\n보통 접시에 다리가 있는 모양이니까 천천히 찾아봐~");
        Bihwa_Ani.SetTrigger("X");
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayCharacterDialog("비화 3", 8.7f);
        yield return new WaitUntil(() => DialogManager.instance.MissionDialog_ob.activeSelf.Equals(false));
        BihwaOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1); //안보이게 이동
        yield return new WaitForSeconds(1.5f);
        Bihwa_Default_motion();
        GameManager.instance.Ongoing = false;
        Mission_UIManager.instance.MissionCheck_sc.noRelicOneShow = false;
        yield return new WaitForSeconds(1f); //4초 후에 활성화 시키기
        Mission_UIManager.instance.TargetGrup.SetActive(true);
    }

    IEnumerator _Bihwa_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Debug.Log("--------------------------------------------facechange2");
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
        Bihwa_Face(true, false, false, false, false);
        yield return null;

    }
    IEnumerator _Bihwa_Complete_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Happy_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Bihwa_Default_motion();
        yield return null;
    }
    IEnumerator _Bihwa_Fail_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Panic_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Bihwa_Default_motion();
        yield return null;
    }

    int Panic_CurrentRange = -1;
    int Defalt_CurrentRange = -1;
    int Happy_CurrentRange = -1;
    void Panic_FaceChange()
    {
        int Range = Random.Range(0, 2);
        Debug.Log(Range);
        if (Panic_CurrentRange.Equals(Range))
        {
            if (Range.Equals(0)) { Range = 1; }
            else if (Range.Equals(1)) { Range = 0; }
        }
        if (Range.Equals(0))
        {
            Bihwa_Face(false, false, false, true, false);
            Panic_CurrentRange = 0;
        }
        else
        {
            Bihwa_Face(false, false, false, false, true);
            Panic_CurrentRange = 1;
        }
    }
    void Defalt_FaceChange()
    {
        int Range = Random.Range(0, 2);
        Debug.Log(Range);
        if (Defalt_CurrentRange.Equals(Range))
        {
            if (Range.Equals(0)) { Range = 1; }
            else if (Range.Equals(1)) { Range = 0; }
        }
        if (Range.Equals(0))
        {
            Bihwa_Face(true, false, false, false, false);
            Defalt_CurrentRange = 0;
        }
        else
        {
            Bihwa_Face(false, true, false, false, false);
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
            Bihwa_Face(false, true, false, false, false);
            Happy_CurrentRange = 0;
        }
        else
        {
            Bihwa_Face(false, false, true, false, false);
            Happy_CurrentRange = 1;
        }
    }
    public void Bihwa_SetDialog()
    {
        DialogManager.instance.Dialog_str = new string[DialogManager.instance.suro_dialog]; //캐릭터가 가지고있는 나레이션 갯수
        DialogManager.instance.Dialog_str[0] = "안녕, 얘들아! 나는 소문난 요리사 비화라고 해~";
        DialogManager.instance.Dialog_str[1] = "나는 요리하는 데 필요한 소중한 접시를 잃어버렸어.\n꼭 내 접시를 찾아줬으면 좋겠어!";
    }
    public void Bihwa_Face(bool basice, bool happy1, bool happy2, bool panic1, bool panic2)
    {
        Face_Grup[0].SetActive(basice);
        Face_Grup[1].SetActive(happy1);
        Face_Grup[2].SetActive(happy2);
        Face_Grup[3].SetActive(panic1);
        Face_Grup[4].SetActive(panic2);
        //   Debug.Log(basice+ " "+ happy1 +" "+ happy2 + " "+ panic1+" "+ panic2);
    }
    public void Bihwa_body(bool basic, bool find, bool x, bool found)
    {
        Body_Grup[0].SetActive(basic);
        Body_Grup[1].SetActive(find);
        Body_Grup[2].SetActive(x);
        Body_Grup[3].SetActive(found);
    }
    public void Bihwa_Gesture(bool find, bool x, bool found)
    {
        Find_Grup.SetActive(find);
        X_Grup.SetActive(x);
        Found_Grup.SetActive(found);
    }
    public void Bihwa_Default_motion()
    {
        Bihwa_Face(true, false, false, false, false);
        Bihwa_Gesture(false, false, false);
        Bihwa_body(true, false, false, false);
    }
}
