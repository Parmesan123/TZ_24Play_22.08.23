using Properties;
using System;

namespace Input
{
	public class PauseGameInput: GameInput, IUpdatable
	{
		public void Update()
		{
			CheckTouch();
		}

		private void CheckTouch()
		{
			if (!UnityEngine.Input.GetMouseButton(0))
				return;
			OnSwitchInputEvent?.Invoke(InputType.GamePlayInput);
		}

	}
}