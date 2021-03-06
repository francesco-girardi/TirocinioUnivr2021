
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

}