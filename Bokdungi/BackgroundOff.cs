using UnityEngine;
using System.Collections;
using Vuforia;

public class BackgroundOff : MonoBehaviour
{
	public GameObject ArCameraOb;
	[SerializeField]BackgroundPlaneBehaviour bgPlane;
    // Update is called once per frame
    void Update()
	{
		bgPlane = ArCameraOb.transform.GetChild(1).gameObject.GetComponent<BackgroundPlaneBehaviour>();

		//if (bgPlane.enabled)
		//{
		//	bgPlane.enabled = false;
		//}
		if (Mission_UIManager.instance.mBackgroundWasSwitchedOff)
		{
				// switch it off
				bgPlane.gameObject.SetActive(false);
		//mBackgroundWasSwitchedOff = true;
		}
        else
        {
			bgPlane.gameObject.SetActive(true);
		}
	}

}
	
