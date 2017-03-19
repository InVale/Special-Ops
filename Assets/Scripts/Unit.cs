using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

	NavMeshAgent _myAgent;

	void Start () {
		_myAgent = gameObject.GetComponent<NavMeshAgent>();
	}
	
	public void Destination (Vector3 destination) {
		_myAgent.destination = destination;
	}
}
