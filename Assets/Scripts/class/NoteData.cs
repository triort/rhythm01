using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class NoteData
{
    public float time;
    public float pos_x;
    public float pos_y;
    public int type;

    public NoteData()
    {
        time = 0f;
        pos_x = 0f;
        pos_y = 0f;
        type = 0;
    }
}