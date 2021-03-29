using Events;
using Stat;

namespace Enemy.Slime.FireSlime {

    public class FireSlimeEnemyStat : CharacterStats {

        public override void Die() {
            base.Die();

            EnemyDeathInfo enemyDeathInfo = new EnemyDeathInfo();
            enemyDeathInfo.EventDescription = "Fire Slime enemy death event";
            enemyDeathInfo.enemy = gameObject;
            enemyDeathInfo.killer = PlayerManager.Instance.playerObject;

            EventSystem.Current.FireEvent(enemyDeathInfo);

            Destroy(gameObject);
        }

    }

}
