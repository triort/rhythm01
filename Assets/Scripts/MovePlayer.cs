using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public GameObject player;
    public JsonReader jsonReader;
    public float frameSpeed = 0.016f;
    public int frameRate = 60;
    public int noteNumber = 0;
    private bool gameStarted = false;

    void Awake()
    {
        Application.targetFrameRate = frameRate;
    }
    void Start()
    {

    }

    void Update()
    {
        if (!gameStarted)
        {
            Invoke(nameof(PlayerMove), 4f);
            gameStarted = true;
        }
    }

    private void PlayerMove()
    {
        Debug.Log($"Game Start");
        if (jsonReader.mapLoaded == true)
        {
            player.transform.position = new Vector3(
            jsonReader.noteDataList[0].pos_x,
            jsonReader.noteDataList[0].pos_y,
            0f
        );
        }
    }
}
