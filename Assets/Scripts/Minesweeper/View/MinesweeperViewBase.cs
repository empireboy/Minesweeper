using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper
{
	[RequireComponent(typeof(GridLayoutGroup))]
	[RequireComponent(typeof(RectTransform))]
	public abstract class MinesweeperViewBase<T> : MonoBehaviour where T : Component
	{
		[SerializeField] private T _defaultTile;
		[SerializeField] protected Sprite _defaultTileSprite;
		[SerializeField] protected Sprite _emptyTileSprite;
		[SerializeField] protected Sprite _mineTileSprite;
		[SerializeField] protected Sprite _mineSelectedTileSprite;
		[SerializeField] protected Sprite _flagMineTileSprite;
		[SerializeField] protected Sprite _oneTileSprite;
		[SerializeField] protected Sprite _twoTileSprite;
		[SerializeField] protected Sprite _threeTileSprite;
		[SerializeField] protected Sprite _fourTileSprite;
		[SerializeField] protected Sprite _fiveTileSprite;
		[SerializeField] protected Sprite _sixTileSprite;
		[SerializeField] protected Sprite _sevenTileSprite;
		[SerializeField] protected Sprite _eightTileSprite;
		[SerializeField] protected Sprite _flagTileSprite;

		protected T[,] _grid;

		private void Start()
		{
			Init(transform.root.GetComponentInChildren<MinesweeperGameManager>().MinesweeperPlayground);
		}

		public virtual void Init(MinesweeperPlayground minesweeperPlayground)
		{
			GetComponent<GridLayoutGroup>().constraintCount = minesweeperPlayground.width;

			_grid = new T[minesweeperPlayground.width, minesweeperPlayground.height];

			for (int i = 0; i < minesweeperPlayground.width; i++)
			{
				for (int j = 0; j < minesweeperPlayground.height; j++)
				{
					_grid[i, j] = GameObject.Instantiate(_defaultTile, transform).GetComponent<T>();
				}
			}

			MinesweeperGameManager minesweeperGameManager = transform.root.GetComponentInChildren<MinesweeperGameManager>();
			minesweeperGameManager.TileDownEvent += OnTileDown;
			minesweeperGameManager.TileReleaseEvent += OnTileRelease;
			minesweeperGameManager.TileSelectEvent += OnTileSelect;
			minesweeperGameManager.TileRevealEvent += OnTileReveal;
		}

		public abstract void OnTileDown(GameObject tile, int mouseButton);
		public abstract void OnTileRelease(GameObject tile, int mouseButton);
		public abstract void OnTileSelect(GameObject tile, int mouseButton);
		public abstract void OnTileReveal(GameObject tile);
	}
}