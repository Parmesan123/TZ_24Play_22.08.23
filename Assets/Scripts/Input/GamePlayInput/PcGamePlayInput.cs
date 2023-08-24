using System;
using UnityEngine;

namespace Input
{
	public class PcGamePlayInput:GameInput, IGamePlayInput, IFixedUpdatable
	{
		public event Action<float> OnMoveEvent;
		
		public void FixedUpdate()
		{
			MoveInput();
		}

		private void MoveInput()
		{
			if(UnityEngine.Input.GetMouseButton(0))
			{
				float xValue = UnityEngine.Input.mousePosition.x * 2 / Screen.width - 1;
				OnMoveEvent?.Invoke(xValue);
				return;
			}
			OnSwitchInputEvent?.Invoke(InputType.PauseGameInput);
		}
	}
}