﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData {
    public string saveName;
    public string currentLevel;
    public Vector3 position;
    public string saveDate;
    public float playerHealth;
    public List<bool> eventTrigger = new List<bool>();
}
