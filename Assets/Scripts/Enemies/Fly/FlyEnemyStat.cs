using Events;
using Stat;

namespace Enemy.Fly {

    public class FlyEnemyStat : CharacterStats {

        public override void Die() {
            base.Die();

            EnemyDeathInfo enemyDeathInfo = new EnemyDeathInfo();
            enemyDeathInfo.EventDescription = "Fly enemy death event";
            enemyDeathInfo.killer = gameObject;

            EventSystem.Current.FireEvent(enemyDeathInfo);

            Destroy(gameObject);
        }

    }

}
