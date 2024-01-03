
using UnityEngine;

public class SoundFunction : MonoBehaviour
{
    public static SoundFunction Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else Instance = this;
    }
  
    //버튼 클릭 사운드 
    public void Click_sound()
    {
        SoundManager.Instance.PlaySFX("3 버튼 클릭음1");
    }
    // 화면 터치 사운드 
    public void TouchScreen_sound()
    {
        SoundManager.Instance.PlaySFX("4 화면 터치음1");
    }
    //다음으로 가기 , 확인 버튼 사운드
    public void NextBtnClick_sound()
    {
        SoundManager.Instance.PlaySFX("6 다음으로 효과음1");
    }
    public void OpenMissionWindow_sound()
    {
        SoundManager.Instance.PlaySFX("8 미션전달창 효과음1");
    }
    //실패 사운드
    public void Fail_sound()
    {
        SoundManager.Instance.PlaySFX("9 틀렸을 때1");
    }
    //당황하는 사운드
    public void Panic_sound()
    {
        SoundManager.Instance.PlaySFX("7 당황하는 효과음1");
    }
    //클리어 사운드
    public void Clear_sound()
    {
        SoundManager.Instance.PlaySFX("10 맞았을 때1");
    }

    public void Found_sound()
    {
        SoundManager.Instance.PlaySFX("11 기뻐하는 효과음1");
    }
    //도장 사운드
    public void Stamp_sound()
    {
        SoundManager.Instance.PlaySFX("12 도장이 찍히는 효과음1");
    }
      
}
