using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXPlaylist", menuName = "Pipes/SFXPlaylist")]
public class Pipe_SoundsPlay : ScriptableObject
{
    public List<PlayClipData> AwaitingClips { get; } = new List<PlayClipData>();

    public void AddClip(PlayClipData playClipData)
    {
        AwaitingClips.Add(playClipData);
    }
}

[System.Serializable]
public struct PlayClipData
{
    public ClipsCollection clipCollection;
    public Vector3 position;

    public PlayClipData(ClipsCollection clipCollection, Vector3 position)
    {
        this.clipCollection = clipCollection;
        this.position = position;
    }
}