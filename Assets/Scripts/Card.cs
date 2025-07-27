using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private Image image;
    public Sprite cardFace;
    private static Sprite cardBack;
    public AudioClip flipCardSound;
    private AudioSource audioSource;

    private bool flipped = false;
    private GameManager gameManager;

    private void Start()
    {
        image = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();

        if (cardBack == null)
        {
            cardBack = Resources.Load<Sprite>("CardSprites/CardBack");
        }

        image.sprite = cardBack;

        gameManager = FindObjectOfType<GameManager>();
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(Flip);
        }
    }

    public void Flip()
    {
        if (flipped || gameManager == null || !gameManager.CanFlip(this))
            return;

        flipped = true;
        image.sprite = cardFace;
    }

    public void FlipBack()
    {
        flipped = false;
        image.sprite = cardBack;
    }

    public bool IsFlipped()
    {
        return flipped;
    }
}
