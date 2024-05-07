using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine . Rendering . UI;
using UnityEditor;
using static UnityEngine . Rendering . DebugUI;

public class CoinPileVisualizer : MonoBehaviour
{

    public Sprite [ ] coinPiles;
    public int coinIndex =0;
    private SpriteRenderer pileObj;
    public TextMeshProUGUI textCoin;
    public GameObject spawn;
    private int coinAmt;
    int millCoint;
    public TextMeshProUGUI totalCash;
    // Start is called before the first frame update
    void Start()
    {
        pileObj = gameObject . GetComponent<SpriteRenderer> ();
        int millions = Mathf.FloorToInt(coinAmt / 1000000f);
        millCoint += millions;
        for (int i = 0; i < millCoint ; i++)
        {

            Instantiate (spawn , new Vector2 (transform . position . x + Random . Range (-1.8f , 2f) , transform . position . y + Random . Range (0f , 4f)) , Quaternion . identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        textCoin . text = GameManager . Instance . GoldPileNum . ToString ();   
        totalCash.text = GameManager.Instance.Coins.ToString () + "$";
        if(coinAmt != GameManager.Instance.GoldPileNum)
        {
            coinAmt = GameManager . Instance . GoldPileNum;
            CurrentCoinPile ();
        }

        pileObj . sprite = coinPiles [ coinIndex ];
    }


    void CurrentCoinPile( )
    {
        int i = coinAmt;
   
        if( i > 10)
        {
            coinIndex = 1;
            if(i > 100)
            {
                coinIndex = 2;
                if ( i > 500 )
                {
                    coinIndex = 3;
                    if ( i > 1000 )
                    {
                        coinIndex = 4;
                        if ( i > 5000 )
                        {
                            coinIndex = 5;
                            if(i > 10000)
                            {
                                    coinIndex = 6;
                                if(i > 50000)
                                {

                                        coinIndex = 7;
                                    if(i > 100000)
                                    {
                                        coinIndex = 8;
                                        if(i > 500000)
                                        {
                                            coinIndex = 9;
                                            if(i > 1000000)
                                            {
                                      
                                                Instantiate (spawn, new Vector2 (transform . position . x + Random . Range (-1.8f , 2f) , transform . position . y + Random . Range (0f , 4f)) , Quaternion . identity);
                                                
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }

                }

            }
        }

    }
}
