
using UnityEngine;
using UnityEngine.UI;

public class CameraResolution : MonoBehaviour
{
    /*   Camera camera;
        private void Awake()
        {
            Camera.main.aspect = 16f / 9f;
           // Camera_Resolution();
       }

       void OnPreCull() => GL.Clear(true, true, Color.black);

        //카메라 9:16 비율로 레터박스 해주는 함수
        void Camera_Resolution()
        {
           camera = GetComponent<Camera>();

            //카메라 컴포넌트의 Viewport Rect
            Rect rt = camera.rect;

            //가로모드 16:9
            float scale_height = ((float)Screen.width / Screen.height) / ((float)16 / 9);
            float scale_width = 1f / scale_height;

            if (scale_height < 1)
            {
                rt.height = scale_height;
                rt.y = (1f - scale_height) / 2f;
            }
            else
            {
                rt.width = scale_width;
                rt.x = (1f - scale_width) / 2f;
            }

            camera.rect = rt;
        }*/
    public Camera camera_cam;
    public CanvasScaler[] Canvases;
    void OnPreCull() => GL.Clear(true, true, Color.black);
    private void Start()
    {
        //  SetResolution(); // 초기에 게임 해상도 고정
        //Camera2View();
        SetCameraAsect();
    }

    // 해상도 설정하는 함수 
    public void SetResolution()
    {
        int setWidth = 2560; // 사용자 설정 너비
        int setHeight = 1440; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            for (int i =0; i<Canvases.Length; i++)
            {
                Canvases[i].matchWidthOrHeight = 1;
            }
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            camera_cam.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
  
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            camera_cam.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }

    public void Camera2View()
    {
        Rect rt = camera_cam.rect;

        float scale_height = ((float)Screen.width / Screen.height) / ((float)16 / 9);   //가로
        float scale_width = 1f / scale_height;

        if (scale_height < 1)
        {
            rt.height = scale_height;
            rt.y = (1f - scale_height) / 2f;
            for (int i = 0; i < Canvases.Length; i++)
            {
                Canvases[i].matchWidthOrHeight = 1;
            }
        }
        else
        {
            for (int i = 0; i < Canvases.Length; i++)
            {
                Canvases[i].matchWidthOrHeight = 0;
            }
            rt.width = scale_width;
            rt.x = (1f - scale_width) / 2f;
        }

        camera_cam.rect = rt;
    }
    public void SetCameraAsect()
    {
     
        float targetaspect = 2560.0f / 1440.0f;
        float windowaspect = 0;

#if UNITY_EDITOR
        windowaspect = (float)camera_cam.pixelWidth / (float)camera_cam.pixelHeight;
#else
            windowaspect = (float)Screen.width / (float)Screen.height;
#endif
        float scaleheight = windowaspect / targetaspect;
  //      Camera camera = UnityEngine.Camera.main;

        if (scaleheight < 1.0f)
        {
            for (int i = 0; i < Canvases.Length; i++)
            {
                Canvases[i].matchWidthOrHeight = 0;
            }
            Rect rect = camera_cam.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera_cam.rect = rect;
        }
        else
        { // add pillarbox 
            for (int i = 0; i < Canvases.Length; i++)
            {
                Canvases[i].matchWidthOrHeight = 1;
            }
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera_cam.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera_cam.rect = rect;
        }
    }

    private void Update()
    {
      //  if (Input.GetMouseButtonDown(0))
      //  {
      //      SoundFunction.Instance.TouchScreen_sound();
      //  }
      //
//#if UNITY_ANDROID || UNITY_IOS
//
//        // Touch myTouch = Input.GetTouch(0);
//        Touch[] myTouches = Input.touches;
//        if (Input.touchCount > 0)
//        {
//            if (Input.touchCount == 0)
//            {
//                SoundFunction.Instance.TouchScreen_sound();
//            }
//        }
//#endif  
    }
    //  private void Creat_touch()
    //  {
    //      Vector3 mPosition = ca.ScreenToWorldPoint(Input.mousePosition);
    //      mPosition.z = 100;
    //      //  Debug.Log(mPosition);
    //      Debug.DrawLine(Vector3.zero, mPosition, Color.red);
    //     
    //  }
    //
    //  private void Creat_touch(Vector3 _touchPos)
    //  {
    //      Vector3 mPosition = ca.ScreenToWorldPoint(_touchPos);
    //      mPosition.z = 100;
    //      //   Debug.Log(mPosition);
    //      Debug.DrawLine(Vector3.zero, mPosition, Color.red);
    //  }
}
