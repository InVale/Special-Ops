using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using DG.Tweening;
using UnityEngine.AI;

[NetworkSettings (sendInterval = .03f, channel = 0)]
public class Player : NetworkBehaviour {

	public GameObject CirclePrefab;
	public UnitArray[] Units;
	public LayerMask HittableLayer;
	public LayerMask GroundLayer;

	[Serializable]
	public struct UnitArray {
		public KeyCode MyKey;
		public GameObject MyUnit;
	}

	Unit[] _playerUnits;
	Overlord _overlord;
	Camera _myCamera;
	int _myID;

	void Start () {
		if (isLocalPlayer) {
			_overlord = GameObject.FindGameObjectWithTag("Master").GetComponent<Overlord>();
			_myID = NetworkManager.singleton.client.connection.hostId;
			_myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

			GameObject[] unitsToCreate = new GameObject[Units.Length];
			for (int i = 0; i < Units.Length; i++) {
				unitsToCreate[i] = Units[i].MyUnit;
			}

			_overlord.CmdCreatePlayerUnits(_myID, unitsToCreate);
		}
	}

	void Update () {

		if (isLocalPlayer) {
			for (int i = 0; i < Units.Length; i++) {
				if (Input.GetKeyDown(Units[i].MyKey)) {
					Ray ray = _myCamera.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
					if (Physics.Raycast(ray, out hit, Mathf.Infinity, HittableLayer)) {
						if ( GroundLayer == (GroundLayer | (1 << hit.collider.gameObject.layer))) {
							_overlord.CmdMoveUnit(_myID, i, hit.point);
							Instantiate(CirclePrefab, hit.point, Quaternion.identity);
						}
					}
				}
			}
		}
	}
}
