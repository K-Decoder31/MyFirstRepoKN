using UnityEngine;

public class ItemControl : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        else 
        {
            Debug.LogWarning("Puutu spriterenderer!");
            //luo automaatisesti jokin Componenti
            //gameObject.AddComponent<SpriteRenderer>();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) 
        {
            if (collision.CompareTag("Player")) 
            {
                Debug.Log("Item found!");
                spriteRenderer.color = Color.black;
                //collision.gameObject.SetActive(false);

            }
        }
    }

}
