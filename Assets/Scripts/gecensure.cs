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
        dakika = (int)(kronometre / 60); // Dakikay� hesapla

        // Metin nesnesine s�reyi g�ncelle
        kronometretext.text = string.Format("{0:00}:{1:00}", dakika, saniye);

        if (kronometre >= 15 * 60)
        {
            // S�re 15 dakikaya ula�t���nda yap�lacak i�lemler
            Kazandin();
            return; // Oyunu durdurmak i�in return kullanabilirsiniz.
        }

       
    }

   

    void Kazandin()
    {
        // Kazand���nda yap�lacak i�lemler
        // �rne�in, kazand�n panelini a�abilir veya oyunu durdurabilirsiniz.
    }
}
