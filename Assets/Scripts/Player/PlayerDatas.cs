using UnityEngine;

public class PlayerDatas {

    public int sceneBuildIndex;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public int playerHealth;
    public int playerMoney;

    public PlayerDatas(int sceneBuildIndex, Vector3 playerPosition, Quaternion playerRotation, int playerHealth,
        int playerMoney) {
        this.sceneBuildIndex = sceneBuildIndex;
        this.playerPosition = playerPosition;
        this.playerRotation = playerRotation;
        this.playerHealth = playerHealth;
        this.playerMoney = playerMoney;
    }

    public override string ToString() {
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder("PLAYER DATAS: \n");
        stringBuilder.Append("\tScene Index: " + sceneBuildIndex + "\n");
        stringBuilder.Append("\tPlayer Position: " + playerPosition + "\n");
        stringBuilder.Append("\tPlayer Rotaion: " + playerRotation + "\n");
        stringBuilder.Append("----------\n");
        stringBuilder.Append("\tPlayer Health: " + playerHealth + "\n");
        stringBuilder.Append("\tPlayer Money: " + playerMoney + "\n");

        return stringBuilder.ToString();
    }

}
