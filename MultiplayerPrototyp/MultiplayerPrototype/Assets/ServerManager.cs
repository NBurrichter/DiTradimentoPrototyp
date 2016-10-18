using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServerManager : MonoBehaviour {

    List<int> professionList = new List<int>()
    {
        0,
        0,
        //0,
        1,
        2
    };

    void Start () {
        for (int i = 0; i < professionList.Count; i++)
        {
            Debug.Log(professionList[i]);
        }
        Debug.Log(professionList.Count);
        //Give Players their profesion
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            int listposition = Random.Range(0, professionList.Count);
            for (int i = 0; i < professionList.Count; i++)
            {
                Debug.Log(professionList[i]);
            }
            Debug.Log("|");
            player.GetComponent<Player>().profession = professionList[listposition];
            professionList.RemoveAt(listposition);
        }
    }
	
}
