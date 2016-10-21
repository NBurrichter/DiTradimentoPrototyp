using UnityEngine;
using System.Collections;

public class SpreadTest : MonoBehaviour {

    Vector3 startposition = new Vector3(1, 2, 3);
    public Vector3 direction = Vector3.forward*5;
	
	// Update is called once per frame
	void Update () {
        Vector3 randomDirection = Random.insideUnitCircle * 1;
        Vector3 resultPosition = randomDirection + direction;
        Debug.DrawLine(startposition, startposition + resultPosition, Color.red);
	}
}
