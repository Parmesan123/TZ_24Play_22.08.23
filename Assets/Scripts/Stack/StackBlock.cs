using Level;
using Properties;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Stack
{
	[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
	public class StackBlock: MonoBehaviour, IMovable
	{
		private const float HalfSizeBox = 0.45f;
		private const float DisableTime = 2f;

		private TrailRenderer _trailRenderer;
		private ParticleSystem _particleSystem;
		public Transform Transform() => transform;
		
		public event Action<StackBlock> OnCollisionWithObstacleEvent;
		public event Action OnCollisionWithStackBlockEvent;

		private void Awake()
		{
			_trailRenderer = GetComponentInChildren<TrailRenderer>();
			_particleSystem = GetComponentInChildren<ParticleSystem>();
		}

		private void Update()
		{
			CheckCollision();
		}

		private void CheckCollision()
		{
			Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(HalfSizeBox, HalfSizeBox, HalfSizeBox));

			foreach (Collider coll in colliders)
			{
				if (!coll.TryGetComponent(out PickUp pickUp)) 
					continue;
				OnCollisionWithStackBlockEvent?.Invoke();
				pickUp.gameObject.SetActive(false);
				return;
			}
			
			if (colliders.Any(coll => coll.TryGetComponent(out Obstacle _)))
			{
				OnCollisionWithObstacleEvent?.Invoke(this);
				StartCoroutine(DisableBlock());
			}
		}

		public void EnableTrail(bool enable)
		{
			_trailRenderer.enabled = enable;
		}

		public void PlayParticle()
		{
			_particleSystem.Play();
		}
		
		private IEnumerator DisableBlock()
		{
			yield return new WaitForSeconds(DisableTime);
			gameObject.SetActive(false);
		}

	}
}