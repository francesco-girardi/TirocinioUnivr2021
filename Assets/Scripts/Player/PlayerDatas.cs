
public class PlayerDatas {

    public int playerHealth;
    public int playerMoney;

    public PlayerDatas(int playerHealth, int playerMoney) {
        this.playerHealth = playerHealth;
        this.playerMoney = playerMoney;
    }

    public override string ToString() {
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder("PLAYER DATAS: \n");
        stringBuilder.Append("\tPlayer Health: " + playerHealth + "\n");
        stringBuilder.Append("\tPlayer Money: " + playerMoney + "\n");

        return stringBuilder.ToString();
    }

}
