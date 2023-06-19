
using UnityEngine;
using UnityEngine.UI;


public class ZombiCanBar : MonoBehaviour
{

    [SerializeField]  Image HealtBar;
    [SerializeField] float decreaseSpeed;
    float healtBarInstance = 1f;

    void Start()
    {
        
    }


    void Update()
    {
        HealtBar.fillAmount = Mathf.MoveTowards(HealtBar.fillAmount, healtBarInstance, decreaseSpeed * Time.deltaTime);
    }
    public void HealthBarProgress(float currentHealth, float maxHealth)
    {
        healtBarInstance = currentHealth / maxHealth;
        transform.gameObject.SetActive(true); // zombi hasar aldýðýnda objeyi görünür yap.
    }
}
