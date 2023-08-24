using Properties;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Level
{
	public class Platform: MonoBehaviour, IDestroyed
	{
		[SerializeField] private float riseTime;
		[SerializeField] private float yOffset;
		[SerializeField] private Transform obstaclePoint;
		[SerializeField] private Transform endPoint;
		[SerializeField] private Transform[] spawnPoints;

		private Obstacle _obstacle;

		private Vector3 PlatformPosition => transform.position;

		public Vector3 EndPointPosition => endPoint.position;
		public List<Transform> SpawnPoints => spawnPoints.ToList();
		public List<PickUp> PickUps { get; } = new List<PickUp>();

		public void PutPlatform(Vector3 newPosition)
		{
			Vector3 startPosition = new Vector3(newPosition.x, newPosition.y - yOffset, newPosition.z);
			transform.position = startPosition;
			
			StartCoroutine(MoveRoutine());

			IEnumerator MoveRoutine()
			{
				float distance = Vector3.Distance(PlatformPosition, newPosition) / riseTime;
				float speed = distance / riseTime;
				
				float timer = 0;
				
				while (timer < riseTime)
				{
					float progress = timer * speed / distance;

					Vector3 lerpPos = Vector3.Lerp(startPosition, newPosition, progress);
					transform.position = lerpPos;

					timer += Time.fixedDeltaTime;
					yield return new WaitForSeconds(Time.fixedDeltaTime);
				}

				transform.position = newPosition;
			}
		}

		public void DisablePlatform(Transform containerPickUp)
		{
			foreach (PickUp pickUp in PickUps)
			{
                Destroy(pickUp.gameObject);
			}
			PickUps.Clear();
			
			gameObject.SetActive(false);
		}
		
		public void SetObstacle(Obstacle obstacle)
		{
			if(_obstacle is not null)
			{
				Destroy(_obstacle.gameObject);
			}
			
			_obstacle = Instantiate(obstacle, obstaclePoint.position, quaternion.identity, transform);
		}

		public void Destroy()
		{
			Destroy(gameObject);
		}
	}
}