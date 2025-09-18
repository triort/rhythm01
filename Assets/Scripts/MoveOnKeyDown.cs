using UnityEngine;
using UnityEngine.InputSystem;

public class MoveOnKeyDown : MonoBehaviour
{
    public Vector2[,] posList;

    public int xIndex = 0;
    public int yIndex = 0;

    void Start()
    {
        // 例: 3x3 の座標リストを初期化（xyのみ）
        posList = new Vector2[3, 3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                posList[i, j] = new Vector2(i * 2.0f, j * 2.0f);
            }
        }
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
                    // Vector2 → Vector3 に変換（zは0固定）
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
}
