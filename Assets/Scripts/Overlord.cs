using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Overlord : NetworkBehaviour {

	Dictionary<int, Unit[]> _players = new Dictionary<int, Unit[]>();

	[Command]
	public void CmdCreatePlayerUnits (int ID, GameObject[] Units) {
		if (!_players.ContainsKey(ID)) {
			Unit[] newUnits = new Unit[Units.Length];

			for (int i = 0; i < Units.Length; i++) {
				GameObject newUnit = Instantiate(Units[i], new Vector3(0, (i + 1)*3, 0), Quaternion.identity) as GameObject;
				newUnits[i] = newUnit.GetComponent<Unit>();

				NetworkServer.Spawn(newUnit);
			}

			_players.Add(ID, newUnits);
		}
		else {
			Debug.LogError("This player already exists ! WTF is happening ?");
		}
	}

	[Command]
	public void CmdMoveUnit (int ID, int UnitNumber, Vector3 Position) {
		if (_players.ContainsKey(ID)) {
			_players[ID][UnitNumber].Destination(Position);
		}
		else {
			Debug.LogError("This player doesn't exist.");
		}
	}
}
