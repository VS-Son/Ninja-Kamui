using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
  public static UiManager instance;
  [SerializeField] private TMP_Text textCoin;

  // public static UiManager Instance
  // {
  //   get
  //   {
  //     if (instance == null)
  //     {
  //       instance = FindObjectOfType<UiManager>();
  //     }
  //
  //     return instance;
  //   }
  // }
  private void Awake()
  {
    instance = this;
  }

  public void SetCoin(int coin)
  {
    textCoin.text = coin.ToString("");
  }
}
