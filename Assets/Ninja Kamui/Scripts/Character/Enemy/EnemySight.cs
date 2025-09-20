using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
   public Enemy enemy;
   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag(("Player")))
      {
         
         enemy.SetTarget(col.GetComponent<Character>());
         Debug.Log(col.gameObject.name);
      }
   }

  private void OnTriggerExit2D(Collider2D col)
   {
      if (col.CompareTag("Player"))
      {
         enemy.SetTarget(null);
      }
   }
}
