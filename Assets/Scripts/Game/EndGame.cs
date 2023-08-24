using Player;
using Stack;
using System;

namespace Game
{
	public class EndGame
	{
		public event Action EndGameEvent; 
		
		public void Init(CubeHolder cubeHolder)
		{
			cubeHolder.EndGameEvent += GameOver;
		}

		private void GameOver()
		{
			EndGameEvent?.Invoke();
		}
	}
}