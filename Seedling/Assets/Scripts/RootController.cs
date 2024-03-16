using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float spawnDistance;
    private float horizontalInput;
    private Vector3 startPos;

    public GameObject staticRoot;

    private SpriteRenderer spriteRenderer;
    private Color startColor;
    private Color endColor = new Color(0.8867924f, 0.6829652f, 0.5396048f);
    private Color currentColor;
    private float transitionDuration = 5f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        StartCoroutine(DryingUp());
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameOver)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.back * horizontalInput * rotationSpeed * Time.deltaTime);

            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }


        if (Vector3.Distance(startPos, transform.position) >= spawnDistance)
        {
            startPos = transform.position;
            GameObject rootObject = Instantiate(staticRoot, transform.position, transform.rotation);
            rootObject.GetComponent<SpriteRenderer>().color = currentColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("rock"))
        {
            Debug.Log("player hit a rock. Game Over!");
        } else if (collision.gameObject.CompareTag("water"))
        {
            timer = 0f;
            spriteRenderer.color = startColor;
        }
    }


    /// <summary>
    /// Changed the color of the root gradually until it dies.
    /// </summary>
    /// <returns>a routine</returns>
    IEnumerator DryingUp()
    {
        while(!GameManager.gameOver)
        {
            timer += Time.deltaTime;
            if(timer <= transitionDuration)
            {
                currentColor = Color.Lerp(startColor, endColor, timer / transitionDuration);
                spriteRenderer.color = currentColor;
            }
            else
            {
                Debug.Log("timer is larger than transitionDuration");
                GameManager.gameOver = true;
            }
            yield return null;
        }
        spriteRenderer.color = endColor;
    }
}