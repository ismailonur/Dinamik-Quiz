using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonucManager : MonoBehaviour
{
    public Text dogruTxt, yanlisTxt, puanTxt;

    public GameObject birinciYildiz, ikinciYildiz, ucuncuYildiz;

    public void SonuclariYazdir(int dogruAdet, int yanlisAdet, int puan)
    {
        dogruTxt.text = dogruAdet.ToString();
        yanlisTxt.text = yanlisAdet.ToString();
        puanTxt.text = puan.ToString();

        birinciYildiz.SetActive(false);
        ikinciYildiz.SetActive(false);
        ucuncuYildiz.SetActive(false);

        if(dogruAdet == 3)
        {
            birinciYildiz.SetActive(true);
            ikinciYildiz.SetActive(true);
            ucuncuYildiz.SetActive(true);
        }
        else if (dogruAdet == 2)
        {
            birinciYildiz.SetActive(true);
            ikinciYildiz.SetActive(true);
            ucuncuYildiz.SetActive(false);
        }
        else if(dogruAdet == 1)
        {
            birinciYildiz.SetActive(true);
            ikinciYildiz.SetActive(false);
            ucuncuYildiz.SetActive(false);
        }
    }

}
