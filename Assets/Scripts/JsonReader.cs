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
    }
    void Start()
    {
        DataReader();
        Debug.Log("JSON Data Read");
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
    }
}
