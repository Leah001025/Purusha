using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI text = playerData as TextMeshProUGUI;
        string curText = "";
        Color curColor = Color.white;
        float curAlpha = 0f;
        if(!text) { return; }

        int inputCount = playable.GetInputCount();
        for(int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if(inputWeight > 0f)
            {
                ScriptPlayable<SubtitleBehaviour> inputPlayable = (ScriptPlayable<SubtitleBehaviour>)playable.GetInput(i);

                SubtitleBehaviour input = inputPlayable.GetBehaviour();
                curText = input.subtitleText;
                curAlpha = inputWeight;
                curColor = input.nameColor;
            }
        }
        text.text = curText;
        text.color = new Color(curColor.r, curColor.g, curColor.b, curAlpha);
    }
}
