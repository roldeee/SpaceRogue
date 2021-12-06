using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour
{
    [SerializeField]
    private int score;

    [SerializeField]
    private int minGold, maxGold;

    private void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.collider.GetComponent<Projectile>();
        if (projectile != null)
        {
            PlayerDataManager.Instance.playerData.score += score;
            int gold = Random.Range(minGold, maxGold + 1);
            if (gold != 0)
            {
                PlayerDataManager.Instance.playerData.gold += gold;
                PlayerDataPanel.AddGold(gold);
            }
            Destroy(gameObject);
        }
    }
}
