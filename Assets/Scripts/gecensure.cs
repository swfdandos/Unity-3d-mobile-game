using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class gecensure : MonoBehaviour
{
    public float kronometre = 0f;
    public TextMeshProUGUI kronometretext;
    int dakika = 0;
    int saniye = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        kronometre -= Time.deltaTime;
        saniye = (int)(kronometre % 60); // Saniyeyi hesapla
        dakika = (int)(kronometre / 60); // Dakikayý hesapla

        // Metin nesnesine süreyi güncelle
        kronometretext.text = string.Format("{0:00}:{1:00}", dakika, saniye);

        if (kronometre >= 15 * 60)
        {
            // Süre 15 dakikaya ulaþtýðýnda yapýlacak iþlemler
            Kazandin();
            return; // Oyunu durdurmak için return kullanabilirsiniz.
        }

       
    }

   

    void Kazandin()
    {
        // Kazandýðýnda yapýlacak iþlemler
        // Örneðin, kazandýn panelini açabilir veya oyunu durdurabilirsiniz.
    }
}
