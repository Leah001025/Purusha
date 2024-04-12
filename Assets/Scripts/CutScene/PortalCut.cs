using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PortalCut : MonoBehaviour
{
    private PlayableDirector playableDirector;
    public TimelineAsset[] timeline;
    private bool isCutScenePlay;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"&&!isCutScenePlay)
        {
            isCutScenePlay = true;
            playableDirector.Play(timeline[0]);
        }
    }
}
