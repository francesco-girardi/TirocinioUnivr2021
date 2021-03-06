using UnityEngine;
using Stat;
using Events;

public class PlayerLogic : CharacterStats {

    private string dataPath;

    public override void Die() {
        base.Die();

        PlayerDeathInfo playerDeathInfo = new PlayerDeathInfo();
        playerDeathInfo.EventDescription = "Player death";
        playerDeathInfo.killer = gameObject;

        EventSystem.Current.FireEvent(playerDeathInfo);

        Destroy(gameObject);
    }

    private void Awake() {
        dataPath = Application.persistentDataPath + "/playerData.json";
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.K))
            Die();
    }
}
