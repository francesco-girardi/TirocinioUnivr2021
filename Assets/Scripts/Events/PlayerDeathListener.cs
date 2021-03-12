using UnityEngine;

namespace Events.Listeners {

    public class PlayerDeathListener : MonoBehaviour {

        private void Start() {
            EventSystem.Current.RegisterListener<PlayerDeathInfo>(OnPlayerDeathEvent);
        }

        private void OnPlayerDeathEvent(PlayerDeathInfo playerDeathInfo) {
            Debug.Log("Player was killed by " + playerDeathInfo.killer.name);
        }

    }

}

