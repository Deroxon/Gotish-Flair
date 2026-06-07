using UnityEngine;

public class worldScrolling : MonoBehaviour
{

    [SerializeField] Transform playerTransform;
    [SerializeField] Vector2Int currentTilePosition = new Vector2Int(0,0);
    [SerializeField] Vector2Int playerTilePosition;
    [SerializeField] Vector2Int onTileGridPlayerPosition;
    [SerializeField] float tileSize = 20f;


    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;
    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidht = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Start()
    {
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.y = (int)(playerTransform.position.y / tileSize);

        currentTilePosition = playerTilePosition;

        UpdatesTilesOnScreen();
    }

    private void Update()
    {
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.y = (int)(playerTransform.position.y / tileSize);

        playerTilePosition.x -= playerTransform.position.x < 0 ? 1 : 0;
        playerTilePosition.y -= playerTransform.position.y < 0 ? 1 : 0;

        if(currentTilePosition != playerTilePosition)
        {
            currentTilePosition = playerTilePosition;

            onTileGridPlayerPosition.x = CalculatePosition(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.y = CalculatePosition(onTileGridPlayerPosition.y, false);
            UpdatesTilesOnScreen();
        }
    }

    private void UpdatesTilesOnScreen()
    {
        for(int pov_x = -(fieldOfVisionWidht/2); pov_x <= fieldOfVisionWidht/2; pov_x++)
        {
            for(int pov_y = -(fieldOfVisionHeight/2); pov_y <= fieldOfVisionHeight/2; pov_y++)
            {
                int tileToUpdate_x = CalculatePosition(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePosition(playerTilePosition.y + pov_y, false);

                                Debug.Log(
                    $"tileToUpdate_x={tileToUpdate_x}, tileToUpdate_y={tileToUpdate_y}"
                );
                                Debug.Log(
                    $"Array size = [{terrainTileHorizontalCount},{terrainTileVerticalCount}]"
                );
                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                tile.transform.position = CalculateTilePosition(playerTilePosition.x + pov_x, playerTilePosition.y + pov_y);
            }
        }
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {


        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    private int CalculatePosition(int value, bool horizontal)
    {
        int size = horizontal ? terrainTileHorizontalCount : terrainTileVerticalCount;

        return ((value % size) + size) % size;
    }

    internal void Add(GameObject tileGameObject, Vector2Int tilePosition)
    {
        terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }
}
