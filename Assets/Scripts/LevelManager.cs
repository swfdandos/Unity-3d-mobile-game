using UnityEngine;
using UnityEngine.SceneManagement;
    
public class LevelManager : MonoBehaviour
{
    /* private int level = 1;
     public GameObject winnerPanel;
     public GameObject karakterCanGostergesi;
     public GameObject pauseButon;
     public GameObject karakterHareketCanvas;



     private void Start()
     {
         winnerPanel.SetActive(false);

     }

     public void DusmanOlduruldu()
     {
         LevelTamamlandiKontrolu(ZombiSaldýrý.destroyedEnemyCount);
     }

     private void LevelTamamlandiKontrolu(int dusmanSayisi)
     {
         if (dusmanSayisi >= 10)
         {
             winnerPanel.SetActive(true);
             karakterCanGostergesi.SetActive(false);
             karakterHareketCanvas.SetActive(false);
             pauseButon.SetActive(false);
             Debug.Log("Winner");
             // "Winner" yazýsýný göster
             // Main menü ve next level butonlarýný aktif hale getir
         }
     }

     public void MainMenu()
     {
         SceneManager.LoadScene(0); // Panel sýralamasýndaki 0. yi getirir yani main menü panelini.
     }

     public void NextLevel()
     {
         level++; // Level'i bir artýrýn

         string levelName = "Level" + level.ToString(); // Bir sonraki level'in sahne adýný oluþturun

         SceneManager.LoadScene(levelName); // Bir sonraki level'in sahnesini yükleyin
     }*/

    public int targetKillCount = 20;
    public GameObject winnerPanel;
    public GameObject karakterCanGostergesi;
    public GameObject pauseButon;
    public GameObject karakterHareketCanvas;

    private void Start()
    {
        winnerPanel.SetActive(false);

    }
    private void Update()
    {
        if (ZombiSaldýrý.destroyedEnemyCount >= targetKillCount)
        {
            ShowWinnerPanel();
            Time.timeScale = 0f;
            Debug.Log("Winner");
        }
        else
        {
            Time.timeScale = 1f;
            
        }
            
    }

    private void ShowWinnerPanel()
    {
        winnerPanel.SetActive(true);
        karakterCanGostergesi.SetActive(false);
        karakterHareketCanvas.SetActive(false);
        pauseButon.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0); // Ana menü sahnesinin yüklenmesi için sahne indeksi 0 kullanýlýyor
    }

    public void GoToNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextSceneIndex);
            ZombiSaldýrý.destroyedEnemyCount = 0;

        }
        else
        {
            Debug.Log("There is no next level!"); // Son seviyedeyiz, bir sonraki seviye yok
        }
    }
}
