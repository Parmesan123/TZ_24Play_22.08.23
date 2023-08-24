using System;

namespace Input
{
	public interface IGamePlayInput
	{
		public event Action<float> OnMoveEvent;
	}
}