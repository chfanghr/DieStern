using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Chapters
{
    [CreateAssetMenu(fileName = "NewChapterDatabase", menuName = "Chapters", order = 0)]
    public class ChapterDatabase : ScriptableObject
    {
        public List<string> chapters;
    }
}