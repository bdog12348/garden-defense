using UnityEngine;
using UnityEditor;

public class WaveCreationWindow : EditorWindow
{
    [MenuItem("Window/Wave Creator")]
    public static void ShowWindow()
    {
        GetWindow<WaveCreationWindow>("Wave Creator");
    }

    private void OnGUI()
    {
        
    }
}
