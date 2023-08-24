using Game;
using Input;
using Level;
using Movable;
using Player;
using Stack;
using System.Collections.Generic;
using UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace EntryPoints
{
	public class SceneEntryPoint: MonoBehaviour
	{
        private const string PathCubeHolder = "Prefabs/CubeHolder";
        private const string PathPrefabPlayer = "Prefabs/Player";
        private const string PathCollectCubeText = "Prefabs/CollectCubeText";

		[SerializeField] private LevelBehavior levelBehavior;
		[SerializeField] private List<Platform> startPlatforms;
		[SerializeField] private Transform spawnPoint;
		[SerializeField] private InputBehavior inputBehavior;
		[SerializeField] private Transform moveContainer;
		[SerializeField] private GameObject pauseGamePanel;
		[SerializeField] private GameObject endGamePanel;
		[SerializeField] private Button resetButton;
		
		private CubeHolder _cubeHolder;
		private InputProvider _inputProvider;
		private PlayerBehavior _playerBehavior;
		private MoveContainer _moveContainer;
		private UIPanel _uiPanel;
		private Canvas _canvas;
		private EndGame _endGame;
		
		private void Awake()
		{
			InitInput();
			InitMoveObject();
			InitUI();
			InitStateGame();
			InitCubeHolder();
			InitCamera();
			InitLevel();
			Destroy(this);
		}

		private void InitInput()
		{
			_inputProvider = new InputProvider(new List<GameInput>(){new PcGamePlayInput(), 
												   new PauseGameInput(),
												   new EndGameInput()});
			inputBehavior.Init(_inputProvider.GetInput(InputType.PauseGameInput), _inputProvider);
		}
		
		private void InitMoveObject()
		{
			_moveContainer = new MoveContainer(_inputProvider.GetInput(InputType.GamePlayInput) as IGamePlayInput, moveContainer);
		}

		private void InitUI()
		{
			_canvas = Resources.Load<Canvas>(PathCollectCubeText);
			
			_uiPanel = new UIPanel(pauseGamePanel, endGamePanel, resetButton);
		}
		
		private void InitStateGame()
		{
			PauseGame pauseGame = new PauseGame(_inputProvider);
			_endGame = new EndGame();

			StateGame _ = new StateGame(_endGame, pauseGame, _uiPanel, inputBehavior);
		}

		private void InitCubeHolder()
		{
			_cubeHolder = Resources.Load<CubeHolder>(PathCubeHolder);
			_cubeHolder = Instantiate(_cubeHolder, spawnPoint.position, quaternion.identity);
			InitPlayer();
			_cubeHolder.Init(_moveContainer);
			_endGame.Init(_cubeHolder);

			void InitPlayer()
			{
				_playerBehavior = Resources.Load<PlayerBehavior>(PathPrefabPlayer);
				_playerBehavior = Instantiate(_playerBehavior, spawnPoint.position, quaternion.identity);
				_moveContainer.AddMovable(_playerBehavior);
				_playerBehavior.Init(_cubeHolder, _canvas);
			}
		}

		private void InitCamera()
		{
			CameraMover cameraMover = Camera.main.GetComponent<CameraMover>();
			_moveContainer.AddMovable(cameraMover);
			cameraMover.Init(_cubeHolder);
		}
		
		private void InitLevel()
		{
			levelBehavior.Init(_playerBehavior, startPlatforms);
		}
	}
}