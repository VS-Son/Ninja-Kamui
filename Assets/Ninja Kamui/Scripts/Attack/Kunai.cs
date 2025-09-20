using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
   private Rigidbody2D _rb2D;
   [SerializeField] private GameObject hitVfx;
   [SerializeField] private float speed;

   private void Start()
   {
      _rb2D = GetComponent<Rigidbody2D>();
      _rb2D.velocity = transform.right * speed;
      Invoke(nameof(OnDespawn), 2f);
      
   }

   public void OnDespawn()
   {
      Destroy(gameObject);
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag("Enemy"))
      {
         col.GetComponent<Enemy>().OnHit(30f);
         Instantiate(hitVfx, transform.position, transform.rotation);
         OnDespawn();
      }
   }
}
