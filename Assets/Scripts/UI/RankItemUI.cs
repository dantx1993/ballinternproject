using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace BallMaze
{
    public class RankItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _txtRank;
        [SerializeField] private Image _imgRank;
        [SerializeField] private TextMeshProUGUI _txtName;
        [SerializeField] private TextMeshProUGUI _txtPoint;

        [SerializeField] private List<Sprite> _rankSprites;

        public void Init(int rank, string name, int point)
        {
            _txtRank.gameObject.SetActive(rank > 3);
            _imgRank.gameObject.SetActive(rank <= 3);
            if(rank <= 3)
            {
                _imgRank.sprite = _rankSprites[rank - 1];
            }
            else
            {
                _txtRank.text = rank.ToString();
            }
            _txtName.text = name;
            _txtPoint.text = point.ToString();
        }
    }
}