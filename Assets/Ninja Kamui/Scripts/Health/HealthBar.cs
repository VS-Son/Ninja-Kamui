using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   [SerializeField] private Image fill;
   private float hp;
   private float maxHp;

   void Update()
   {
      fill.fillAmount = Mathf.Lerp(fill.fillAmount, hp / maxHp, Time.deltaTime);
   }

   public void OnInit(float maxHp)
   {
      this.maxHp = maxHp;
      hp = maxHp;
      fill.fillAmount = 1;
   }

   public void SetNewHp(float hp)
   {
      this.hp = hp;
   }
}
