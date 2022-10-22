using System;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    [Serializable]
    public class ScoreData
    {
        public string name;
        public int score;

        public ScoreData() { }
        public ScoreData(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
}