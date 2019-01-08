using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class top : MonoBehaviour
{
    public Rigidbody2D topRigid;
    public Rigidbody2D baglanmaNoktasi;
    bool tiklandi = false;
    //yayimizi germe icin bir uzunkuk tanimlayalim
    public float maxUzunluk;
    //levelKontrol scriptindeki cana ulasmak icin kontrol yarattik
    public levelKontrol kontrol;
    public GameObject yeniTop;
    GameObject ball;

    //top birakildigi zaman mudahala edilmesin
    bool birakildimi;

    void Start()
    {
        
    }


    void Update()
    {
        //eger topa tiklandiysa takip etsin
        if (tiklandi)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(mousePos, baglanmaNoktasi.position) > maxUzunluk)
            {
                topRigid.position = baglanmaNoktasi.position + (mousePos - baglanmaNoktasi.position).normalized * maxUzunluk;
            }
            else
            {
                topRigid.position = mousePos;
            }
        }


    }

   

    //bu script hangi nesneye aitse o tiklandigi zaman
    void OnMouseDown()
    {
        //eger birakildiysa return yap buradaki islemleri yapma
        if (birakildimi)
        {
            return;
        }

        tiklandi = true;
        //Kinematic = etki eden unsurlari umursama demek
        topRigid.isKinematic = true;
        //Debug.Log("tiklandi");
    }

    //bu script hangi nesneye aitse o tiklanip birakildigi zaman
    void OnMouseUp()
    {
        //eger birakildiysa return yap buradaki islemleri yapma
        if (birakildimi)
        {
            return;
        }

        tiklandi = false;
        topRigid.isKinematic = false;
        //Debug.Log("Birakildi");
        // attiktan 5 sn sonra topta yok olsun
        Destroy(gameObject, 5);
        Ates();
        birakildimi = true;
        //IENumerator ozel fonskyonunu burada cagiriyorum
        StartCoroutine(topOlustur());
    }

    void Ates()
    {
        //objelerin aktifligini setactive ile kontrol ederken
        //componentleri enabled ile ediyoruz
        //componenti belli bir sure sonra yok etmek icin soyle kullaniyoruz
        Destroy(GetComponent<SpringJoint2D>(), 0.04f);
    }

    void OnDestroy()
    {
        //levelKontrol scriptindeki cana ulasiyorum
        kontrol.can--;

        if (kontrol.can > 0)
        {

            //kameranin yeni olsuturulan topu takip etmesi icin cinemachine ulasalim
            Cinemachine.CinemachineVirtualCamera virtcam = GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>();
            //ulastigimiz kamera hyeni topumuzun transformunu takip etsin
            virtcam.Follow = yeniTop.transform;
        }

    }


    IEnumerator topOlustur()
    {
        //belli bir sure beklenerek yapilan islemler icin bu metodu kullaniyoruz
        yield return new WaitForSeconds(3.0f);

        // can hakkim varsa tekrar top olustursun
        if (kontrol.can > 1)
        {
            ball = Instantiate(yeniTop, baglanmaNoktasi.position, new Quaternion());
            top topu = ball.GetComponent<top>();
            topu.baglanmaNoktasi = baglanmaNoktasi;
            topu.kontrol = kontrol;
            topu.yeniTop = yeniTop;
            ball.GetComponent<SpringJoint2D>().connectedBody = baglanmaNoktasi;
        }
    }
}
