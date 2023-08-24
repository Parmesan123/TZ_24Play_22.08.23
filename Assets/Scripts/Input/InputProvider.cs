using System.Collections.Generic;

namespace Input
{
	public class InputProvider
	{
		private readonly List<GameInput> _inputs;
		
		public InputProvider(List<GameInput> inputs)
		{
			_inputs = inputs;
		}

		public GameInput GetInput(InputType inputType)
		{
			GameInput gameInput = null;
			
			switch (inputType)
			{
				case InputType.GamePlayInput:
					gameInput = _inputs.Find(findInput => findInput is PcGamePlayInput);
					break;
				case InputType.PauseGameInput:
					gameInput = _inputs.Find(findInput => findInput is PauseGameInput);
					break;
				case InputType.EndGameInput:
					gameInput = _inputs.Find(findInput => findInput is EndGameInput);
					break;
			}

			return gameInput;
		}
	}
}