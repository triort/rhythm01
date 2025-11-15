using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

// #include "Assets/Scripts/class/NoteData.cs"

public class JsonReader : MonoBehaviour
{
    public const string FILE_PATH = "Assets/Map/test.json";
    public List<NoteData> noteDataList = new List<NoteData>();

    public GameObject userCube;
    public GameObject notePrefab;
    private double BPM = 100.0;
    private double startTime = 0.0;
    private double endTime = 0.0;

    public float flashDuration = 0.5f;

    private float beatInterval = 1f;
    private float timer;
    private int precount = 0;
    public bool mapLoaded = false;


    void Start()
    {
        DataReader();
        Debug.Log("JSON Data Read");
        // 3秒待って譜面生成
        Invoke(nameof(LoadMap), 3f);
        GameStart();
    }
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            for (int i = 0; i < noteDataList.Count; i++)
            {
                Debug.Log($"Note {i}: time={noteDataList[i].time}, pos_x={noteDataList[i].pos_x}, pos_y={noteDataList[i].pos_y}, type={noteDataList[i].type}");
            }
        }

        timer += Time.deltaTime;
        if (timer >= beatInterval)
        {
            precount++;
            if (precount <= 4)
            {
                StartCoroutine(Flash());
                timer -= beatInterval;
                Debug.Log($"count: {precount}");
            }
        }
    }
    private void DataReader()
    {
        string jsonString = File.ReadAllText(FILE_PATH, Encoding.UTF8);
        JsonData jsonData = JsonMapper.ToObject(jsonString);

        for (int i = 0; i < jsonData["notes"].Count; i++)
        {
            NoteData noteData = new NoteData();
            noteData.time = Convert.ToSingle((double)jsonData["notes"][i]["time"]);
            noteData.pos_x = Convert.ToSingle((double)jsonData["notes"][i]["pos_x"]);
            noteData.pos_y = Convert.ToSingle((double)jsonData["notes"][i]["pos_y"]);
            noteData.type = Convert.ToInt32((long)jsonData["notes"][i]["type"]);

            noteDataList.Add(noteData);
        }
        BPM = Convert.ToSingle((double)jsonData["meta"]["bpm"]);
        startTime = Convert.ToSingle((double)jsonData["meta"]["startTime"]);
        endTime = Convert.ToSingle((double)jsonData["meta"]["endTime"]);

        Debug.Log($"BPM: {BPM}");
        Debug.Log($"Start Time: {startTime}");
        Debug.Log($"End Time: {endTime}");

        beatInterval = 60f / (float)BPM;
        Debug.Log($"Beat Interval: {beatInterval}");
        mapLoaded = true;
    }

    void LoadMap()
    {
        foreach (var note in noteDataList)
        {
            Instantiate(notePrefab, new Vector3(note.pos_x, note.pos_y, 0f), Quaternion.identity);
        }
    }

    void GameStart()
    {

    }

    void CountDown()
    {

    }

    private System.Collections.IEnumerator Flash()
    {
        // オブジェクトを一瞬明るく
        userCube.GetComponent<Renderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(flashDuration);
        userCube.GetComponent<Renderer>().material.color = Color.white;
    }

}
