using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Minesweeper
{
	[CustomEditor(typeof(MinesweeperPlayground))]
	public class MinesweeperPlaygroundEditor : Editor
	{
		MinesweeperPlayground minesweeper;

		public void OnEnable()
		{
			minesweeper = (MinesweeperPlayground)target;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (minesweeper.grid.Length <= 0)
			{
				if (GUILayout.Button("Generate Random Grid", EditorStyles.miniButton))
					minesweeper.Randomize();
			}
			else
			{
				if (GUILayout.Button("Update Random Grid", EditorStyles.miniButton))
					minesweeper.Randomize();
			}

			EditorUtility.SetDirty(minesweeper);
		}
	}
}