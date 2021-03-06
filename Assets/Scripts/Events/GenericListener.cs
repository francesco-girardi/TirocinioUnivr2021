using UnityEngine;

namespace Events {

    public class GenericListener : MonoBehaviour {

        private void Start() {
            EventSystem.Current.RegisterListener<DebugEventInfo>(OnGenericEvent);
        }

        private void OnGenericEvent(DebugEventInfo debugEventInfo) {
            Debug.Log("A generic event.");
        }

    }

}
