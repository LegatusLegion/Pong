using System.Collections.Generic;
using System;


namespace Pong.Scoreboards
{
    [Serializable]
    public class ScoreboardSaveData
    {
        
        public List<ScoreboardEntryData> highscores = new List<ScoreboardEntryData>();
    }
}

