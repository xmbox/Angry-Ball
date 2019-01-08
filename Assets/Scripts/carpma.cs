using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carpma : MonoBehaviour
{
    public GameObject Puan;
    public int puan;

    void Start()
    {
        if (gameObject.tag == "dusman")
        {
            levelKontrol.dusmanSayisi++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // trgiggeri acik olmayan carpismalari kontrol etmek icin
    private void OnCollisionEnter2D(Collision2D col)
    {
        //relativeVelocity=bana gelme, bana carpma hizi
        //.magnitude = vektorun uzunlugunu verir
        if (col.relativeVelocity.magnitude > 5f)
        {
            Olme();
        } 
        else if (col.relativeVelocity.magnitude > 3f && gameObject.tag == "dusman")
        {
            Olme();
        }
    }

    void Olme()
    {
        GameObject olText = Instantiate(Puan, transform.position, new Quaternion());
        olText.GetComponent<TextMesh>().text = puan.ToString();

        Destroy(gameObject, 0.2f);
    }

    // ben yok oldugum zaman su islemi yap demek olan fonksyon
    // obje yok edilmeden hemen once calisir
    private void OnDestroy()
    {
        // tum puanlari toplayip toplam puan aliyoruz
        levelKontrol.toplamPuan += puan;

        // dusman sayisini azaltma ve yeni level
        if (gameObject.tag == "dusman")
        {
            levelKontrol.dusmanSayisi--;
        }

    }
}
