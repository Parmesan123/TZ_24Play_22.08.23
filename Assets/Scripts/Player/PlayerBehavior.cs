using Properties;
using Stack;
using Unity.Mathematics;
using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerBehavior: MonoBehaviour, IMovable
	{
		private const float HeightBlock = 1f;
		private static readonly int Jump = Animator.StringToHash("Jump");
        
		private Animator _animator;
		private Canvas _canvas;
		
		public Transform Transform() => transform;
		public Vector3 PlayerPosition => transform.position;
		
		public void Init(CubeHolder cubeHolder, Canvas canvas)
		{
			cubeHolder.OnStackBlockEvent += BlockPickUp;
			_animator = GetComponentInChildren<Animator>();
			_canvas = canvas;
		}

		private void BlockPickUp()
		{
			RaiseModel();
			PlayAnim();
			CreateText();
		}

		private void RaiseModel() => transform.localPosition = new Vector3(0, PlayerPosition.y + HeightBlock, 0);
		
		private void PlayAnim() => _animator.SetTrigger(Jump);

		
		private void CreateText()
		{
			Canvas canvas = Instantiate(_canvas, PlayerPosition, quaternion.identity);
			
			Destroy(canvas.gameObject, 2f);
		}
	}
}