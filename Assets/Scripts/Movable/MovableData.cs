using UnityEngine;

namespace Movable
{
	[CreateAssetMenu(fileName = "MovableData",menuName = "SO/MovableData")]
	public class MovableData: ScriptableObject
	{
		[SerializeField] private float forwardMoveSpeed;
		[SerializeField] private float widthMoveSpeed;
		
		public float ForwardMoveSpeed => forwardMoveSpeed;
		public float WidthMoveSpeed => widthMoveSpeed;
	}
}