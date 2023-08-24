using System;

namespace Input
{
	public abstract class GameInput
	{
		public Action<InputType> OnSwitchInputEvent;
	}
}