using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayIP : MonoBehaviour {

	void Update () {
        GetComponent<Text>().text = Network.player.ipAddress;
    }
	
}
