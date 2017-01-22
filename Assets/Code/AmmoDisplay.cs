using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{

    public Submarine Sub;

    public float padding;

    public Image AmmoImage;

    public Color NoAmmoColor;

    private List<Image> _ammoImages = new List<Image>();
    
    // Use this for initialization
	void Start () {
        for (int i = 0; i < Sub.MaxAmmo; i++)
        {
            var im = Instantiate(AmmoImage, transform);
            im.transform.localPosition = new Vector2(0, -i * padding);
            im.transform.localScale = Vector3.one;
            _ammoImages.Add(im);
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < Sub.MaxAmmo; i++)
        {
            _ammoImages[i].color = i < Sub.AmmoCount ? Color.white : NoAmmoColor;
        }
    }
}
