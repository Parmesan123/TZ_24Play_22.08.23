using Movable;
using UnityEngine;
using System;
using System.Collections.Generic;
using Pool;

namespace Stack
{
	public class CubeHolder: MonoBehaviour
	{
		private const string PathStackBlock = "Prefabs/Stack Block";
		private const float HeightBlock = 1.1f;
		
		[SerializeField] private StackBlock firstStackBlock;
		
		private PoolObjects<StackBlock> _poolStackBlock;
		private List<StackBlock> _stackBlocks;
		private MoveContainer _moveContainer;

		public event Action OnStackBlockEvent;
		public event Action OnRemoveBlockEvent;
		public event Action EndGameEvent;
		
		public void Init(MoveContainer moveContainer)
		{
			_stackBlocks = new List<StackBlock>();
			
			_moveContainer = moveContainer;
			AddStartBlock(firstStackBlock);
			
			StackBlock prefab = Resources.Load<StackBlock>(PathStackBlock);
			_poolStackBlock = new PoolObjects<StackBlock>(prefab, transform);
			_poolStackBlock.CreatePool(2);
		}

		private void AddStartBlock(StackBlock startBlock)
		{
			startBlock.OnCollisionWithObstacleEvent += RemoveBlock;
			startBlock.OnCollisionWithStackBlockEvent += AddBlock;
			
			_moveContainer.AddMovable(startBlock);
			_stackBlocks.Add(startBlock);
			
			startBlock.EnableTrail(true);
			
			OnStackBlockEvent?.Invoke();
		}
		
		private void AddBlock()
		{
			_poolStackBlock.GetFreeElement(out StackBlock newStackBlock);
			
			newStackBlock.OnCollisionWithObstacleEvent += RemoveBlock;
			newStackBlock.OnCollisionWithStackBlockEvent += AddBlock;
			
			newStackBlock.transform.position = _stackBlocks[^1].transform.position + new Vector3(0, HeightBlock, 0);
			
			newStackBlock.PlayParticle();
			_moveContainer.AddMovable(newStackBlock);
			_stackBlocks.Add(newStackBlock);
			
			OnStackBlockEvent?.Invoke();
		}

		private void RemoveBlock(StackBlock removeStackBlock)
		{
			OnRemoveBlockEvent?.Invoke();
			
			removeStackBlock.OnCollisionWithObstacleEvent -= RemoveBlock;
			removeStackBlock.OnCollisionWithStackBlockEvent -= AddBlock;
			
			_stackBlocks[0].EnableTrail(false);
			
			_moveContainer.RemoveMovable(removeStackBlock);
			_stackBlocks.Remove(removeStackBlock);

            if (CheckEndGame())
				return;
			
			_stackBlocks[0].EnableTrail(true);
		}
		
		private bool CheckEndGame()
		{
			if (_stackBlocks.Count != 0) 
				return false;
			
			EndGameEvent?.Invoke();
			return true;
		}
	}
}