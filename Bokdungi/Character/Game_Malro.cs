using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Malro : MonoBehaviour
{
    [Header("Malro")]
    public GameObject MalroOb;
    public GameObject[] Body_Grup;
    public GameObject Find_Grup;
    public GameObject X_Grup;
    public GameObject Found_Grup;
    public GameObject[] Face_Grup;

    public Animator Malro_Ani;
    public Button fail_btn;
    public Button success_btn;

    // Start is called before the first frame update
    void Start()
    {
        Malro_Default_motion();
        if (SceneManager.GetActiveScene().name.Equals("02Game"))
        {
            Malro_SetDialog();
            StartCoroutine(_Malro_Event_Start());
        }
        else if (SceneManager.GetActiveScene().name.Equals("04Mission"))
        {
            MalroOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1);
        }
        else if (SceneManager.GetActiveScene().name.Equals("05Found"))
        {
            StartCoroutine(_Malro_CompleteDialog());
        }

    }
    public void Malro_SetDialog()
    {
        DialogManager.instance.Dialog_str = new string[DialogManager.instance.suro_dialog]; //캐릭터가 가지고있는 나레이션 갯수
        DialogManager.instance.Dialog_str[0] = "안녕? 난 가야에서 가장 유명한 대장간에서 일하는 말로라고 해.";
        DialogManager.instance.Dialog_str[1] = "나는 불을 담는 화로 모양 그릇이 필요한데, 찾을 수가 없네.\n네가 찾아줄 수 있을까?";
    }
    //처음 시작 이벤트 
    IEnumerator _Malro_Event_Start()
    {
        DialogManager.instance.Dialog_ob.SetActive(true);
        DialogManager.instance.Dialog_count = 0;
        int nextNum = DialogManager.instance.Dialog_count + 1;
        StartCoroutine(_Malro_FaceChange());
        DialogManager.instance.DialogAnimation.SetTrigger("DialogOpenT"); //대화창 열림
        yield return new WaitForSeconds(1);
        DialogManager.instance.Dialog(DialogManager.instance.Dialog_count); //처음 대사 ㄱ;
        SoundManager.Instance.PlayCharacterDialog("말로 1",5.6f);
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false);
        DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true); //다음으로 가기 버튼
        yield return new WaitUntil(() => DialogManager.instance.Dialog_count.Equals(nextNum));
        //배경 변경 - 찾아줘
        Game_UIManager.instance.Bubble_ob.SetActive(true);
        MalroOb.transform.DOLocalMove(new Vector3(-500, -197, 0), 1);
        ////////////// 찾아줘! //////////////////////////
        Malro_body(false, true, false, false);
        Malro_Gesture(true, false, false);
        Malro_Ani.SetTrigger("Find");
        SoundManager.Instance.PlayCharacterDialog("말로 1-1",6.6f);
        ////////////////////////////////////////////////////
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false);
        DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true); //다음으로 가기 버튼
        yield return new WaitUntil(() => DialogManager.instance.Dialog_ob.activeSelf.Equals(false));

        Game_UIManager.instance.Collection_ob.SetActive(true);
        yield return new WaitForSeconds(1);
        SoundFunction.Instance.OpenMissionWindow_sound();
        MalroOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 0.3f); //안보이게 이동
        Malro_Default_motion();
        Game_UIManager.instance.CollectionStart_Btn.gameObject.SetActive(true);
        GameManager.instance.Ongoing = false; //표정 멈춰!

        yield return null;
    }
    //미션 성공
    public void Malro_CompleteDialog()
    {
        StartCoroutine(_Malro_CompleteDialog());
    }
    IEnumerator _Malro_CompleteDialog()
    {
        SoundFunction.Instance.Found_sound();
        PlayerPrefs.SetString("TL_Malro_Clear", "true");
        GameManager.instance.Change_ClearState();
        DialogManager.instance.DialogStr("찾아줘서 정말 고마워.\n얼른 고로 형 무기를 만들어서 기쁘게 해줘야겠어!");
     
        ////////////// 맞았어!!! //////////////////////////
        Malro_body(false, false, false, true);
        Malro_Gesture(false, false, true);
        StartCoroutine(_Malro_Complete_FaceChange());
        Malro_Ani.SetTrigger("Clear");
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == true); //다음 버튼 눌렀을 경우
        yield return new WaitForSeconds(1.5f);
        DialogManager.instance.ClearStamp_ob.SetActive(true);
        SoundManager.Instance.PlayCharacterDialog("말로 2", 6.5f);
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
    //미션 실패 
    public void Malro_FailDialog()
    {
        StartCoroutine(_Malro_FailDialog());
    }
    IEnumerator _Malro_FailDialog()
    {
        DialogManager.instance.Fail_ob.SetActive(true);
        yield return new WaitForSeconds(1f);
        DialogManager.instance.Fail_ob.SetActive(false);
        MalroOb.transform.DOLocalMove(new Vector3(787, -197, 0), 1);
        ////////////// 틀렸어//////////////////////////
        Malro_body(false, false, true, false); //x
        Malro_Gesture(false, true, false);
        StartCoroutine(_Malro_Fail_FaceChange());
        Malro_Ani.SetTrigger("X");
        ///////////////////////////////////////////////////
        DialogManager.instance.MissionDialogStr("헤헤 이건 화로 모양 토기가 아니야.\n넓은 입을 가진 그릇 받침을 다시 찾아볼래?");
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayCharacterDialog("말로 3",7.4f);
        yield return new WaitUntil(() => DialogManager.instance.MissionDialog_ob.activeSelf.Equals(false));

        MalroOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1); //안보이게 이동
        yield return new WaitForSeconds(1.5f);
        Malro_Default_motion();
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
            Malro_Face(false, false, false, true, false);
            Panic_CurrentRange = 0;
        }
        else
        {
            Malro_Face(false, false, false, false, true);
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
            Malro_Face(true, false, false, false, false);
            Defalt_CurrentRange = 0;
        }
        else
        {
            Malro_Face(false, true, false, false, false);
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
            Malro_Face(false, true, false, false, false);
            Happy_CurrentRange = 0;
        }
        else
        {
            Malro_Face(false, false, true, false, false);
            Happy_CurrentRange = 1;
        }
    }
    IEnumerator _Malro_FaceChange()
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
        Malro_Face(true, false, false, false, false);
        yield return null;

    }
    IEnumerator _Malro_Complete_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Happy_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Malro_Default_motion();
        yield return null;
    }
    IEnumerator _Malro_Fail_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Panic_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Malro_Default_motion();
        yield return null;
    }
    public void Malro_Face(bool basice, bool happy1, bool happy2, bool panic1, bool panic2)
    {
        Face_Grup[0].SetActive(basice);
        Face_Grup[1].SetActive(happy1);
        Face_Grup[2].SetActive(happy2);
        Face_Grup[3].SetActive(panic1);
        Face_Grup[4].SetActive(panic2);
        //   Debug.Log(basice+ " "+ happy1 +" "+ happy2 + " "+ panic1+" "+ panic2);
    }
    public void Malro_body(bool basic, bool find, bool x, bool found)
    {
        Body_Grup[0].SetActive(basic);
        Body_Grup[1].SetActive(find);
        Body_Grup[2].SetActive(x);
        Body_Grup[3].SetActive(found);
    }
    public void Malro_Gesture(bool find, bool x, bool found)
    {
        Find_Grup.SetActive(find);
        X_Grup.SetActive(x);
        Found_Grup.SetActive(found);
    }
    public void Malro_Default_motion()
    {
        Malro_Face(true, false, false, false, false);
        Malro_Gesture(false, false, false);
        Malro_body(true, false, false, false);
    }
}

