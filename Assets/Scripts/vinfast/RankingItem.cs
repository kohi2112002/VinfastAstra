using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RankingItem : MonoBehaviour
{

    public Text index;
    public Text playerName;
    public Text score;
    public void SetRankingItem(string _index, string _playerName, string _score)
    {
        index.text = _index + ".";
        playerName.text = _playerName;
        score.text = _score;
    }
}
