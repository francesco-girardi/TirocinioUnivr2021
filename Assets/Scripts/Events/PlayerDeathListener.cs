using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events {

    public class PlayerDeathListener : MonoBehaviour {

        private void Start() {
            EventSystem.Current.RegisterListener<PlayerDeathInfo>(OnPlayerDeathEvent);
        }

        private void OnPlayerDeathEvent(PlayerDeathInfo playerDeathInfo) {
            Debug.Log("Player was killed by " + playerDeathInfo.killer.name);
        }

    }

}

