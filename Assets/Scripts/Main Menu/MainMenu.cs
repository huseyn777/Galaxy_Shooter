﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Load game scene
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
