using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopPanel : MonoBehaviour
{
    private Text curGoldText;

    private void Update()
    {
        if (curGoldText == null)
        {
            curGoldText = GameObject.Find("CurGoldText").GetComponent<Text>();
        }

        curGoldText.text = PlayerDataManager.Instance.playerData.gold + "";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                OnClose();
            }
        }
        else if (Input.GetKey(KeyCode.T) && Input.GetKeyDown(KeyCode.S))
        {
            OnShow();
        }
    }

    public void OnShow()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        transform.localPosition = Vector3.zero;
    }

    public void OnClose()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        transform.DOLocalMove(new Vector3(-2000, 0, 0), 1f).SetEase(Ease.Linear);
    }

    public void OnButtonClickButtonDown(GameObject obj)
    {
        int gold = int.Parse(obj.transform.GetChild(2).GetComponent<Text>().text);
        if (PlayerDataManager.Instance.playerData.gold < gold)
        {
            return;
        }
        else
        {
            PlayerDataManager.Instance.playerData.gold -= gold;
        }

        switch (obj.gameObject.name)
        {
            case "HPButton":
                FindObjectOfType<PlayerHealth>().Heal(1);
                break;
            case "AUTO_RESButton":
                PlayerDataManager.Instance.playerData.hasAutoRes = true;
                break;
            case "DOUBLE_FIREButton":
                FindObjectOfType<PlayerWeapon>().SetFireType(PlayerWeapon.FIRE_TYPE.DOUBLE_FIRE);
                break;
            case "SPREAD_FIREButton":
                FindObjectOfType<PlayerWeapon>().SetFireType(PlayerWeapon.FIRE_TYPE.SPREAD_FIRE);
                break;
        }
    }
}
