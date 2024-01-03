using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Hwangok : MonoBehaviour
{
    [Header("Hwangok")]
    public GameObject HwangokOb;
    public GameObject[] Body_Grup;
    public GameObject Find_Grup;
    public GameObject X_Grup;
    public GameObject Found_Grup;
    public GameObject[] Face_Grup;

    public GameObject NecklaceOb;
    public Button fail_btn;
    public Button success_btn;

    public Animator Hwangok_Ani;

    // Start is called before the first frame update
    void Start()
    {
        Hwangok_Default_motion();
        if (SceneManager.GetActiveScene().name.Equals("02Game"))
        {
            Hwangok_SetDialog();
            StartCoroutine(_Hwangok_Event_Start());
        }
        else if(SceneManager.GetActiveScene().name.Equals("04Mission"))
        {
            HwangokOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1); 
        }
        else if (SceneManager.GetActiveScene().name.Equals("05Found"))
        {
            StartCoroutine(_Hwangok_CompleteDialog());
        }
    }
    public void Hwangok_SetDialog()
    {
        DialogManager.instance.Dialog_str = new string[DialogManager.instance.suro_dialog]; //캐릭터가 가지고있는 나레이션 갯수
        DialogManager.instance.Dialog_str[0] = "안녕~ 나는 먼 나라에서 온 황옥 공주야.";
        DialogManager.instance.Dialog_str[1] = "부모님께서 주신 목걸이를 잃어버렸는데\n나에겐 너무 소중한 목걸이라 꼭 찾아줬으면 좋겠어!";
    }
    //처음 시작 이벤트 
    IEnumerator _Hwangok_Event_Start()
    {
        DialogManager.instance.Dialog_ob.SetActive(true);
        DialogManager.instance.Dialog_count = 0;
        int nextNum = DialogManager.instance.Dialog_count + 1;
        StartCoroutine(_Hwangok_FaceChange());
        DialogManager.instance.DialogAnimation.SetTrigger("DialogOpenT"); //대화창 열림
        yield return new WaitForSeconds(1);
        DialogManager.instance.Dialog(DialogManager.instance.Dialog_count); //처음 대사 ㄱ;
        SoundManager.Instance.PlayCharacterDialog("황옥 1",4.3f);
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false);
        DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true); //다음으로 가기 버튼
        yield return new WaitUntil(() => DialogManager.instance.Dialog_count.Equals(nextNum));
        //배경 변경 - 찾아줘
        Game_UIManager.instance.Bubble_ob.SetActive(true);
        HwangokOb.transform.DOLocalMove(new Vector3(-500, -197, 0), 1);
        ////////////// 찾아줘! //////////////////////////
        Hwangok_body(false, true, false, false);
        Hwangok_Gesture(true, false, false);
        Hwangok_Ani.SetTrigger("Find");
        SoundManager.Instance.PlayCharacterDialog("황옥 1-1",7.3f);
        ////////////////////////////////////////////////////
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == false);
        DialogManager.instance.Dialog_nextBtn.gameObject.SetActive(true); //다음으로 가기 버튼
        yield return new WaitUntil(() => DialogManager.instance.Dialog_ob.activeSelf.Equals(false));
        Game_UIManager.instance.Collection_ob.SetActive(true);
        yield return new WaitForSeconds(1);
        SoundFunction.Instance.OpenMissionWindow_sound();
        //  HwangokOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 1); //안보이게 이동
        Hwangok_Default_motion();
        Game_UIManager.instance.CollectionStart_Btn.gameObject.SetActive(true);
        GameManager.instance.Ongoing = false; //표정 멈춰!
        yield return null;
    }
    //미션 성공 "Found Scene"
    public void Hwangok_CompleteDialog()
    {
        StartCoroutine(_Hwangok_CompleteDialog());
    }
    IEnumerator _Hwangok_CompleteDialog()
    {
        SoundFunction.Instance.Found_sound();
        PlayerPrefs.SetString("TL_Hwangok_Clear", "true");
        GameManager.instance.Change_ClearState();
        DialogManager.instance.DialogStr("와! 이거 맞아! 하나밖에 없는 나의 보물을 찾아줬어!\n정말 고마워. 이 은혜 잊지 않을게.");
     
        ////////////// 맞았어!!! //////////////////////////
        Hwangok_body(false, false, false, true);
        Hwangok_Gesture(false, false, true);
        NecklaceOb.SetActive(true);
        StartCoroutine(_Hwangok_Complete_FaceChange());
        Hwangok_Ani.SetTrigger("Clear");
        yield return new WaitWhile(() => DialogManager.instance.NextBtn_Time == true); //다음 버튼 눌렀을 경우
        yield return new WaitForSeconds(1.5f);
        DialogManager.instance.ClearStamp_ob.SetActive(true);
        SoundManager.Instance.PlayCharacterDialog("황옥 2",8.4f);
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

    public void Hwangok_FailDialog()
    {
        StartCoroutine(_Hwangok_FailDialog());
    }
    IEnumerator _Hwangok_FailDialog()
    {
        DialogManager.instance.Fail_ob.SetActive(true);
        yield return new WaitForSeconds(1f);
        DialogManager.instance.Fail_ob.SetActive(false);
        HwangokOb.transform.DOLocalMove(new Vector3(787, -197, 0), 1);
        ////////////// 틀렸어//////////////////////////
        Hwangok_body(false, false, true, false); //x
        Hwangok_Gesture(false, true, false);
        StartCoroutine(_Hwangok_Fail_FaceChange());
        ///////////////////////////////////////////////////
        DialogManager.instance.MissionDialogStr("남색 유리옥에 곱은 옥이 있는 목걸이니까\n조금만 더 힘내줘!");
      
        Hwangok_Ani.SetTrigger("X");
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayCharacterDialog("황옥 3",5.5f);
        yield return new WaitUntil(() => DialogManager.instance.MissionDialog_ob.activeSelf.Equals(false));
        HwangokOb.transform.DOLocalMove(new Vector3(1660, -197, 0), 0.3f); //안보이게 이동
        yield return new WaitForSeconds(1.5f);
        Hwangok_Default_motion();
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
            Hwangok_Face(false, false, false, true, false);
            Panic_CurrentRange = 0;
        }
        else
        {
            Hwangok_Face(false, false, false, false, true);
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
            Hwangok_Face(true, false, false, false, false);
            Defalt_CurrentRange = 0;
        }
        else
        {
            Hwangok_Face(false, true, false, false, false);
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
            Hwangok_Face(false, true, false, false, false);
            Happy_CurrentRange = 0;
        }
        else
        {
            Hwangok_Face(false, false, true, false, false);
            Happy_CurrentRange = 1;
        }
    }
    IEnumerator _Hwangok_FaceChange()
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
        Hwangok_Face(true, false, false, false, false);
        yield return null;

    }

    IEnumerator _Hwangok_Complete_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Happy_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Hwangok_Default_motion();
        yield return null;
    }
    IEnumerator _Hwangok_Fail_FaceChange()
    {
        GameManager.instance.Ongoing = true;
        while (GameManager.instance.Ongoing)
        {
            Panic_FaceChange();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        Hwangok_Default_motion();
        yield return null;
    }
    public void Hwangok_Face(bool basice, bool happy1, bool happy2, bool panic1, bool panic2)
    {
        Face_Grup[0].SetActive(basice);
        Face_Grup[1].SetActive(happy1);
        Face_Grup[2].SetActive(happy2);
        Face_Grup[3].SetActive(panic1);
        Face_Grup[4].SetActive(panic2);
        //   Debug.Log(basice+ " "+ happy1 +" "+ happy2 + " "+ panic1+" "+ panic2);
    }
    public void Hwangok_body(bool basic, bool find, bool x, bool found)
    {
        Body_Grup[0].SetActive(basic);
        Body_Grup[1].SetActive(find);
        Body_Grup[2].SetActive(x);
        Body_Grup[3].SetActive(found);
    }
    public void Hwangok_Gesture(bool find, bool x, bool found)
    {
        Find_Grup.SetActive(find);
        X_Grup.SetActive(x);
        Found_Grup.SetActive(found);
    }
    public void Hwangok_Default_motion()
    {
        Hwangok_Face(true, false, false, false, false);
        Hwangok_Gesture(false, false, false);
        Hwangok_body(true, false, false, false);
        NecklaceOb.SetActive(false);
    }
}