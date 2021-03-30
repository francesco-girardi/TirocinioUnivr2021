
public class PlayerDatas {

    public int playerHealth;

    public PlayerDatas(int playerHealth) {
        this.playerHealth = playerHealth;
    }

    public override string ToString() {
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder("PLAYER DATAS: \n");
        stringBuilder.Append("\tPlayer Health: " + playerHealth + "\n");

        return stringBuilder.ToString();
    }

}
