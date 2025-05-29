using UnityEngine;
using UnityEngine.Tilemaps;

public class PaGameController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject wallHighEdge;
    [SerializeField] GameObject keepCrouch;

    private void Start()
    {

        for (int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
        {
            for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
            {
                TileBase tileBase = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tileBase != null)
                {
                    //Climb
                    if (tileBase.name == "Ground_grass_0000_tile" || tileBase.name == "Ground_grass_0020_tile" || tileBase.name == "Ground_grass_0022_tile")
                    {
                        GameObject go = Instantiate(wallHighEdge, new Vector3(x - 0.2f, y + 1.2f, 0), Quaternion.Euler(0, 0, 0));
                        go.GetComponent<PaClimbable>().climbDirection = PaCat.Direction.Left;
                    }
                    if (tileBase.name == "Ground_grass_0002_tile" || tileBase.name == "Ground_grass_0016_tile" || tileBase.name == "Ground_grass_0022_tile")
                    {
                        GameObject go = Instantiate(wallHighEdge, new Vector3(x + 1.2f, y + 1.2f, 0), Quaternion.Euler(0, 0, 0));
                        go.GetComponent<PaClimbable>().climbDirection = PaCat.Direction.Right;
                    }


                    if (tileBase.name == "Ground_grass_0006_tile")
                    {
                        Instantiate(keepCrouch, new Vector3(x + 1.2f, y - 0.2f, 0), Quaternion.Euler(0, 0, 0));
                    }
                    else if (tileBase.name == "Ground_grass_0008_tile")
                    {
                        Instantiate(keepCrouch, new Vector3(x - 0.2f, y - 0.2f, 0), Quaternion.Euler(0, 0, 0));
                    }
                    else if (tileBase.name == "Ground_grass_0007_tile")
                    {
                        Instantiate(keepCrouch, new Vector3(x + 1.2f, y - 0.2f, 0), Quaternion.Euler(0, 0, 0));
                        Instantiate(keepCrouch, new Vector3(x - 0.2f, y - 0.2f, 0), Quaternion.Euler(0, 0, 0));
                    }
                    else if (tileBase.name == "Ground_grass_0022_tile")
                    {
                        Instantiate(keepCrouch, new Vector3(x + 0.5f, y - 0.2f, 0), Quaternion.Euler(0, 0, 0));
                    }
                }

            }
        }

    }

}
