using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCombat : MonoBehaviour
{
    [SerializeField] private TMP_Text hpText;

    public void OnInit(float damage)
    {
        hpText.text = damage.ToString("");
    }
    public void EndTextCombat()
    {
        gameObject.SetActive(false);
    }
}
