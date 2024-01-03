using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance { get; private set; }
    [Header("Dialog")]
    public GameObject Dialog_ob;
    public Text Dialog_TM;
    public Button Dialog_nextBtn;
    public Animator DialogAnimation;
    [Header("MissionDialog")]
    public GameObject MissionDialog_ob;
    public Text MissionDialog_TM;
    public Button MissionDialog_nextBtn;
    public Animator MissionDialogAnimation;
    public GameObject Succese_ob; //o 
    public GameObject Fail_ob; //x


    public int Dialog_count;
    public string[] Dialog_str;
    public int bokdungi_dialog = 6, 
        bihwa_dialog, suro_dialog, daero_dialog, goro_dialog, hwangok_dialog, malro_dialog = 2;

    public GameObject ClearStamp_ob;

    public bool Dialog_ing = false; //대화 진행 중 인 경우, 
    public bool NextBtn_Time = false;
    public bool MissionDialog_NextBtn_Time = false;

    public float Typing_speed =4;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        Dialog_nextBtn.onClick.AddListener(() => Dialog_NextBtnEvnet()); //버튼 누를떄 마다 목록 갱신
        MissionDialog_nextBtn.onClick.AddListener(() => MissionDialog_NextBtnEvnet());
     
    }
    void Dialog_NextBtnEvnet()
    {
        Dialog_TM.text = " "; //초기화
        SoundFunction.Instance.NextBtnClick_sound();
        Dialog_count += 1; //버튼 누를떄 마다 목록 갱신
        if (SceneManager.GetActiveScene().name.Equals("01Tutorial"))
        {
         //   Debug.Log("다이얼로그 작동중 여기에 드러오나???????----------------1");

            if (Dialog_count.Equals(3))
            {
           //     Debug.Log("다이얼로그 작동중 여기에 드러오나???????----------------2");
                NextBtn_Time = false; //버튼 비활성화
            }
            else
            {
            //    Debug.Log("다이얼로그 작동중 여기에 드러오나???????----------------3");
                Dialog(Dialog_count); //다음 대사!@
                NextBtn_Time = false; //버튼 비활성화
            }
        }
        else
        {
            Dialog(Dialog_count); //다음 대사!@
            NextBtn_Time = false; //버튼 비활성화
        }
      
    }
    void MissionDialog_NextBtnEvnet()
    {
        MissionDialog_TM.text = " "; //초기화
        SoundFunction.Instance.NextBtnClick_sound();
        MissionDialog_NextBtn_Time = false;
    }
    // 다음으로 가기 버튼 누르면 다이얼로그 나옴
    public void Dialog(int Dialog_count)
    {
        StartCoroutine(_Dialog());
    }
    IEnumerator _Dialog()
    {
       // Debug.Log("count  : " + Dialog_count + " " + "strcount" + " : " + Dialog_str.Length);

        if (Dialog_count < Dialog_str.Length)
        {
            Dialog_TM.text = " "; //초기화
          //  Debug.Log("Typing_speed" + Typing_speed);
            string dialog_str = Dialog_str[Dialog_count];
            NextBtn_Time = false;
            Dialog_nextBtn.gameObject.SetActive(false);
            Dialog_TM.DOText(dialog_str, Typing_speed);
            yield return new WaitForSeconds(Typing_speed);
            NextBtn_Time = true; //4초후에 다음으로 가기 버튼 활성화 시키기
        }
        else
        {
            Dialog_nextBtn.gameObject.SetActive(false);
            Dialog_TM.text = " "; //초기화
            NextBtn_Time = false;
            DialogAnimation.SetTrigger("DialogCloseT");
            SoundManager.Instance.dialog.Stop();
            yield return new WaitForSeconds(2f);
            Dialog_ob.SetActive(false); //대화 종료하기 
        }
   
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  
    public void DialogStr(string story)
    {
        NextBtn_Time = false;
        Dialog_ob.SetActive(true);
        Dialog_TM.text = " "; //초기화
        string dialog_str = story;
        StartCoroutine(_DialogStr(dialog_str));
    }
    IEnumerator _DialogStr(string dialogtext)
    {
        Dialog_ob.SetActive(true);
       Dialog_TM.text = " "; //초기화
        DialogAnimation.SetTrigger("DialogOpenT");
       Dialog_nextBtn.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        NextBtn_Time = true;
        Dialog_TM.DOText(dialogtext, Typing_speed);
        yield return new WaitForSeconds(Typing_speed);
       Dialog_nextBtn.gameObject.SetActive(true);
        yield return new WaitUntil(() => NextBtn_Time == false); //다음 버튼 눌렀을 경우
        Dialog_nextBtn.gameObject.SetActive(false);
       Dialog_TM.text = " "; //초기화
       DialogAnimation.SetTrigger("DialogCloseT");
        SoundManager.Instance.dialog.Stop();
        yield return new WaitForSeconds(2);
        Dialog_ob.SetActive(false); //대화 종료하기 
    }
    //복둥이 다이얼로그 
   public string[] dialog_str;
    public void LongDialogStr(string story1, string story2)
    {
      
        NextBtn_Time = false;
        Dialog_ob.SetActive(true);
        Dialog_TM.text = " "; //초기화

        dialog_str = new string[2];
        dialog_str[0] = story1;
        dialog_str[1] = story2;
        StartCoroutine(_longDialogStr());
    }
    IEnumerator _longDialogStr()
    {
        Dialog_ob.SetActive(true);
        Dialog_TM.text = " "; //초기화
        Dialog_nextBtn.gameObject.SetActive(false);
        NextBtn_Time = true;
        Dialog_TM.DOText(dialog_str[0], 4);
        yield return new WaitForSeconds(5);
        Dialog_TM.text = " "; //초기화
        Dialog_TM.DOText(dialog_str[1], 6);
        yield return new WaitForSeconds(7);
        Dialog_nextBtn.gameObject.SetActive(true);
        yield return new WaitUntil(() => NextBtn_Time == false); //다음 버튼 눌렀을 경우


        yield return null;
    }
    // 미션 다이얼로그

    public void MissionDialogStr(string story)
    {
        string dialog_str = story;
        StartCoroutine(_MissionDialogStr(dialog_str));
    }
    IEnumerator _MissionDialogStr(string dialogtext)
    {
        MissionDialog_ob.SetActive(true);
        MissionDialog_TM.text = " "; //초기화
        MissionDialogAnimation.SetTrigger("DialogOpenT");
        MissionDialog_NextBtn_Time = false;
        MissionDialog_nextBtn.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        MissionDialog_NextBtn_Time = true;
        MissionDialog_TM.DOText(dialogtext, Typing_speed);
        yield return new WaitForSeconds(Typing_speed);
        MissionDialog_nextBtn.gameObject.SetActive(true);
        yield return new WaitUntil(() => MissionDialog_NextBtn_Time == false); //다음 버튼 눌렀을 경우
        MissionDialog_nextBtn.gameObject.SetActive(false);
        MissionDialog_TM.text = " "; //초기화
        MissionDialogAnimation.SetTrigger("DialogCloseT");
        SoundManager.Instance.dialog.Stop();
        yield return new WaitForSeconds(2);
        MissionDialog_ob.SetActive(false); //대화 종료하기 
    }
    public void MissionDialog(int Dialog_count)
    {
        StartCoroutine(__MissionDialog());
    }
    IEnumerator __MissionDialog()
    {
    //    Debug.Log("count  : " + Dialog_count + " " + "strcount" + " : " + Dialog_str.Length);
        if (Dialog_count < Dialog_str.Length)
        {
            MissionDialog_TM.text = " "; //초기화
            string dialog_str = Dialog_str[Dialog_count];
            NextBtn_Time = false;
            MissionDialog_nextBtn.gameObject.SetActive(false);
            MissionDialog_TM.DOText(dialog_str, Typing_speed);
            yield return new WaitForSeconds(Typing_speed);
            NextBtn_Time = true; //4초후에 다음으로 가기 버튼 활성화 시키기
        }
        else
        {
            MissionDialog_nextBtn.gameObject.SetActive(false);
            MissionDialog_TM.text = " "; //초기화
            NextBtn_Time = false;
           MissionDialogAnimation.SetTrigger("DialogCloseT");
            SoundManager.Instance.dialog.Stop();
            yield return new WaitForSeconds(2f);
            MissionDialog_ob.SetActive(false); //대화 종료하기 

        }

    }

}
