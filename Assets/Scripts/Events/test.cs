using UnityEngine;
using Events;

public class test : MonoBehaviour {

    void Start() {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.D))
            LaunchEvent();
    }

    void LaunchEvent() {
        DebugEventInfo debugEventInfo = new DebugEventInfo();
        debugEventInfo.EventDescription = "Generic event";

        EventSystem.Current.FireEvent(debugEventInfo);
    }
}
