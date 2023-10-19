using UnityEngine;

public class HeroDoll : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 initialPosition;
    public Card card;
    public Sprite charactor;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Init()
    {
        gameObject.SetActive(true);
        rb.velocity = HeroParticle.RandomForce();
        rb.angularVelocity = Random.Range(-100, 100);
        gameObject.GetComponent<SpriteRenderer>().sprite = charactor;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            rb.isKinematic = true;
        }
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            rb.isKinematic = false;
            ThrowObject();
        }
    }
    private float lerpSpeed = 10f; // T?c ?? lerp
    private void OnMouseDrag()
    {
        if (isDragging)
        {
            rb.velocity *= 0.9f;
            rb.angularVelocity *= 0.9f;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            initialPosition = objectPosition;
            // Áp d?ng lerp cho di chuy?n m??t mà
            transform.position = Vector3.Lerp(transform.position, objectPosition, lerpSpeed * Time.deltaTime);
        }
    }

    private void ThrowObject()
    {
        Vector3 throwDirection = transform.position - initialPosition;

        float throwForce = throwDirection.magnitude * 10f;
        //Debug.Log(throwForce);
        //if (throwForce < 10) return;

        rb.AddForce(throwDirection.normalized * -throwForce, ForceMode2D.Impulse);
    }
}