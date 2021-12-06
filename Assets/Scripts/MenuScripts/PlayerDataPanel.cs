using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerDataPanel : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI goldText;
    private static GameObject addGoldParent;
    private static GameObject addGoldText;

    private void Start()
    {
        addGoldParent = GameObject.Find("AddGold");
        addGoldText = GameObject.Find("AddGoldText").gameObject;
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        goldText = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        scoreText.text = "Score: " + PlayerDataManager.Instance.playerData.score;
        goldText.text = "Gold: " + PlayerDataManager.Instance.playerData.gold;
    }

    public static void AddGold(int addCount)
    {
        TextMeshProUGUI text = Instantiate(addGoldText, addGoldParent.transform).GetComponent<TextMeshProUGUI>();

        if (addGoldParent.transform.childCount > 4)
        {
            int desCount = addGoldParent.transform.childCount - 4;
            for (int i = 1; i < desCount + 1; i++)
            {
                addGoldParent.transform.GetChild(i).DOKill();
                Destroy(addGoldParent.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < addGoldParent.transform.childCount; i++)
        {
            addGoldParent.transform.GetChild(i).GetComponent<RectTransform>().localPosition = new Vector3(150, (i-1) * -60, 0);
            if (addGoldParent.transform.GetChild(i).GetComponent<RectTransform>().localPosition.y > -60)
            {
                addGoldParent.transform.GetChild(i).GetComponent<RectTransform>().localPosition = new Vector3(150, -60, 0);
            }
        }

        text.DOFade(0.95f, 0.2f).onComplete = delegate () {
            text.DOFade(0, 4f).SetEase(Ease.Linear);
        };
        text.text = "Gold + " + addCount;
    }
}
