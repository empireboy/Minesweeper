using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minesweeper
{
	public class MinesweeperGameManager : MonoBehaviour
	{
		public enum TileTypes
		{
			Empty,
			Mine,
			One,
			Two,
			Three,
			Four,
			Five,
			Six,
			Seven,
			Eight,
			None = -1
		}

		public delegate void TileDownHandler(GameObject tile, int mouseButton);
		public event TileDownHandler TileDownEvent;
		public delegate void TileReleaseHandler(GameObject tile, int mouseButton);
		public event TileReleaseHandler TileReleaseEvent;
		public delegate void TileSelectHandler(GameObject tile, int mouseButton);
		public event TileSelectHandler TileSelectEvent;
		public delegate void TileRevealHandler(GameObject tile);
		public event TileRevealHandler TileRevealEvent;

		[Tooltip("Current MinesweeperPlayground")]
		[SerializeField] private MinesweeperPlayground minesweeperPlayground;
		public MinesweeperPlayground MinesweeperPlayground
		{
			get
			{
				return minesweeperPlayground;
			}
		}

		private void Awake()
		{
			minesweeperPlayground.Randomize();
		}

		private void Start()
		{
			transform.root.GetComponentInChildren<GraphicRaycasterHandler>().MouseDownEvent += OnTileDown;
			transform.root.GetComponentInChildren<GraphicRaycasterHandler>().MouseReleasedEvent += OnTileRelease;
			transform.root.GetComponentInChildren<GraphicRaycasterHandler>().SelectEvent += OnTileSelect;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);

			if (Input.GetKeyDown(KeyCode.Escape))
				Application.Quit();
		}

		private void OnTileDown(GameObject tile, int mouseButton)
		{
			if (TileDownEvent != null)
				TileDownEvent(tile, mouseButton);
		}

		private void OnTileRelease(GameObject tile, int mouseButton)
		{
			if (TileReleaseEvent != null)
				TileReleaseEvent(tile, mouseButton);
		}

		private void OnTileSelect(GameObject tile, int mouseButton)
		{
			if (TileSelectEvent != null)
				TileSelectEvent(tile, mouseButton);
		}

		public void RevealTile(GameObject tile)
		{
			if (TileRevealEvent != null)
				TileRevealEvent(tile);
		}
	}
}