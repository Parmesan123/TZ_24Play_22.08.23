using Input;
using UI;
using UnityEngine.SceneManagement;

namespace Game
{
	public class StateGame
	{
		private readonly UIPanel _uiPanel;
		private readonly InputBehavior _inputBehavior;
		
		public StateGame(EndGame endGame, PauseGame pauseGame, UIPanel uiPanel, InputBehavior inputBehavior)
		{
			_uiPanel = uiPanel;
			_inputBehavior = inputBehavior;
			endGame.EndGameEvent += EndGame;
			pauseGame.PauseGameEvent += PauseGame;
			_uiPanel.OnResetButtonPressed += ResetGame;
		}

		private void ResetGame()
		{
			SceneManager.LoadScene("GameScene");
		}
		private void EndGame()
		{
			_uiPanel.EnableEndGamePanel(true);
			_inputBehavior.SwitchInputSystem(InputType.EndGameInput);
		}

		private void PauseGame(bool enable)
		{
			_uiPanel.EnablePauseGamePanel(enable);
		}
	}
}