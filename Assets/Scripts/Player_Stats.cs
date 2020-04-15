using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    Dictionary<string, int> playerAttackTable = new Dictionary<string, int>();
    Dictionary<string, int> playerDefenseTable = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        initAttackTable();
        initDefenseTable();
    }

    void initAttackTable() {
        //playerAttackTable.Add("characterName", 0);
        playerAttackTable.Add("Unity-Chan", 5);
    }

    void initDefenseTable() {
        //playerDefenseTable.Add("characterName", 0);
        playerDefenseTable.Add("Unity-Chan", 5);
    }

    public int getPlayerAttack(string player) {
        Debug.Log(player);
        if (!playerAttackTable.ContainsKey(player))
        {
            return 0;
        }
        return playerAttackTable[player];
    }

    public int getPlayerDefense(string player) {
        if (!playerDefenseTable.ContainsKey(player))
        {
            return 0;
        }

        return playerDefenseTable[player];
    }
}
