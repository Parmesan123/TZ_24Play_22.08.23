using UnityEngine;
using Properties;

namespace Input
{
	public class InputBehavior: MonoBehaviour
	{
		private InputProvider _inputProvider;
		private GameInput _gameInput;
		private IUpdatable _updatableInput;
		private IFixedUpdatable _fixedUpdatableInput;

		private void Update()
		{
			_updatableInput?.Update();
		}

		private void FixedUpdate()
		{
			_fixedUpdatableInput?.FixedUpdate();
		}

		public void Init(GameInput startInput, InputProvider inputProvider)
		{
			_inputProvider = inputProvider;
			_gameInput = startInput;
			_gameInput.OnSwitchInputEvent += SwitchInputSystem;
			_updatableInput = _gameInput as IUpdatable;
			_fixedUpdatableInput = _gameInput as IFixedUpdatable;
		}
		
		public void SwitchInputSystem(InputType inputType)
		{
			GameInput newGameInput = _inputProvider.GetInput(inputType);
			_gameInput.OnSwitchInputEvent -= SwitchInputSystem;
			_gameInput = newGameInput;
			_gameInput.OnSwitchInputEvent += SwitchInputSystem;
			_updatableInput = newGameInput as IUpdatable;
			_fixedUpdatableInput = newGameInput as IFixedUpdatable;
		}
	}
}