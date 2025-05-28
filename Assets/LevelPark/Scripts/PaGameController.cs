using UnityEngine;
using UnityEngine.Tilemaps;

public class PaGameController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject wallHighEdge;

    private void Start()
    {

        for (int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
        {
            for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
            {
                TileBase tileBase = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tileBase != null)
                {
                    //Debug.Log(tileBase.name);
                    if (tileBase.name == "Ground_grass_0000_tile" || tileBase.name == "Ground_grass_0020_tile")
                    {
                        GameObject go = Instantiate(wallHighEdge, new Vector3(x - 0.2f, y + 1.2f, 0), Quaternion.Euler(0, 0, 0));
                        go.GetComponent<PaClimbable>().climbDirection = PaCat.Direction.Left;
                    }
                    else if (tileBase.name == "Ground_grass_0002_tile" || tileBase.name == "Ground_grass_0016_tile")
                    {
                        GameObject go = Instantiate(wallHighEdge, new Vector3(x + 1.2f, y + 1.2f, 0), Quaternion.Euler(0, 0, 0));
                        go.GetComponent<PaClimbable>().climbDirection = PaCat.Direction.Right;
                    }
                }

            }
        }

    }

}
