using Properties;
using Stack;
using System.Collections;
using UnityEngine;

namespace Movable
{
	public class CameraMover: MonoBehaviour, IMovable
	{
		[SerializeField] private float shakeForce = 0.1f;
		[SerializeField] private float cameraShakeDuration = 0.4f;

		private Vector3 CameraPosition => transform.position;
		
        public Transform Transform() => transform;

		private bool _shakeCameraEnabled;
		
		public void Init(CubeHolder cubeHolder)
		{
			cubeHolder.OnRemoveBlockEvent += ShakeCamera;
		}
		
		private void ShakeCamera()
		{
			if (_shakeCameraEnabled)
				return;

			_shakeCameraEnabled = true;
			Vector3 startPos = transform.localPosition;
			
			StartCoroutine(ShakeRoutine());

			IEnumerator ShakeRoutine()
			{
				float timer = cameraShakeDuration;

				while (timer > 0)
				{
					Vector3 shakePos = Random.insideUnitSphere * shakeForce;
					transform.localPosition += new Vector3(shakePos.x, shakePos.y, 0);
					
					timer -= Time.fixedDeltaTime;
					yield return new WaitForFixedUpdate();
					
					transform.localPosition = startPos;
				}

				transform.localPosition = startPos;
				_shakeCameraEnabled = false;
			}
		}
	}
}