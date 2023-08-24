using Input;
using System;
using UnityEngine;

namespace Game
{
	public class PauseGame
	{
		public event Action<bool> PauseGameEvent;
		
		public PauseGame(InputProvider inputProvider)
		{
			inputProvider.GetInput(InputType.GamePlayInput).OnSwitchInputEvent += Pause;
			inputProvider.GetInput(InputType.PauseGameInput).OnSwitchInputEvent += Pause;
		}

		private void Pause(InputType inputType)
		{
			switch (inputType)
			{
				case InputType.GamePlayInput:
					PauseGameEvent?.Invoke(false);
					break;
				case InputType.PauseGameInput:
					PauseGameEvent?.Invoke(true);
					break;
			}
		}
	}
}