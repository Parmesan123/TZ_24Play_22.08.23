using Pool;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
	public class PlatformFabric
	{
        private const string PrefabsTrackGround = "Prefabs/TrackGround";
        private const string PathPickup = "Prefabs/Pickup";
		private const string PathObstacle = "Prefabs/ObstaclePrefab";
		private const float WidthLimit = 2f;
			
		private readonly PoolObjects<Platform> _poolPlatform;
		private readonly Obstacle[] _obstacles;
		private readonly PickUp _pickUpPrefab;

		public PlatformFabric(Transform platformContainer)
		{
			Platform platformPrefab = Resources.Load<Platform>(PrefabsTrackGround);
			_pickUpPrefab = Resources.Load<PickUp>(PathPickup);
			_obstacles = Resources.LoadAll<Obstacle>(PathObstacle);
			
			_poolPlatform = new PoolObjects<Platform>(platformPrefab, platformContainer);
			_poolPlatform.CreatePool(2);
		}

		public void CreateStartPlatform(List<Platform> platforms)
		{
			foreach (Platform platform in platforms)
			{
                platform.SetObstacle(_obstacles[Random.Range(0, _obstacles.Length)]);
				SetPickUpOnPlatform(platform);
			}
			_poolPlatform.AddToPool(platforms);
		}
		
		public Platform CreatePlatform(Platform lastPlatform)
		{
			_poolPlatform.GetFreeElement(out Platform newPlatform);
			newPlatform.SetObstacle(_obstacles[Random.Range(0, _obstacles.Length)]);
			newPlatform.PutPlatform(lastPlatform.EndPointPosition);
			SetPickUpOnPlatform(newPlatform);
			
			return newPlatform;
		}

		private void SetPickUpOnPlatform(Platform platform)
		{
			List<Transform> copySpawnPoint = platform.SpawnPoints;

			int countPickUp = Random.Range(3,5);

			for (int i = 0; i < countPickUp; i++)
			{
				Transform randTransform = copySpawnPoint[Random.Range(0, copySpawnPoint.Count)];
				copySpawnPoint.Remove(randTransform);
				PickUp pickUp = Object.Instantiate(_pickUpPrefab, randTransform);
				pickUp.transform.position = randTransform.position + new Vector3(Random.Range(-WidthLimit, WidthLimit), 0,0);
				platform.PickUps.Add(pickUp);
			}
		}
	}
}