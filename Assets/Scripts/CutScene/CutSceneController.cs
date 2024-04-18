using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutSceneController : MonoBehaviour
{
    private PlayableDirector playableDirector;
    public TimelineAsset[] timeline;
    //private Dictionary<int, bool> isCutScenePlay;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (SceneLoadManager.Instance.NowSceneName == "IntroScene")
            {
                playableDirector.Play(timeline[0]);
                return;
            }
            else if (!GameManager.Instance.User.isCutScenePlay)
            {
                GameManager.Instance.User.isCutScenePlay = true;
                //isCutScenePlay.Add(GameManager.Instance.stageID, true);
                playableDirector.Play(timeline[0]);
            }
        }
    }
}
