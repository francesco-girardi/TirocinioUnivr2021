using Events;
using Stat;

namespace Enemy.FaerieDragon {
    public class FaerieDragonEnemyStat : CharacterStats {

        public override void Die() {
            base.Die();

            EnemyDeathInfo enemyDeathInfo = new EnemyDeathInfo();
            enemyDeathInfo.EventDescription = "Faerie Dragon enemy death event";
            enemyDeathInfo.enemy = gameObject;
            enemyDeathInfo.killer = PlayerManager.Instance.playerObject;

            EventSystem.Current.FireEvent(enemyDeathInfo);

            Destroy(gameObject);
        }

    }

}
