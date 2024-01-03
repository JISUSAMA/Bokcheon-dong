using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Goro : MonoBehaviour
{
    [Header("Goro")]
    public GameObject GoroOb;
    public GameObject[] Body_Grup;
    public GameObject Find_Grup;
    public GameObject X_Grup;
    public GameObject Found_Grup;
    public GameObject[] Face_Grup;

    public Button fail_btn;
    public Button success_btn;
    public Animator Goro_Ani; 

    bool GoroCompleteState = false;

    // Start is called before the first frame update
    void Start()
    {
        Goro_Default_motion();
        if (SceneManager.GetActiveScene().name.Equals("02Game"))
        {
            Goro_SetDialog();
            StartCoroutine(_Goro_Event_Start());
        }
        else if (SceneManager.GetActiveScene().name.Equals("04Mission"))
        {
            GoroOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1);
        }
        else if (SceneManager.GetActiveScene().name.Equals("05Found"))
        {
            StartCoroutine(_Goro_CompleteDialog());
        }

    }
    public void Goro_SetDialog()
    {
        DialogManager.instance.Dialog_str = new string[DialogManager.instance.suro_dialog]; //캐릭터가 가지고있는 나레이션 갯수
        DialogManager.instance.Dialog_str[0] = "반가워! 나는 고로라고 해~";
        DialogManager.instance.Dialog_str[1] = "나는 몸을 보호하기 위해 투구가 필요한데 함께 찾아줄래?\n네가 도와주면 정말 고마울 거야!";
    }
    //처음 시작 이벤트 
    IEnumerator _Goro_Event_Start()
    {
        DialogManager.instance.Dialog_ob.SetActive(true);
        DialogManager.instance.Dialog_count = 0;
        int nextNum = DialogManager.instance.Dialog_count + 1;
        StartCoroutine(_Goro_FaceChange());
        DialogManager.instance.DialogAnimation.SetTrigger("DialogOpenT"); //대화창 열림
        yield return new WaitForSeconds(1);
        DialogManager.instance.Dialog(DialogManager.instance.Dialog_count); //처음 대사 ㄱ;
        SoundManager.Instance.PlayCharacterDialog("고로 1",2.3f);
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false);
        DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true); //다음으로 가기 버튼
        yield return new WaitUntil(() => DialogManager.instance.Dialog_count.Equals(nextNum));
        //배경 변경 - 찾아줘
        Game_UIManager.instance.Bubble_ob.SetActive(true);
        GoroOb.transform.DOLocalMove(new Vector3(-500, -197, 0), 1);
        ////////////// 찾아줘! //////////////////////////
        Goro_body(false, true, false, false);
        Goro_Gesture(true, false, false);
        Goro_Ani.SetTrigger("Find");
        SoundManager.Instance.PlayCharacterDialog("고로 1-1",7.4f);
        ////////////////////////////////////////////////////
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false);
        DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true); //다음으로 가기 버튼
        yield return new WaitUntil(() => DialogManager.instance.Dialog_ob.activeSelf.Equals(false));
        Game_UIManager.instance.Collection_ob.SetActive(true);
        yield return new WaitForSeconds(1);
        SoundFunction.Instance.OpenMissionWindow_sound();
        GoroOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1); //안보이게 이동
        Goro_Default_motion();
        Game_UIManager.instance.CollectionStart_Btn.gameObject.SetActive(true);
        GameManager.instance.Ongoing = false; //표정 멈춰!

        yield return null;
    }
   
    public void Goro_CompleteDialog()
    {
        StartCoroutine(_Goro_CompleteDialog());
    }
    //미션 성공 "Find Scene"
    IEnumerator _Goro_CompleteDialog()
    {
        SoundFunction.Instance.Found_sound();
        PlayerPrefs.SetString("TL_Goro_Clear", "true");
        GameManager.instance.Change_ClearState();

        DialogManager.instance.DialogStr("역시 찾아줄 거라 믿었어!\n이제 든든하게 몸을 보호할 수 있겠어. 고마워!");

        ////////////// 맞았어!!! //////////////////////////
        Goro_body(false, false, false, true);
        Goro_Gesture(false, false, true);
        StartCoroutine(_Goro_Complete_FaceChange());
        Goro_Ani.SetTrigger("Clear");
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == true); //다음 버튼 눌렀을 경우
        yield return new WaitForSeconds(1.5f);
        DialogManager.instance.ClearStamp_ob.SetActive(true);
        SoundManager.Instance.PlayCharacterDialog("고로 2",6.3f);
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
    //미션 실패 "Mission Scene"
    public void Goro_FailDialog()
    {
        StartCoroutine(_Goro_FailDialog());
    }
    IEnumerator _Goro_FailDialog()
    {
        DialogManager.instance.Fail_ob.SetActive(true);
        yield return new WaitForSeconds(1f);
        DialogManager.instance.Fail_ob.SetActive(false);
        GoroOb.transform.DOLocalMove(new Vector3(787, -197, 0), 1);
        ////////////// 틀렸어//////////////////////////
        Goro_body(false, false, true, false); //x
        Goro_Gesture(false, true, false);
        StartCoroutine(_Goro_Fail_FaceChange());
        Goro_Ani.SetTrigger("X");
        ///////////////////////////////////////////////////
        DialogManager.instance.MissionDialogStr("아쉽게도 내가 찾던 물건은 아니야.\n투구는 머리에 쓰는 물건이니까 잘 찾아봐~");

        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayCharacterDialog("고로 3",7.3f);
        yield return new WaitUntil(() => DialogManager.instance.MissionDialog_ob.activeSelf.Equals(false));
        GoroOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 0.3f); //안보이게 이동
        yield return new WaitForSeconds(1.5f);
        Goro_Default_motion();
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
            Goro_Face(false, false, false, true, false,false,false);
            Panic_CurrentRange = 0;
        }
        else
        {
            Goro_Face(false, false, false, false, true,false,false);
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
            Goro_Face(true, false, false, false, false,false,false);
            Defalt_CurrentRange = 0;
        }
        else
        {
            Goro_Face(false, true, false, false, false,false,false);
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
            if (GoroCompleteState)
            {
                Goro_Face(false, false, false, false, false, true, false);
            }
            else
            {
                Goro_Face(false, true, false, false, false, false, false);
            }
            Happy_CurrentRange = 0;
        }
        else
        {
            if (GoroCompleteState)
            {
                Goro_Face(false, false, false, false, false, false, true);
            }
            else
            {
                Goro_Face(false, false, true, false, false, false, false);
            }
            Happy_CurrentRange = 1;
        }
    }
    IEnumerator _Goro_FaceChange()
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
        Goro_Face(true, false, false, false, false,false,false);
        yield return null;

    }

    IEnumerator _Goro_Complete_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            GoroCompleteState = true;
            Happy_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Goro_Default_motion();
        yield return null;
    }
    IEnumerator _Goro_Fail_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Panic_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Goro_Default_motion();
        yield return null;
    }
    public void Goro_Face(bool basice, bool happy1, bool happy2, bool panic1, bool panic2,bool find1, bool find2)
    {
        Face_Grup[0].SetActive(basice);
        Face_Grup[1].SetActive(happy1);
        Face_Grup[2].SetActive(happy2);
        Face_Grup[3].SetActive(panic1);
        Face_Grup[4].SetActive(panic2);
        Face_Grup[5].SetActive(find1);
        Face_Grup[6].SetActive(find2);
        //   Debug.Log(basice+ " "+ happy1 +" "+ happy2 + " "+ panic1+" "+ panic2);
    }
    public void Goro_body(bool basic, bool find, bool x, bool found)
    {
        Body_Grup[0].SetActive(basic);
        Body_Grup[1].SetActive(find);
        Body_Grup[2].SetActive(x);
        Body_Grup[3].SetActive(found);
    }
    public void Goro_Gesture(bool find, bool x, bool found)
    {
        Find_Grup.SetActive(find);
        X_Grup.SetActive(x);
        Found_Grup.SetActive(found);
    }
    public void Goro_Default_motion()
    {
        Goro_Face(true, false, false, false, false,false,false);
        Goro_Gesture(false, false, false);
        Goro_body(true, false, false, false);
    }
}

