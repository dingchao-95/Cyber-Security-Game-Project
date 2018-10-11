using UnityEngine;
using System.Collections;

public interface IPlayerRespawnListener
{
	void OnPlayerRespawnInThisCheckPoint(Checkpoint checkpoint, Player player);
}
