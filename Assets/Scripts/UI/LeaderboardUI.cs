using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public class LeaderboardUI : MonoBehaviour
    {
        [SerializeField] private RankItemUI _rankItemPrefab;
        [SerializeField] private Transform _rankItemParent;

        [SerializeField] private GameObject _goList;
        [SerializeField] private GameObject _goNoList;

        public void SetData(List<ScoreData> highscore)
        {
            _goList.SetActive(highscore.Count > 0);
            _goNoList.SetActive(highscore.Count <= 0);
            if(highscore.Count <= 0) return;
            _rankItemParent.DestroyAllChilren();
            for (int i = 0; i < highscore.Count; i++)
            {
                RankItemUI rankItemUI = Instantiate<RankItemUI>(_rankItemPrefab, _rankItemParent);
                rankItemUI.Init(i + 1, highscore[i].name, highscore[i].score);
            }
        }
    }
}