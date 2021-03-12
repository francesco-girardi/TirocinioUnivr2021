using UnityEngine;

namespace Events {

    /// <summary>
    /// The base Event class
    /// </summary>
    public abstract class EventInfo {
        public string EventDescription;
    }

    /// <summary>
    /// Debug event class. Use to throw a generic event.
    /// </summary>
    public class DebugEventInfo : EventInfo {
        public int VerbosityLevel;
    }

    /// <summary>
    /// Called when player die
    /// </summary>
    public class PlayerDeathInfo : EventInfo {
        public GameObject killer;
    }

    /// <summary>
    /// Called when an enemy die
    /// </summary>
    public class EnemyDeathInfo : EventInfo {
        public GameObject enemy;
        public GameObject killer;
    }
}