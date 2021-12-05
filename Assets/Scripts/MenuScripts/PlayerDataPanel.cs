using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerDataPanel : MonoBehaviour
{
    private Text scoreText;
    private Text goldText;

    private static Transform addGoldparent;
    private static GameObject addGoldText;

    private void Start()
    {
        addGoldparent = transform.GetChild(2).transform;
        addGoldText = addGoldparent.GetChild(0).gameObject;

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        goldText = GameObject.Find("GoldText").GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = PlayerDataManager.Instance.playerData.score + "";
        goldText.text = PlayerDataManager.Instance.playerData.gold + "";
    }

    public static void AddGold(int addCount)
    {
        Text text = GameObject.Instantiate(addGoldText, addGoldparent).GetComponent<Text>();

        if (addGoldparent.childCount > 4)
        {
            int desCount = addGoldparent.childCount - 4;
            for (int i = 1; i < desCount + 1; i++)
            {
                addGoldparent.GetChild(i).DOKill();
                Destroy(addGoldparent.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < addGoldparent.childCount; i++)
        {
            addGoldparent.GetChild(i).GetComponent<RectTransform>().localPosition = new Vector3(150, (i-1) * -60, 0);
            if (addGoldparent.GetChild(i).GetComponent<RectTransform>().localPosition.y > -60)
            {
                addGoldparent.GetChild(i).GetComponent<RectTransform>().localPosition = new Vector3(150, -60, 0);
            }
        }

        text.DOFade(0.95f, 0.2f).onComplete = delegate () {
            text.DOFade(0, 4f).SetEase(Ease.Linear);
        };
        text.text = "Gold + " + addCount;
    }
}
