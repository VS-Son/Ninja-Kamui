using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo2 : MonoBehaviour
{
   private Vector3 offset;
   public Camera cam;
   

  private void OnMouseDown()
   {
      Vector3 mousePos = Input.mousePosition;
      mousePos.z = cam.WorldToScreenPoint(transform.position).z;
      offset = transform.position - cam.ScreenToWorldPoint(mousePos);
   }

   private void OnMouseDrag()
   {
      Vector3 mousePos = Input.mousePosition;
      mousePos.z = cam.WorldToScreenPoint(transform.position).z; 
      Vector3 targetPos = cam.ScreenToWorldPoint(mousePos) + offset;

      transform.position = targetPos;
   }
}
