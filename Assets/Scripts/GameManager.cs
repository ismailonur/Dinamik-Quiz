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

    void Start()
    {
        if(cevaplanmamisSorular == null || cevaplanmamisSorular.Count == 0)
        {
            cevaplanmamisSorular = sorular.ToList<Soru>();
        }

        RastgeleSoruSec();
    }

    void RastgeleSoruSec()
    {
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DogruButonaBasildi()
    {
        if (gecerliSoru.dogrumu)
        {
            Debug.Log("Doğru");
        }
        else
        {
            Debug.Log("Yanlış");
        }

        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(1000f, .5f);
        StartCoroutine(SorularArasiBekle());
    }
    
    public void YanlisButonaBasıldı()
    {
        if (!gecerliSoru.dogrumu)
        {
            Debug.Log("Doğru");
        }
        else
        {
            Debug.Log("Yanlış");
        }

        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-1000f, .5f);
        StartCoroutine(SorularArasiBekle());
    }
}
