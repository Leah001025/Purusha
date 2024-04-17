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
    private bool isCutScenePlay =false;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        //isCutScenePlay = GameManager.Instance.User.isCutScenePlay;
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
            else if (!isCutScenePlay)
            {
                isCutScenePlay = true;
                //isCutScenePlay.Add(GameManager.Instance.stageID, true);
                playableDirector.Play(timeline[0]);
            }
        }
    }
}
