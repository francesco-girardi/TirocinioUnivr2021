using System;

namespace Console {

    public class DebugCommandBase {

        private string _commandId;
        private string _commandDescription;
        private string _commandFormat;

        public string commandId { get { return _commandId; } }
        public string commandDescription { get { return _commandDescription; } }
        public string commandFormat { get { return _commandFormat; } }

        /// <summary>
        /// Base command format
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="format"></param>
        public DebugCommandBase(string id, string description, string format) {
            _commandId = id;
            _commandDescription = description;
            _commandFormat = format;
        }

    }

    public class DebugCommand : DebugCommandBase {

        private Action command;

        /// <summary>
        /// Command format with no additional inputs
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="format"></param>
        /// <param name="command"></param>
        public DebugCommand(string id, string description, string format, Action command) : base(id, description, format) {
            this.command = command;
        }

        public void Invoke() {
            command.Invoke();
        }

    }

    public class DebugCommand<T1> : DebugCommandBase {

        private Action<T1> command;

        /// <summary>
        /// Command format with additional inputs
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="format"></param>
        /// <param name="command"></param>
        public DebugCommand(string id, string description, string format, Action<T1> command) : base(id, description, format) {
            this.command = command;
        }

        public void Invoke(T1 value) {
            command.Invoke(value);
        }

    }

}
