using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


	private Animator anim;
	private Rigidbody2D rb2d;
    private int inventario;
    private int arma1;
    private int arma2;

    public Transform posPe;
	[HideInInspector] public bool tocaChao = false;


	public float Velocidade;
	public float ForcaPulo = 1000f;
	[HideInInspector] public bool viradoDireita = true;
	private bool pula = false;
    public Image vida;
	private MensagemControle MC;

	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();

		GameObject mensagemControleObject = GameObject.FindWithTag ("MensagemControle");
		if (mensagemControleObject != null) {
			MC = mensagemControleObject.GetComponent<MensagemControle> ();
		}
	}
	
	
	void Update () {
		//Implementar Pulo Aqui!
		tocaChao = Physics2D.Linecast(transform.position, posPe.position, 1 << LayerMask.NameToLayer("Chao"));
		if(Input.GetKeyDown("space") && tocaChao){
			//pula
			pula = true;
          

		}
	}


	void FixedUpdate()
	{
		float translationY = 0;
		float translationX = Input.GetAxis ("Horizontal") * Velocidade;
		transform.Translate (translationX, translationY, 0);
		transform.Rotate (0, 0, 0);
		if (translationX != 0 && tocaChao) {
			anim.SetTrigger ("corre");
		} else {
			anim.SetTrigger("parado");
		}

		//Programar o pulo Aqui! 
		if(pula == true){
			anim.SetTrigger("pula");
			rb2d.AddForce(new Vector2 (0f,ForcaPulo));
			pula = false;
		}

		if (translationX > 0 && !viradoDireita) {
			Flip ();
		} else if (translationX < 0 && viradoDireita) {
			Flip();
		}
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Atirando 1");
        }


    }
    void Flip()
	{
		viradoDireita = !viradoDireita;
		Vector3 escala = transform.localScale;
		escala.x *= -1;
		transform.localScale = escala;
	}

	public void SubtraiVida()
	{
		vida.fillAmount-=0.1f;
		if (vida.fillAmount <= 0) {
			MC.GameOver();
			Destroy(gameObject);
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Arma2")
        {

            other.gameObject.SetActive(false);
            inventario = arma2;

        }
        if (other.tag == "Arma1")
        {

            other.gameObject.SetActive(false);
            inventario = arma1;
        }
    }
}


