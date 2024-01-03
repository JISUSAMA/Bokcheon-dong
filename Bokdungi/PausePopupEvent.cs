using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PausePopupEvent : MonoBehaviour
{
    public GameObject PausePopup_ob; 
    public Button RetryBtn;
    public Button ExitBtn;

    void OnEnable()
    {
        SoundFunction.Instance.Click_sound();
        ExitBtn.onClick.AddListener(() => OnClick_StopBtn());
        RetryBtn.onClick.AddListener(() => OnClick_RetryBtn());
        Time.timeScale = 0;
    }
    void OnDisable()
    {
        Time.timeScale = 1;
    }
    //게임 그만하기 버튼
    public void OnClick_StopBtn()
    {
        SoundFunction.Instance.Click_sound();
        SceneManager.LoadScene("00TitleScreen");
    }
    //게임 돌아가기
    public void OnClick_RetryBtn()
    {
        SoundFunction.Instance.Click_sound();
        this.gameObject.SetActive(false);
       
    }

}
