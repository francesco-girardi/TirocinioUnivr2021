using Events;
using Stat;

namespace Enemy.Slime.ThunderSlime {

    public class ThunderSlimeEnemyStat : CharacterStats {

        public override void Die() {
            base.Die();

            EnemyDeathInfo enemyDeathInfo = new EnemyDeathInfo();
            enemyDeathInfo.EventDescription = "Thunder Slime enemy death event";
            enemyDeathInfo.enemy = gameObject;
            enemyDeathInfo.killer = PlayerManager.Instance.playerObject;

            EventSystem.Current.FireEvent(enemyDeathInfo);

            Destroy(gameObject);
        }

    }

}


