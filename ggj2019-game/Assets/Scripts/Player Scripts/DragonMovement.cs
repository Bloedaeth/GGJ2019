using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class DragonMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float flyForce;

    private bool canFly = true;

    public float flightPower = 100;
    public float increaseRate = 0.5f;
    public float decreaseRate = 1.0f;

    private Rigidbody rb;

    private KeyCode flyKey;

    public AudioClip woosh;
    private AudioSource source;

    public Animation runAnim;
    private Animator anim;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        flyKey = GetComponent<DragonControls>().flyControl;       
    }

    private void LateUpdate()
    {
        GetKeyPress();
    }

    private void GetKeyPress() {
        
        float h = Input.GetAxis("Horizontal");

        // fly only when w pressed
        if (Input.GetKeyDown(flyKey) && canFly)
            Fly();

        //anim.SetFloat("Speed", h < 0 ? -1 : 1);
        anim.SetFloat("H", Mathf.Abs(h));

        transform.position += new Vector3(h * moveSpeed * Time.deltaTime, 0, 0);
    }

    private void Fly() {
        rb.AddForce(Vector3.up * flyForce, ForceMode.Acceleration);

        source.PlayOneShot(woosh, 0.3f);
        
        // TODO
        // add limit to fly ability
    }
}
