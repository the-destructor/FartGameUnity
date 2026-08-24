using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections.Generic;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class ButtonSendToScene : MonoBehaviour
{
    public void ButtonPress(int value)
    {
        SceneManager.LoadScene(value);
    }
}
