using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper
{
	public class MinesweeperViewTile : MinesweeperViewBase<MinesweeperTile>
	{
		protected MinesweeperTile[,] _gridTile;

		public override void Init(MinesweeperPlayground minesweeperPlayground)
		{
			base.Init(minesweeperPlayground);

			_gridTile = new MinesweeperTile[_grid.GetLength(0), _grid.GetLength(1)];
			
			for (int i = 0; i < _grid.GetLength(0); i++)
			{
				for (int j = 0; j < _grid.GetLength(1); j++)
				{
					_gridTile[i, j] = _grid[i, j].GetComponent<MinesweeperTile>();
					_gridTile[i, j].SetGridSpace(i, j);
					_gridTile[i, j].SetType(minesweeperPlayground.grid[i, j]);
				}
			}
		}

		public override void OnTileDown(GameObject tile, int mouseButton)
		{
			if (!tile.GetComponent<MinesweeperTile>().Selected && mouseButton == 0 && tile.GetComponent<Image>().sprite != _flagTileSprite)
				tile.GetComponent<Image>().sprite = _emptyTileSprite;
		}

		public override void OnTileRelease(GameObject tile, int mouseButton)
		{
			if (!tile.GetComponent<MinesweeperTile>().Selected && mouseButton == 0 && tile.GetComponent<Image>().sprite != _flagTileSprite)
				tile.GetComponent<Image>().sprite = _defaultTileSprite;
		}

		public override void OnTileSelect(GameObject tile, int mouseButton)
		{
			if (mouseButton == 0)
			{
				if (tile.GetComponent<Image>().sprite != _flagTileSprite)
					MinesweeperGameManager.Instance.RevealTile(tile);

				// Reveal Mines
				if (tile.GetComponent<MinesweeperTile>().Type == MinesweeperGameManager.TileTypes.Mine)
				{
					RevealAllMines();
					tile.GetComponent<Image>().sprite = _mineSelectedTileSprite;
					FindObjectOfType<GraphicRaycasterHandler>().enabled = false;
				}
			}
			else if (mouseButton == 1)
			{
				if (!tile.GetComponent<MinesweeperTile>().Selected)
				{
					if (tile.GetComponent<Image>().sprite != _flagTileSprite)
					{
						tile.GetComponent<Image>().sprite = _flagTileSprite;
					}
					else
					{
						tile.GetComponent<Image>().sprite = _defaultTileSprite;
					}
				}
			}
		}

		public override void OnTileReveal(GameObject tile)
		{
			if (tile.GetComponent<Image>().sprite != _flagTileSprite)
				RevealTile(tile);

			// Start FloodFill
			if (tile.GetComponent<MinesweeperTile>().Type == MinesweeperGameManager.TileTypes.Empty && tile.GetComponent<Image>().sprite == _emptyTileSprite)
				FloodFill(tile);
		}

		private void FloodFill(GameObject currentTile)
		{
			MinesweeperTile minesweeperTileCurrent = currentTile.GetComponent<MinesweeperTile>();

			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					GameObject nextTile = null;

					if (
						minesweeperTileCurrent.GridSpaceHorizontal + i >= 0 &&
						minesweeperTileCurrent.GridSpaceHorizontal + i <= _gridTile.GetLength(0) - 1 &&
						minesweeperTileCurrent.GridSpaceVertical + j >= 0 &&
						minesweeperTileCurrent.GridSpaceVertical + j <= _gridTile.GetLength(1) - 1
					)
					{
						nextTile = _gridTile[minesweeperTileCurrent.GridSpaceHorizontal + i, minesweeperTileCurrent.GridSpaceVertical + j].gameObject;
						if (nextTile.GetComponent<Image>().sprite == _emptyTileSprite || nextTile == currentTile)
							nextTile = null;
					}
					if (nextTile != null)
						MinesweeperGameManager.Instance.RevealTile(nextTile);
				}
			}
		}

		private void RevealTile(GameObject tile)
		{
			MinesweeperTile minesweeperTile = tile.GetComponent<MinesweeperTile>();
			minesweeperTile.SetSelected(true);

			switch (minesweeperTile.Type)
			{
				case MinesweeperGameManager.TileTypes.Empty:
					tile.GetComponent<Image>().sprite = _emptyTileSprite;
					break;
				case MinesweeperGameManager.TileTypes.Mine:
					tile.GetComponent<Image>().sprite = _mineTileSprite;
					break;
				case MinesweeperGameManager.TileTypes.One:
					tile.GetComponent<Image>().sprite = _oneTileSprite;
					break;
				case MinesweeperGameManager.TileTypes.Two:
					tile.GetComponent<Image>().sprite = _twoTileSprite;
					break;
				case MinesweeperGameManager.TileTypes.Three:
					tile.GetComponent<Image>().sprite = _threeTileSprite;
					break;
				case MinesweeperGameManager.TileTypes.Four:
					tile.GetComponent<Image>().sprite = _fourTileSprite;
					break;
				case MinesweeperGameManager.TileTypes.Five:
					tile.GetComponent<Image>().sprite = _fiveTileSprite;
					break;
				case MinesweeperGameManager.TileTypes.Six:
					tile.GetComponent<Image>().sprite = _sixTileSprite;
					break;
				case MinesweeperGameManager.TileTypes.Seven:
					tile.GetComponent<Image>().sprite = _sevenTileSprite;
					break;
				case MinesweeperGameManager.TileTypes.Eight:
					tile.GetComponent<Image>().sprite = _eightTileSprite;
					break;
			}
		}

		private void RevealAllMines()
		{
			for (int i = 0; i < _gridTile.GetLength(0); i++)
			{
				for (int j = 0; j < _gridTile.GetLength(1); j++)
				{
					GameObject currentTile = _gridTile[i, j].gameObject;

					if (currentTile.GetComponent<Image>().sprite == _flagTileSprite && currentTile.GetComponent<MinesweeperTile>().Type != MinesweeperGameManager.TileTypes.Mine)
					{
						currentTile.GetComponent<Image>().sprite = _flagMineTileSprite;
					}
					else if (currentTile.GetComponent<MinesweeperTile>().Type == MinesweeperGameManager.TileTypes.Mine && currentTile.GetComponent<Image>().sprite != _flagTileSprite)
					{
						MinesweeperGameManager.Instance.RevealTile(currentTile);
					}
				}
			}
		}
	}
}