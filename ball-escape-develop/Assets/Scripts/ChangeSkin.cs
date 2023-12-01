using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSkin : MonoBehaviour
{
    [SerializeField] List<Sprite> ListSkinSprite = new List<Sprite>();
    public SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ListSkinSprite[PlayerPrefs.GetInt("Skin Use")];
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
    
}
