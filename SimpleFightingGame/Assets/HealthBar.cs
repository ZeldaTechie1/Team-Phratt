using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public float Health = 100f;
    public float maxHealth = 100f;
    public float minHealth = 0f;
    Image image;

    // Use this for initialization
    void Start () {
	Image image = GetComponent<Image>();
         
	}

    void Damage(float value)
    {
        Health = Health - value;
    }
	
	// Update is called once per frame
	void Update () {
         image.fillAmount = Health;

          if (Health < minHealth)
            { Health = minHealth; }
          if(Health > maxHealth)
            { Health = maxHealth; }

    }
}
