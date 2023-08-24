using Properties;
using System;

namespace Input
{
	public class PcPauseGameInput: GameInput, IUpdatable
	{
		public void Update()
		{
			CheckInput();
		}

		private void CheckInput()
		{
			if (!UnityEngine.Input.GetMouseButton(0))
				return;
			OnSwitchInputEvent?.Invoke(InputType.GamePlayInput);
		}
	}
}