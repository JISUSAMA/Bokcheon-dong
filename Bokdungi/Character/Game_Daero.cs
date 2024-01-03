using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Daero : MonoBehaviour
{
    [Header("Daero")]
    public GameObject DaeroOb;
    public GameObject[] Body_Grup;
    public GameObject Find_Grup;
    public GameObject X_Grup;
    public GameObject[] Face_Grup;

    public Animator Daero_Ani;
    public Button fail_btn;
    public Button success_btn;

    // Start is called before the first frame update
    void Start()
    {
        Daero_Default_motion();
        if (SceneManager.GetActiveScene().name.Equals("02Game"))
        {
            Daero_SetDialog();
            StartCoroutine(_Daero_Event_Start());
        }
        else if (SceneManager.GetActiveScene().name.Equals("04Mission"))
        {
            DaeroOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1);
        }
        else if (SceneManager.GetActiveScene().name.Equals("05Found"))
        {
            StartCoroutine(_Daero_CompleteDialog());
        }


    }
    public void Daero_SetDialog()
    {
        DialogManager.instance.Dialog_str = new string[DialogManager.instance.suro_dialog]; //캐릭터가 가지고있는 나레이션 갯수
        DialogManager.instance.Dialog_str[0] = "이럇! 안녕! 난 대로라고 해.";
        DialogManager.instance.Dialog_str[1] = "말을 길들이기 위해선 말의 입에 씌울 재갈이 필요한데 찾아줬으면 좋겠어.";
    }
    //처음 시작 이벤트 
    IEnumerator _Daero_Event_Start()
    {
        DialogManager.instance.Dialog_ob.SetActive(true);
        DialogManager.instance.Dialog_count = 0;
        int nextNum = DialogManager.instance.Dialog_count + 1;
        StartCoroutine(_Daero_FaceChange());
        DialogManager.instance.DialogAnimation.SetTrigger("DialogOpenT"); //대화창 열림
        yield return new WaitForSeconds(1);
        DialogManager.instance.Dialog(DialogManager.instance.Dialog_count); //처음 대사 ㄱ;
        SoundManager.Instance.PlayCharacterDialog("대로 1",4);
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false);
        DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true); //다음으로 가기 버튼
        yield return new WaitUntil(() => DialogManager.instance.Dialog_count.Equals(nextNum));
        //배경 변경 - 찾아줘
        Game_UIManager.instance.Bubble_ob.SetActive(true);
        DaeroOb.transform.DOLocalMove(new Vector3(-500, -197, 0), 1);
     
        ////////////// 찾아줘! //////////////////////////
        Daero_body(false, true, false, false);
        Daero_Gesture(true, false, false);
        Daero_Ani.SetTrigger("Find");
        SoundManager.Instance.PlayCharacterDialog("대로 1-1",6);
        ////////////////////////////////////////////////////
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false);
        DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true); //다음으로 가기 버튼
        yield return new WaitUntil(() => DialogManager.instance.Dialog_ob.activeSelf.Equals(false));
        Game_UIManager.instance.Collection_ob.SetActive(true);
        yield return new WaitForSeconds(1);
        SoundFunction.Instance.OpenMissionWindow_sound();
        DaeroOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1); //안보이게 이동
        Daero_Default_motion();
        Game_UIManager.instance.CollectionStart_Btn.gameObject.SetActive(true);
        GameManager.instance.Ongoing = false; //표정 멈춰!
        yield return null;
    }
    //미션 성공
    public void Daero_CompleteDialog()
    {
        StartCoroutine(_Daero_CompleteDialog());
    }
    IEnumerator _Daero_CompleteDialog()
    {
        SoundFunction.Instance.Found_sound();
        PlayerPrefs.SetString("TL_Daero_Clear", "true");
        GameManager.instance.Change_ClearState();
   
        DialogManager.instance.DialogStr("우와 바로 찾은 거야? 진짜 대단하다! 나중에 말 타고 싶으면 언제든지 말해.");
        ////////////// 맞았어!!! //////////////////////////
        Daero_body(false, false, false, true);
        Daero_Gesture(false, false, true);
        StartCoroutine(_Daero_Complete_FaceChange());
        Daero_Ani.SetTrigger("Clear");
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == true); //다음 버튼 눌렀을 경우
        yield return new WaitForSeconds(1.5f);
        DialogManager.instance.ClearStamp_ob.SetActive(true);
        SoundManager.Instance.PlayCharacterDialog("대로 2", 7.8f);
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
    public void Daero_FailDialog()
    {
        StartCoroutine(_Daero_FailDialog());
    }
    //미션 실패 
    IEnumerator _Daero_FailDialog()
    {
        DialogManager.instance.Fail_ob.SetActive(true);
        yield return new WaitForSeconds(1f);
        DialogManager.instance.Fail_ob.SetActive(false);
        DaeroOb.transform.DOLocalMove(new Vector3(787, -197, 0), 1);
        ////////////// 틀렸어//////////////////////////
        Daero_body(false, false, true, false); //x
        Daero_Gesture(false, true, false);
        StartCoroutine(_Daero_Fail_FaceChange());
        ///////////////////////////////////////////////////
        DialogManager.instance.MissionDialogStr("음 아쉽게도 내가 찾는 물건은 아니야.\n재갈에는 사다리 모양의 판이 있어! 다시 살펴봐!");
        Daero_Ani.SetTrigger("X");
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayCharacterDialog("대로 3", 8f);
        yield return new WaitUntil(() => DialogManager.instance.MissionDialog_ob.activeSelf.Equals(false));
        DaeroOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1); //안보이게 이동
        yield return new WaitForSeconds(1.5f);
        Daero_Default_motion();
        GameManager.instance.Ongoing = false;
        Mission_UIManager.instance.MissionCheck_sc.noRelicOneShow = false;
        yield return new WaitForSeconds(1f); //4초 후에 활성화 시키기
        Mission_UIManager.instance.TargetGrup.SetActive(true);
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
            Daero_Face(false, false, false, true, false);
            Panic_CurrentRange = 0;
        }
        else
        {
            Daero_Face(false, false, false, false, true);
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
            Daero_Face(true, false, false, false, false);
            Defalt_CurrentRange = 0;
        }
        else
        {
            Daero_Face(false, true, false, false, false);
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
            Daero_Face(false, true, false, false, false);
            Happy_CurrentRange = 0;
        }
        else
        {
            Daero_Face(false, false, true, false, false);
            Happy_CurrentRange = 1;
        }
    }
    IEnumerator _Daero_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
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
        Daero_Face(true, false, false, false, false);
        yield return null;

    }

    IEnumerator _Daero_Complete_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Happy_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Daero_Default_motion();
        yield return null;
    }
    IEnumerator _Daero_Fail_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Panic_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Daero_Default_motion();
        yield return null;
    }
    public void Daero_Face(bool basice, bool happy1, bool happy2, bool panic1, bool panic2)
    {
        Face_Grup[0].SetActive(basice);
        Face_Grup[1].SetActive(happy1);
        Face_Grup[2].SetActive(happy2);
        Face_Grup[3].SetActive(panic1);
        Face_Grup[4].SetActive(panic2);
        //   Debug.Log(basice+ " "+ happy1 +" "+ happy2 + " "+ panic1+" "+ panic2);
    }
    public void Daero_body(bool basic, bool find, bool x, bool found)
    {
        Body_Grup[0].SetActive(basic);
        Body_Grup[1].SetActive(find);
        Body_Grup[2].SetActive(x);
        Body_Grup[3].SetActive(found);
    }
    public void Daero_Gesture(bool find, bool x, bool found)
    {
        Find_Grup.SetActive(find);
        X_Grup.SetActive(x);
    }
    public void Daero_Default_motion()
    {
        Daero_Face(true, false, false, false, false);
        Daero_Gesture(false, false, false);
        Daero_body(true, false, false, false);
    }
}

