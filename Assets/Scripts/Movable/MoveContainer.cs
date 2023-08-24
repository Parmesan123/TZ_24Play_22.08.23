using Input;
using Properties;
using UnityEngine;

namespace Movable
{
	public class MoveContainer
	{
		private const string PathMovableData = "Data/MovableData";
		private const float WidthLimit = 2f;
		
		private readonly MovableData _movableData;
		private readonly Transform _moveContainer;

		private Vector3 PositionContainer => _moveContainer.position;
		
		public MoveContainer(IGamePlayInput gamePlayInput, Transform moveContainer)
		{
			gamePlayInput.OnMoveEvent += Move;
			_moveContainer = moveContainer;
			_movableData = Resources.Load<MovableData>(PathMovableData);
		}
        
		private void Move(float xValue)
		{
			Vector3 newPosition = PositionContainer + _movableData.ForwardMoveSpeed * Time.fixedDeltaTime * Vector3.forward;
			newPosition = new Vector3(xValue * WidthLimit,newPosition.y,newPosition.z);

			if (Mathf.Abs(newPosition.x) > WidthLimit)
			{
				float limitX = WidthLimit * newPosition.x / Mathf.Abs(newPosition.x);
				
				newPosition = new Vector3(limitX, newPosition.y, newPosition.z);
			}

			_moveContainer.position = newPosition;
		}

		public void AddMovable(IMovable movable)
		{
			movable.Transform().SetParent(_moveContainer);
		}

		public void RemoveMovable(IMovable movable)
		{
			movable.Transform().SetParent(null);
		}
	}
}