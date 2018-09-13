using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper
{
	public class MinesweeperViewImage : MinesweeperViewBase<Image>
	{
		protected Image[,] _gridImage;

		public override void Init(MinesweeperPlayground minesweeperPlayground)
		{
			base.Init(minesweeperPlayground);

			_gridImage = new Image[_grid.GetLength(0), _grid.GetLength(1)];

			for (int i = 0; i < _grid.GetLength(0); i++)
			{
				for (int j = 0; j < _grid.GetLength(1); j++)
				{
					_gridImage[i, j] = _grid[i, j].GetComponent<Image>();
				}
			}
		}

		public override void OnTileDown(GameObject tile, int mouseButton)
		{
			tile.GetComponent<Image>().sprite = _emptyTileSprite;
		}

		public override void OnTileRelease(GameObject tile, int mouseButton)
		{
			tile.GetComponent<Image>().sprite = _defaultTileSprite;
		}

		public override void OnTileSelect(GameObject tile, int mouseButton)
		{
			tile.GetComponent<Image>().sprite = _emptyTileSprite;
		}

		public override void OnTileReveal(GameObject tile)
		{
			throw new System.NotImplementedException();
		}
	}
}