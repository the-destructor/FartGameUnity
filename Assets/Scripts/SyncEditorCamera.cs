using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class SyncEditorCamera : MonoBehaviour
{
    void Update()
    {
        // Only run inside the Unity Editor, not in the built game
        #if UNITY_EDITOR
        if (!Application.isPlaying && SceneView.lastActiveSceneView != null)
        {
            var sceneCam = SceneView.lastActiveSceneView.camera;
            if (sceneCam != null)
            {
                transform.position = sceneCam.transform.position;
            }
        }
        #endif
    }
}