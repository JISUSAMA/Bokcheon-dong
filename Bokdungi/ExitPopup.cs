
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitPopup : MonoBehaviour
{
    public GameObject ExitPopup_ob;
    public Button Yes_btn;
    public Button No_btn;

    private void Awake()
    {
        Yes_btn.onClick.AddListener(() => OnClick_Yes_btn());
        No_btn.onClick.AddListener(() => OnClick_No_btn());
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
    public void OnClick_Yes_btn()
    {
        SoundFunction.Instance.Click_sound();
        Application.Quit(); //게임 종료
    }
    public void OnClick_No_btn()
    {
        SoundFunction.Instance.Click_sound();
        ExitPopup_ob.SetActive(false);
    }
}
