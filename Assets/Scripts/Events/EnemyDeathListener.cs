using UnityEngine;

namespace Events.Listeners {

    public class EnemyDeathListener : MonoBehaviour {

        private void Start() {
            EventSystem.Current.RegisterListener<EnemyDeathInfo>(OnEnemyDeathEvent);
        }

        private void OnEnemyDeathEvent(EnemyDeathInfo enemyDeathInfo) {
            Debug.Log(enemyDeathInfo.enemy.name + " was killed by " + enemyDeathInfo.killer.name);
        }

    }

}
