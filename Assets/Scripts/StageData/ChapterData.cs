using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ChapterData : DataBase<ChapterData>
{
    [SerializeField] public int _chapterId;
    [SerializeField] public string _chapterName;
    [SerializeField] public string _description;
    [SerializeField] public int _stageCount;

    public int ChapterID { get { return _chapterId; } set { _chapterId = value; } }
    public string ChapterName { get { return _chapterName; } set { _chapterName = value; } }
    public string Description { get { return _description; } set { _description = value; } }
    public int StageCount { get { return _stageCount; } set { _stageCount = value; } }
}
