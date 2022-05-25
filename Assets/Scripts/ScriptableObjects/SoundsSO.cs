using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Sounds", fileName = "Sounds")]
public class SoundsSO : ScriptableObject
{

    public AudioSystem.SoundAudioClip[] sounds;
}
