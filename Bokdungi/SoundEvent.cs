using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : MonoBehaviour
{
 
    public void Stamp_Event()
    {
        SoundFunction.Instance.Stamp_sound();
    }
    public void Panic_Event()
    {
        SoundFunction.Instance.Panic_sound();
    }
}
