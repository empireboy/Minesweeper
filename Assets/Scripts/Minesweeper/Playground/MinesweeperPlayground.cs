using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper
{
	[CreateAssetMenu (menuName = "Minesweeper/New MinesweeperPlayground", fileName = "MinesweeperPlayground.asset")]
	public class MinesweeperPlayground : ScriptableObject
	{
		[Header("Minesweeper Settings")]
		[Tooltip("Minesweeper horizontal amount of grid tiles")]
		public int width = 10;
		[Tooltip("Minesweeper vertical amount of grid tiles")]
		public int height = 10;

		[Header("Random Settings")]
		public int mineCount = 10;

		public MinesweeperGameManager.TileTypes[,] grid = new MinesweeperGameManager.TileTypes[0, 0];

		public void Randomize()
		{
			grid = new MinesweeperGameManager.TileTypes[width, height];

			for (int i = 0; i < mineCount; i++)
			{
				int mineX = Random.Range(0, width);
				int mineY = Random.Range(0, height);
				if (grid[mineX, mineY] != MinesweeperGameManager.TileTypes.Mine)
				{
					// Place a mine
					grid[mineX, mineY] = MinesweeperGameManager.TileTypes.Mine;
				}
				else
				{
					// If there already is a mine, keep the current mine in mind
					i--;
				}
			}

			// Loop trough every tile
			for (int spaceX = 0; spaceX < width; spaceX++)
			{
				for (int spaceY = 0; spaceY < height; spaceY++)
				{
					MinesweeperGameManager.TileTypes currentTileType = grid[spaceX, spaceY];

					if (currentTileType == MinesweeperGameManager.TileTypes.Mine)
						continue;

					int counter = 0;

					// Loop trough the tiles around a single tile
					for (int aroundHor = 0; aroundHor < 3; aroundHor++)
					{
						for (int aroundVert = 0; aroundVert < 3; aroundVert++)
						{

							/*Debug.Log(
								"spaceX: " + spaceX +
								" spaceY: " + spaceY +
								" aroundHor: " + aroundHor +
								" aroundVert: " + aroundVert +
								" aroundHorCalculated: " + Mathf.Clamp(spaceX - 1 + aroundHor, 0, width) +
								" aroundVertCalculated: " + Mathf.Clamp(spaceY - 1 + aroundVert, 0, height) +
								" mineAtCalculatedLocation: " + (grid[Mathf.Clamp(spaceX - 1 + aroundHor, 0, width - 1), Mathf.Clamp(spaceY - 1 + aroundVert, 0, height - 1)] == MinesweeperGameManager.TileTypes.Mine).ToString() +
								" typeAtCalculatedLocation: " + grid[Mathf.Clamp(spaceX - 1 + aroundHor, 0, width - 1), Mathf.Clamp(spaceY - 1 + aroundVert, 0, height - 1)]
							);*/

							if (
								grid[Mathf.Clamp(spaceX - 1 + aroundHor, 0, width - 1), Mathf.Clamp(spaceY - 1 + aroundVert, 0, height - 1)] == MinesweeperGameManager.TileTypes.Mine &&
								spaceX - 1 + aroundHor >= 0 && spaceX - 1 + aroundHor <= width - 1 &&
								spaceY - 1 + aroundVert >= 0 && spaceY - 1 + aroundVert <= height - 1
							)
							{
								// Check if its not the current space
								if (Mathf.Clamp(spaceX - 1 + aroundHor, 0, width - 1) != spaceX || Mathf.Clamp(spaceY - 1 + aroundVert, 0, height - 1) != spaceY)
									counter++;
							}
						}
					}

					switch (counter)
					{
						case 0:
							grid[spaceX, spaceY] = MinesweeperGameManager.TileTypes.Empty;
							break;
						case 1:
							grid[spaceX, spaceY] = MinesweeperGameManager.TileTypes.One;
							break;
						case 2:
							grid[spaceX, spaceY] = MinesweeperGameManager.TileTypes.Two;
							break;
						case 3:
							grid[spaceX, spaceY] = MinesweeperGameManager.TileTypes.Three;
							break;
						case 4:
							grid[spaceX, spaceY] = MinesweeperGameManager.TileTypes.Four;
							break;
						case 5:
							grid[spaceX, spaceY] = MinesweeperGameManager.TileTypes.Five;
							break;
						case 6:
							grid[spaceX, spaceY] = MinesweeperGameManager.TileTypes.Six;
							break;
						case 7:
							grid[spaceX, spaceY] = MinesweeperGameManager.TileTypes.Seven;
							break;
						case 8:
							grid[spaceX, spaceY] = MinesweeperGameManager.TileTypes.Eight;
							break;
					}
				}
			}
		}
	}
}