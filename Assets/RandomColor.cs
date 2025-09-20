using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
       public Renderer rend;
       
       private  void OnMouseDown()
       {
           rend.material.color = new Color(Random.value, Random.value, Random.value);
       }
       
}
