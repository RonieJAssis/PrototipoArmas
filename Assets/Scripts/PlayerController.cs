using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{


    private Animator anim;
    private Rigidbody2D rb2d;

    private bool arma1 = false;
    private bool arma2 = false;
    private bool armado1 = false;
    private bool armado2 = false;
    private bool desarmado = true;
    private float ShootingRate = 0.1f;
    private float ShootCooldown = 0f;
    public Transform SpawnBullet;
    public GameObject Bullet1;
    public GameObject Bullet2;

    public Transform posPe;
    [HideInInspector]
    public bool tocaChao = false;


    public float Velocidade;
    public float ForcaPulo = 1000f;
    [HideInInspector]
    public bool viradoDireita = true;
    private bool pula = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();




    }

    void Update()
    {
        //Implementar Pulo Aqui!
        tocaChao = Physics2D.Linecast(transform.position, posPe.position, 1 << LayerMask.NameToLayer("Chao"));
        if (Input.GetKeyDown("space") && tocaChao)
        {
            //pula
            pula = true;



        }
    }


    void FixedUpdate()
    {
        if (ShootCooldown > 0)
            ShootCooldown -= Time.deltaTime;

        float translationY = 0;
        float translationX = Input.GetAxis("Horizontal") * Velocidade;
        transform.Translate(translationX, translationY, 0);
        transform.Rotate(0, 0, 0);
        if (translationX != 0 && tocaChao && desarmado == true && armado1 == false && armado2 == false)
        {
            anim.SetTrigger("corre");
        }
        if (translationX == 0 && tocaChao && desarmado == true && armado1 == false && armado2 == false)
        {
            anim.SetTrigger("parado");
        }
        if (translationX != 0 && tocaChao && desarmado == false && armado1 == true && armado2 == false)
        {
            anim.SetTrigger("Selecionando arma 1");
        }
        if (translationX == 0 && tocaChao && desarmado == false && armado1 == true && armado2 == false)
        {
            anim.SetTrigger("parado");
        }
        if (translationX != 0 && tocaChao && desarmado == false && armado1 == false && armado2 == true)
        {
            anim.SetTrigger("Selecionando arma 2");
        }
        if (translationX == 0 && tocaChao && desarmado == false && armado1 == false && armado2 == true)
        {
            anim.SetTrigger("parado");
        }

        //Programar o pulo Aqui! 
        if (pula == true)
        {
            anim.SetTrigger("pula");
            rb2d.AddForce(new Vector2(0f, ForcaPulo));
            pula = false;
        }

        if (translationX > 0 && !viradoDireita)
        {

            Flip();
        }
        else if (translationX < 0 && viradoDireita)
        {

            Flip();
        }



        if (Input.GetMouseButtonDown(0) && desarmado == false && armado1 == true && armado2 == false)
        {
            anim.SetTrigger("Atirando 1");
            Fire1();
            ShootCooldown = ShootingRate;
        }
        if (Input.GetMouseButtonDown(0) && desarmado == false && armado1 == false && armado2 == true)
        {
            anim.SetTrigger("Atirando 2");
            Fire2();
            ShootCooldown = ShootingRate;
        }



        if (Input.GetKeyDown(KeyCode.R))
        {
            desarmado = true;
            armado1 = false;
            armado2 = false;
        }
        if (Input.GetKeyDown(KeyCode.T) && arma1 == true)
        {
            desarmado = false;
            armado1 = true;
            armado2 = false;
        }
        if (Input.GetKeyDown(KeyCode.Y) && arma2 == true)
        {
            desarmado = false;
            armado1 = false;
            armado2 = true;
        }



    }
    void Flip()
    {
        viradoDireita = !viradoDireita;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Arma2")
        {

            other.gameObject.SetActive(false);
            arma2 = true;


        }
        if (other.tag == "Arma1")
        {

            other.gameObject.SetActive(false);
            arma1 = true;

        }

    }



    void Armado2()
    {
        desarmado = false;
        armado1 = false;
        armado2 = true;
    }
    void Fire1()
    {
        if (ShootCooldown <= 0f)
        {
            if (Bullet1 != null)
            {
                var cloneBullet = Instantiate(Bullet1, SpawnBullet.position, Quaternion.identity) as GameObject;
                cloneBullet.transform.localScale = this.transform.localScale;
            }
        }
    }
    void Fire2()
    {
        if (ShootCooldown <= 0f)
        {
            if (Bullet2 != null)
            {
                var cloneBullet = Instantiate(Bullet2, SpawnBullet.position, Quaternion.identity) as GameObject;
                cloneBullet.transform.localScale = this.transform.localScale;
            }
        }
    }
}




