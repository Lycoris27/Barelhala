using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    
    public void PlayEvent(string eventName, GameObject emitter = null)
    {
        if (emitter == null) { emitter = gameObject; }
        AkUnitySoundEngine.PostEvent(eventName, emitter);
    }

    public void StopEvent(string eventName, GameObject emitter = null)
    {
        if (emitter == null) { emitter = gameObject; }
        AkUnitySoundEngine.ExecuteActionOnEvent(eventName, AkActionOnEventType.AkActionOnEventType_Stop, emitter);
    }
    
    
}
