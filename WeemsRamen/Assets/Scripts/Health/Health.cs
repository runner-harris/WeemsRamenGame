using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
  [Header ("Health")]  
  [SerializeField] private float startingHealth;
  public float currentHealth {get; private set;}
  private Animator anim;
  private bool dead;
  public GameObject player;
  public GameObject enemy;

    [Header ("iFrames")]
  [SerializeField] private float iFramesDuration;
  [SerializeField] private int numberOfFlashes;
  private SpriteRenderer spriteRend;

   

  

  private void Awake()
  {
      currentHealth = startingHealth;
      anim = GetComponent<Animator>();
      spriteRend = GetComponent<SpriteRenderer>();
  }
  public void TakeEnemyDamage(float _damage)
  {
      currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

      if(currentHealth > 0)
      {
          //player hurt
            //anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
      }
      else
      {
          //player dead
          if(!dead)
          {
                //Enemy
             if (GetComponent<Enemy>() != null){
                GetComponent<Enemy>().enabled = false;
                enemy.SetActive(false);
              }
                

            dead = true;
          }
      }
  }

    public void TakePlayerDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //player hurt
            //anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            //player dead
            if (!dead)
            {
                //anim.SetTrigger("die");
                //Player
                //if (GetComponent<PlayerMovement>() != null)
                //    GetComponent<PlayerMovement>().enabled = false;
                player.SetActive(false);
                SceneManager.LoadScene(3);


                dead = true;
            }
        }
    }
    public void AddHealth(float _value)
  {
      currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
  }
  private IEnumerator Invulnerability()
  {
      Physics2D.IgnoreLayerCollision(10, 11, true);
      for(int i = 0; i < numberOfFlashes; i++)
      {
          spriteRend.color = new Color(1, 0, 0, 0.5f);
          yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
          spriteRend.color = Color.white;
          yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
      }
      Physics2D.IgnoreLayerCollision(10, 11, false);
  }
 
}
