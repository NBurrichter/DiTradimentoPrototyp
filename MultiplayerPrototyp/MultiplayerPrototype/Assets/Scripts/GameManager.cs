using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    #region Player Dictionary
    private const string PLAYER_ID_PREFIX = "Player ";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string _netID, Player _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    public static void UnRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }

    public static Player GetPlayer(string _playerID)
    {
        return players[_playerID];
    }
    #endregion
    
    #region Enemy Dictionary
    private const string ENEMY_ID_PREFIX = "Enemy ";

    private static Dictionary<string, Enemy> enemys = new Dictionary<string, Enemy>();

    public static void RegisterEnemy(string _ID, Enemy _enemy)
    {
        string _enemyID = ENEMY_ID_PREFIX + _ID;
        enemys.Add(_enemyID, _enemy);
        _enemy.transform.name = _enemyID;
    }

    public static void UnRegisterEnemy(string _enemyID)
    {
        enemys.Remove(_enemyID);
    }

    public static Enemy GetEnemy(string _enemyID)
    {
        return enemys[_enemyID];
    }
    #endregion

    
    //Player debug
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        //foreach(string _playerID in players.Keys)
        //{
        //    GUILayout.Label(_playerID + " - " + players[_playerID].transform.name);
        //}

        foreach (string _enemyID in enemys.Keys)
        {
            GUILayout.Label(_enemyID + " - " + enemys[_enemyID].transform.name);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    
}
