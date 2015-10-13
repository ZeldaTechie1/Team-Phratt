using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public float Health = 100f;
    public float maxHealth = 100f;
    public float minHealth = 0f;
    public Image image;

    // Use this for initialization
    void Start () {
         
	}

    public void Damage(float value)
    {
        Health = Health - value;
    }
	
	// Update is called once per frame
	void Update () {
         image.fillAmount = Health/maxHealth;

          if (Health < minHealth)
          {
            Health = minHealth;
            Destroy(this.gameObject);
          }
          if(Health > maxHealth)
          {
            Health = maxHealth;

        }

    }
}
