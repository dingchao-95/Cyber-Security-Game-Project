using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour 
{
	public static LevelManager Instance {get; private set;}

	public Player Player {get; private set;}
	public CameraController Camera {get; private set;}
	public TimeSpan RunningTime{get {return DateTime.UtcNow - _started;} }

	public int CurrentTimeBonus
	{
		get
		{
			var secondDifference = (int)(BonusCutOffSeconds - RunningTime.TotalSeconds);
			return Mathf.Max(0, secondDifference) * BonusSecondMultiplier;
		}
	}

	private List<Checkpoint> _checkPoints;
	private int _currentCheckPointIndex;
	private DateTime _started;
	private int _savedPoints;

	public Checkpoint DebugSpawn;
	public int BonusCutOffSeconds;
	public int BonusSecondMultiplier;

	public void Awake()
	{
		_savedPoints = GameManager.Instance.Points;
		Instance = this;
	}

	public void Start()
	{
		_checkPoints = FindObjectsOfType<Checkpoint>().OrderBy(t => t.transform.position.x).ToList();
		_currentCheckPointIndex = _checkPoints.Count > 0 ? 0 : -1;

		Player = FindObjectOfType<Player>();
		Camera = FindObjectOfType<CameraController>();

		_started = DateTime.UtcNow;

		var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerRespawnListener>();
		foreach (var listener in listeners)
		{
			for (var i =  _checkPoints.Count - 1; i >= 0; i--)
			{
				var distance = ((MonoBehaviour)listener).transform.position.x - _checkPoints[i].transform.position.x;
				
				if(distance < 0)
					continue;
				
				_checkPoints[i].AssignObjectToCheckPoint(listener);
				break;
			}
			
		}

#if UNITY_EDITOR
		if(DebugSpawn != null)
			DebugSpawn.SpawnPlayer(Player);
		else if(_currentCheckPointIndex != -1)
			_checkPoints[_currentCheckPointIndex].SpawnPlayer(Player);
#else
		if(_currentCheckPointIndex != -1)
		{
			_checkPoints[_currentCheckPointIndex].SpawnPlayer(Player);
		}
#endif

	}

	public void Update()
	{
		var isAtLastCheckPoint = _currentCheckPointIndex + 1 >= _checkPoints.Count;

		if(isAtLastCheckPoint)
			return;

		var distanceToNextCheckPoint = _checkPoints[_currentCheckPointIndex + 1].transform.position.x - Player.transform.position.x;

		if(distanceToNextCheckPoint > 0)
			return;

		_checkPoints[_currentCheckPointIndex].PlayerLeftCheckPoint();
		_currentCheckPointIndex++;
		_checkPoints[_currentCheckPointIndex].PlayerHitCheckPoint();
		//TODO : Time bonus

		GameManager.Instance.AddPoints(CurrentTimeBonus);
		_savedPoints = GameManager.Instance.Points;
		_started = DateTime.UtcNow;



	}

	public void GoToNextLevel(string levelName)
	{
		StartCoroutine(GoToNextLevelCo(levelName));
	}

	private IEnumerator GoToNextLevelCo(string levelName)
	{
		Player.FinishLevel();
		GameManager.Instance.AddPoints(CurrentTimeBonus);
		yield return new WaitForSeconds(2f);

		if(string.IsNullOrEmpty(levelName))
			Application.LoadLevel("StartScreen");
		else
			Application.LoadLevel(levelName);
	}

	public void KillPlayer()
	{
		StartCoroutine(KillPlayerCo());
	}

	private IEnumerator KillPlayerCo()
	{
		Player.Kill();
		Camera.isFollowing = false;

		yield return new WaitForSeconds(2f);

		Camera.isFollowing = true;

		if(_currentCheckPointIndex != -1)
			_checkPoints[_currentCheckPointIndex].SpawnPlayer(Player);
		 
		//TODO : additional points.
		_started = DateTime.UtcNow;
		GameManager.Instance.ResetPoints(_savedPoints);
	}


}
