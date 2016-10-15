using UnityEngine;
using System.Collections;

public class IndyNav : MonoBehaviour {

	public GameObject IndyTarget;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<NavMeshAgent> ().SetDestination (IndyTarget.transform.position);
	}
}
