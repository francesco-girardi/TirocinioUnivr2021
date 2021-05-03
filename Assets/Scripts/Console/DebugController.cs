using Stat;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Console {

    public class DebugController : MonoBehaviour {

        public static DebugCommand HELP;

        public static DebugCommand<float> PLAYER_HEALTH;
        public static DebugCommand<float> PLAYER_ADD_MONEY;

        public List<object> commandList;

        private static bool showConsole;

        public static bool ShowConsole {
            get => showConsole;
            set {
                showConsole = value;
            }
        }
        private bool showHelp;

        private string input;

        private Vector2 scroll;

        public void OnReturn() {
            if (showConsole) {
                HandleInput();
                input = "";
            }
        }

        public void OnDebugControl() {
            showConsole = !showConsole;
            Debug.Log("Console is opening... ");
            Cursor.lockState = CursorLockMode.None;
        }

        public void OnGUI() {
            if (!showConsole) {
                if (!EscMenu.GamePaused)
                    Cursor.lockState = CursorLockMode.Locked;
                return;
            }

            float y = 0f;

            if (showHelp) {
                GUI.skin.label.fontSize = 25;
                GUI.Box(new Rect(0, y, Screen.width, 150), "");

                Rect viewport = new Rect(0, 0, Screen.width - 30f, 35 * commandList.Count);

                scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 140), scroll, viewport);

                for (int i = 0; i < commandList.Count; i++) {
                    DebugCommandBase command = commandList[i] as DebugCommandBase;

                    string label = $"{command.commandFormat} - {command.commandDescription}";

                    Rect labelRect = new Rect(5, 35 * i, viewport.width - 150, 35);

                    GUI.Label(labelRect, label);
                }

                GUI.EndScrollView();

                y += 150;
            }

            GUI.Box(new Rect(0, y, Screen.width, 40), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);

            input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
        }

        private void Awake() {
            HELP = new DebugCommand("help", "Show a list of commands", "help", () => {
                showHelp = true;
            });

            PLAYER_HEALTH = new DebugCommand<float>("player_health", "Set current player health into the choosen value.",
                "player_health", (x) => {
                    Debug.Log("Player health set to: " + x);
                    FindObjectOfType<CharacterStats>().SetCurrentHealth((int)x);

                    if (x <= 0)
                        PlayerLogic.Killer = gameObject;
                });

            PLAYER_ADD_MONEY = new DebugCommand<float>("player_add_money", "Set current player money into the choosen value.",
                "player_add_money", (x) => {
                    Debug.Log("Player money set to: " + x);
                    FindObjectOfType<PlayerLogic>().AddMoney((int)x);
                });

            commandList = new List<object> {
                HELP,
                PLAYER_HEALTH,
                PLAYER_ADD_MONEY,
            };
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Backslash)) {

                OnDebugControl();

                if (showConsole)
                    Time.timeScale = 0f;
                else
                    Time.timeScale = 1.0f;
            }

            if (Input.GetKeyDown(KeyCode.Return))
                OnReturn();
        }

        private void HandleInput() {
            string[] properties = input.Split(' ');

            for (int i = 0; i < commandList.Count; i++) {
                DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

                if (input.Contains(commandBase.commandId)) {
                    if (commandList[i] as DebugCommand != null) {
                        (commandList[i] as DebugCommand).Invoke();
                    }
                    else if (commandList[i] as DebugCommand<float> != null) {
                        (commandList[i] as DebugCommand<float>).Invoke(float.Parse(properties[1]));
                    }
                }
            }
        }

    }

}
