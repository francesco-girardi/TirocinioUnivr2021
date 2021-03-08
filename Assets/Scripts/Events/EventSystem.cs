using System.Collections.Generic;
using UnityEngine;

namespace Events {

    public class EventSystem : MonoBehaviour {

        private static EventSystem __Current;
        public static EventSystem Current {
            get {
                if (__Current == null)
                    __Current = GameObject.FindObjectOfType<EventSystem>();

                return __Current;
            }
        }

        delegate void EventListener(EventInfo eventInfo);
        Dictionary<System.Type, List<EventListener>> eventListeners;

        private void OnEnable() {
            __Current = this;
        }

        public void RegisterListener<T>(System.Action<T> listener) where T : EventInfo {
            System.Type eventType = typeof(T);
            if (eventListeners == null)
                eventListeners = new Dictionary<System.Type, List<EventListener>>();

            if (eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
                eventListeners[eventType] = new List<EventListener>();

            EventListener wrapper = (ei) => { listener((T)ei); };

            eventListeners[eventType].Add(wrapper);
        }

        public void UnregisterListener<T>(System.Action<T> listener) where T : EventInfo {
            // TODO
        }

        public void FireEvent(EventInfo eventInfo) {
            System.Type trueEventInfoClass = eventInfo.GetType();
            if (eventListeners == null || eventListeners[trueEventInfoClass] == null)
                return;

            foreach (EventListener el in eventListeners[trueEventInfoClass])
                el(eventInfo);
        }

    }

}
