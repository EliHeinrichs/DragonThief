using System . Collections;
using System . Collections . Generic;
using UnityEngine;
using UnityEngine . SceneManagement;

public class DungeonExit : MonoBehaviour
{
    public float timer = 10f;
    public Collider2D hitBox;
    public SpriteRenderer exitGFX;
    public Sprite returnSprite;
    public string sceneName;



    private void OnTriggerEnter2D ( Collider2D collision )
    {
        if ( collision . gameObject . tag == "Player" )
        {

            GameManager . Instance . level += 1;
            SceneManager . LoadScene (sceneName);// Reload this scene

        }
    }

    private void Start ( )
    {
        hitBox . enabled = false;
        exitGFX . enabled = false;
    }

    // Update is called once per frame
    void Update ( )
    {
        timer -= Time . deltaTime;
        if ( timer < 0)
        {

            
            if ( GameManager . Instance . level % 2 == 0 )
            {
                sceneName = "Shop";
                hitBox . enabled = true;
                exitGFX . enabled = true;
                exitGFX . sprite = returnSprite;
              

            }
            else
            {

                if(GameManager.Instance.level > 50)
                {
                    GameManager.Instance.level = 25;
                }
                sceneName = "Eli's Scene";
                hitBox . enabled = true;
                exitGFX . enabled = true;

              
            }



        }
    }
}
