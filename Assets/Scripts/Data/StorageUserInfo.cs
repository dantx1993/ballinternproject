using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public class StorageUserInfo : BaseSingleton<StorageUserInfo>
    {
        public List<ScoreData> HighScore;

        protected override void Init()
        {
            base.Init();
            HighScore = SaveAndLoad.LoadData<List<ScoreData>>(SaveKeyDefine.HIGHSCORE_KEY, new List<ScoreData>());
        }

        public void SaveNewHighScore(List<ScoreData> highscore)
        {
            HighScore = highscore;
            SaveAndLoad.SaveData<List<ScoreData>>(SaveKeyDefine.HIGHSCORE_KEY, HighScore);
        }
    }
}
