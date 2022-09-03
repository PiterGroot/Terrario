using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    private WorldGeneration worldGeneration;
    private bool DebugMenuState = false;
    public static float fps;
    private GUIStyle Shadow = new GUIStyle();
    private GUIStyle FPSText = new GUIStyle();

    private void Awake() {
        worldGeneration = gameObject.GetComponent<WorldGeneration>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F3)){
            DebugMenuState = !DebugMenuState;
        }
    }
    private void FixedUpdate() {
        if(DebugMenuState){
            //GUI LABEL STUFF
            FPSText.fontSize = 20;
            FPSText.fontStyle = FontStyle.Bold;
            FPSText.normal.textColor = Color.black;
            Shadow.fontSize = 20;
            Shadow.fontStyle = FontStyle.Bold;
            Shadow.normal.textColor = Color.white;
        }
        else{
            FPSText.normal.textColor = new Color(0, 0 , 0 , 0);
            Shadow.normal.textColor = new Color(0, 0 , 0 , 0);
        }
    }
    private void OnGUI(){ 
        float newFPS = 1.0f / Time.smoothDeltaTime;
        fps = Mathf.Lerp(fps, newFPS, 0.0005f);
        GUI.Label(new Rect(25, 22, 100, 100), "FPS: " + ((int)fps).ToString(), FPSText);
        GUI.Label(new Rect(25, 20, 100, 100), "FPS: " + ((int)fps).ToString(), Shadow);
        GUI.Label(new Rect(25, 47, 100, 100), $"XYZ: {worldGeneration.Character.position}", FPSText);
        GUI.Label(new Rect(25, 45, 100, 100), $"XYZ: {worldGeneration.Character.position}", Shadow);
    }
    private void OnDrawGizmos()
    {
        if(DebugMenuState){
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(worldGeneration.Character.position, worldGeneration.Character.gameObject.GetComponent<BoxCollider2D>().size);
        }
    }

}
