using Player;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
	public class LevelBehavior: MonoBehaviour
	{
		private const float CreatePlatformDistance = 50f;
        
		private List<Platform> _createdPlatforms;
		private PlayerBehavior _playerBehavior;
		private PlatformFabric _platformFabric;

		public void Init(PlayerBehavior playerBehavior, List<Platform> startPlatforms)
		{
			_createdPlatforms = new List<Platform>();	
			_playerBehavior = playerBehavior;
			_createdPlatforms.AddRange(startPlatforms);
			
			_platformFabric = new PlatformFabric(transform);
			
			_platformFabric.CreateStartPlatform(_createdPlatforms);
		}

		private void Update()
		{
			CreatePlatform();
		}

		private void CreatePlatform()
		{
			Platform lastPlatform = _createdPlatforms[^1];

			if (!(lastPlatform.EndPointPosition.z - CreatePlatformDistance < _playerBehavior.PlayerPosition.z)) 
				return;
			_createdPlatforms.Add(_platformFabric.CreatePlatform(lastPlatform));
			DisablePlatform();
		}

		private void DisablePlatform()
		{
			if (_createdPlatforms.Count < 4) 
				return;
			_createdPlatforms[0].DisablePlatform(transform);
			_createdPlatforms.RemoveAt(0);
		}
	}
}