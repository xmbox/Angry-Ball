using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelKontrol : MonoBehaviour
{
    public static int dusmanSayisi = 0;
    public int can;
    public static int toplamPuan;
    public GameObject panelAc, tekrarAc;
    public GameObject yldz1;
    public GameObject yldz2;
    public GameObject yldz3;
    //yildiz icin kac puan almamiz gereksin
    public int puan2, puan3;
    //panele puan yazsin
    public Text puanText;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelBittimi();
    }

    //dusman sayisini kontrol edelim
    void levelBittimi()
    {
        Debug.Log(dusmanSayisi);

        if (dusmanSayisi <= 0)
        {
            Debug.Log("Kazandin");
            //kazaninca panelimiz acilsin
            panelAc.SetActive(true);

            // yildizlari ac fonksiyonunu burada cagiriyorum
            StartCoroutine(yildizAc());
        }
        else if (can <= 0)
        {
            tekrarAc.SetActive(true);
        }
    }

    public void YeniLevel()
    {
        //buildindexteki siraya gore bir sonraki levele gec diyorumm
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TekrarOyna()
    {
        //buildindexteki siraya gore kaldigi sahnede devam etsin
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    IEnumerator yildizAc()
    {
        //yildizlari acmadan once puanlari yazdiralim
        puanText.text = toplamPuan.ToString();

        yield return new WaitForSeconds(1f);

        yldz1.SetActive(true);

        if (toplamPuan > puan2)
        {
            yield return new WaitForSeconds(1f);
            yldz2.SetActive(true);
        }

        if (toplamPuan > puan3)
        {
            yield return new WaitForSeconds(1f);
            yldz3.SetActive(true);
        }
    }
}
