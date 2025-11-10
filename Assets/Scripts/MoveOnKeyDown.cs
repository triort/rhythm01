using System;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveOnKeyDown : MonoBehaviour
{
    [Serializable]
    private class MapMeta
    {
        public string title;
        public string artist;
    }

    [Serializable]
    private class MapRoot
    {
        public MapMeta meta;
    }

    public Vector2[,] posList;

    public int xIndex = 0;
    public int yIndex = 0;

    void Start()
    {
        posList = new Vector2[3, 3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                posList[i, j] = new Vector2(i * 2.0f, j * 2.0f);
            }
        }

        LoadMapInfo();
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            Debug.Log("Key was pushed");

            if (posList != null && posList.Length > 0)
            {
                if (xIndex < posList.GetLength(0) && yIndex < posList.GetLength(1))
                {
                    transform.position = new Vector3(posList[xIndex, yIndex].x, posList[xIndex, yIndex].y, 0f);
                    Debug.Log($"Moved to: {posList[xIndex, yIndex]}");

                    yIndex++;
                    if (yIndex >= posList.GetLength(1))
                    {
                        yIndex = 0;
                        xIndex++;
                        if (xIndex >= posList.GetLength(0))
                        {
                            xIndex = 0;
                        }
                    }
                }
            }
        }
    }

    private void LoadMapInfo()
    {
        string path = Path.Combine(Application.dataPath, "Map", "test.json");

        if (!File.Exists(path))
        {
            Debug.LogWarning($"Map file not found at {path}");
            return;
        }

        try
        {
            string json = File.ReadAllText(path);
            MapRoot map = JsonUtility.FromJson<MapRoot>(json);

            if (map != null && map.meta != null)
            {
                Debug.Log($"title: {map.meta.title}");
                Debug.Log($"artist: {map.meta.artist}");
            }
            else
            {
                Debug.LogWarning("Map metadata missing in test.json");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load map info: {ex.Message}");
        }
    }
}
