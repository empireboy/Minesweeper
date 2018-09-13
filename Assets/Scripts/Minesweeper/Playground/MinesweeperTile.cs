using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Minesweeper
{
	public class MinesweeperTile : MonoBehaviour
	{
		private bool _selected;
		public bool Selected
		{
			get
			{
				return _selected;
			}
		}

		[ShowNonSerializedField] private int _gridSpaceHorizontal = -1;
		public int GridSpaceHorizontal
		{
			get
			{
				return _gridSpaceHorizontal;
			}
		}
		[ShowNonSerializedField] private int _gridSpaceVertical = -1;
		public int GridSpaceVertical
		{
			get
			{
				return _gridSpaceVertical;
			}
		}

		private MinesweeperGameManager.TileTypes _type = MinesweeperGameManager.TileTypes.None;
		public MinesweeperGameManager.TileTypes Type
		{
			get
			{
				return _type;
			}
		}


		public void SetSelected(bool selected)
		{
			_selected = selected;
		}

		public void SetGridSpace(int gridSpaceHorizontal, int gridSpaceVertical)
		{
			_gridSpaceHorizontal = gridSpaceHorizontal;
			_gridSpaceVertical = gridSpaceVertical;
		}

		public void SetType(MinesweeperGameManager.TileTypes type)
		{
			_type = type;
		}
	}
}