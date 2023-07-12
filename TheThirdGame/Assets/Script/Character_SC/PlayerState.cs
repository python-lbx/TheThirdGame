using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    [Header("當前階段")]
    public Statue current_Statue;
    public float fade = 0f;
    public enum Statue{Ready,Fight,Dead}
    [Header("角色腳本")]
    public PlayerAttackController playerAttackController; //攻擊
    public PlayerController playerController;
    public PlayerMovement playerMovement; //移動
    //public PlayerController playerController; //角色數值
    public PlatformCollider platformCollider; //穿層
    public Map map; //地圖
    public MoveBag moveBag; //背包
    [Header("角色渲染")]
    public Material material;
    [Header("UI")]
    public GameObject GameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        playerController = GetComponentInChildren<PlayerController>();
        playerAttackController.enabled = false;
        playerMovement.enabled = false;
        platformCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // material.SetFloat("_Fade",fade);//控制數值

        switch (current_Statue)
        {
            case Statue.Ready:
            fade += Time.deltaTime;

            if(fade >= 1f)
            {
                fade = 1;
                current_Statue = Statue.Fight;
            }
            
            break;
            case Statue.Fight:
            playerAttackController.enabled = true;
            playerMovement.enabled = true;
            platformCollider.enabled = true;

            if(playerController.CurrentHP <= 0)
            {
                current_Statue = Statue.Dead;
                AVmanager.instance.Play("GameOver");
                AVmanager.instance.Play("GameOverBGM");
                
                AVmanager.instance.Stop("Tutorial");
                AVmanager.instance.Stop("Level");
                AVmanager.instance.Stop("Boss1");
                AVmanager.instance.Stop("Boss2");
                AVmanager.instance.Stop("Boss3");
            }


            break;

            case Statue.Dead:


            GetComponent<Animator>().SetBool("Died",true);
            gameObject.layer = LayerMask.NameToLayer("Invincible");   


            playerAttackController.enabled = false;
            playerMovement.enabled = false;
            platformCollider.enabled = false;
            map.enabled = false;
            moveBag.enabled = false;

            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            
            // if(fade > 0f)
            // {
            //     fade -= Time.deltaTime;
            // }
            break;
        }
    }

    public void QTG()
    {
        Application.Quit();
    }
    
    public void Restart()
    {
        AVmanager.instance.Stop("GameOverBGM");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameOverUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowGameOverUI()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0;
    }
}
