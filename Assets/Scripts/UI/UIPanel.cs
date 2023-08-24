using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UIPanel
	{
		public event Action OnResetButtonPressed;
		
		private readonly GameObject _pauseGamePanel;
		private readonly GameObject _endGamePanel;
		private readonly Button _restartButton;
		
		public UIPanel(GameObject pauseGamePanel, GameObject endGamePanel, Button restartButton)
		{
			_pauseGamePanel = pauseGamePanel;
			_endGamePanel = endGamePanel;
			_restartButton = restartButton;
			_restartButton.onClick.AddListener(PressedResetButton);
		}

		public void EnablePauseGamePanel(bool enable)
		{
			_pauseGamePanel.SetActive(enable);
		}
		public void EnableEndGamePanel(bool enable)
		{
			_endGamePanel.SetActive(enable);
		}

		private void PressedResetButton()
		{
			OnResetButtonPressed?.Invoke();
		}
	}
}