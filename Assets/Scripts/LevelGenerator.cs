using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Texture2D map;

    public GameObject aStar;

    public ColorToPrefab[] colorMappings;

	// Update is called once per frame
	void Start () {
        GenerateLevel();
        aStar.GetComponent<Grid>().AwakeGrid();
        aStar.GetComponent<Pathfinding>().AwakePathfind();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            // checks for transparent pixel
            return;
        }

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector3 position = new Vector3(x, y, -.1f);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}
