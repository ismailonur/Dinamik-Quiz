using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public Soru[] sorular;
    private static List<Soru> cevaplanmamisSorular;

    private Soru gecerliSoru;

    public Text soruText;

    public Text dogruCevapText, yanlisCevapText;

    public GameObject dogruButon, yanlisButon;

    int dogruAdet, yanlisAdet;

    int toplamPuan;

    public GameObject sonucPaneli;

    SonucManager sonucManager;

    void Start()
    {
        if(cevaplanmamisSorular == null || cevaplanmamisSorular.Count == 0)
        {
            cevaplanmamisSorular = sorular.ToList<Soru>();
        }

        dogruAdet = 0;
        yanlisAdet = 0;
        toplamPuan = 0;

        RastgeleSoruSec();
    }

    void RastgeleSoruSec()
    {
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(320f, .2f);
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-320f, .2f);

        int randomSoruIndex = Random.Range(0, cevaplanmamisSorular.Count);
        gecerliSoru = cevaplanmamisSorular[randomSoruIndex];

        soruText.text = gecerliSoru.soru;

        if (gecerliSoru.dogrumu)
        {
            dogruCevapText.text = "DOĞRU CEVAPLADINIZ.";
            yanlisCevapText.text = "YANLIŞ CEVAPLADINIZ!";
        }
        else
        {
            dogruCevapText.text = "YANLIŞ CEVAPLADINIZ.";
            yanlisCevapText.text = "DOĞRU CEVAPLADINIZ!";
        }
    }

    IEnumerator SorularArasiBekle()
    {
        cevaplanmamisSorular.Remove(gecerliSoru);

        yield return new WaitForSeconds(1f);

        if(cevaplanmamisSorular.Count <= 0)
        {
            sonucPaneli.SetActive(true);

            sonucManager = Object.FindObjectOfType<SonucManager>();
            sonucManager.SonuclariYazdir(dogruAdet, yanlisAdet, toplamPuan);
        }
        else
        {
            RastgeleSoruSec();
        }
    }

    public void DogruButonaBasildi()
    {
        if (gecerliSoru.dogrumu)
        {
            dogruAdet++;
            toplamPuan += 100;
        }
        else
        {
            yanlisAdet++;
        }

        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(1000f, .5f);
        StartCoroutine(SorularArasiBekle());
    }
    
    public void YanlisButonaBasıldı()
    {
        if (!gecerliSoru.dogrumu)
        {
            dogruAdet++;
            toplamPuan += 100;
        }
        else
        {
            yanlisAdet++;
        }

        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-1000f, .5f);
        StartCoroutine(SorularArasiBekle());
    }
}
