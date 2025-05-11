using UnityEngine;
using System.Collections.Generic;

public class VignetteSculptor : VignetteBase
{
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
            UpdateBlockSprite();

            if (currentHealth <= 0)
            {
                ResetSculpture();
            }
        }
    }

    public int currentHealth;
    public int maxHealth = 2;
    public GameObject statueContainer;
    private Sculpture statueScript;
    private GameObject currentStatue;
    public Transform statueSpawner;
    private Animator spawnerAnim;

    public float maxCursorDistance = 10f;
    public float shakeAmount = 0.01f;
    public float shakeSpeed = 10f;

    private Transform activeBlock;
    private SpriteRenderer activeBlockSprite;
    public Animator statueAnimator;

    public List<SculptorInteractable> sculptorBlocks = new List<SculptorInteractable>();
    public int currentIndex;

    public override void Start()
    {
        base.Start();
    }

    public override void StartMinigame()
    {
        if (spawnerAnim == null)
            spawnerAnim = GetComponent<Animator>();

        spawnerAnim.SetTrigger("SpawnStatue");
        this.gameObject.SetActive(true);
        currentIndex = 0;
        currentStatue = Instantiate(statueContainer, statueSpawner);
        statueAnimator = currentStatue.transform.GetChild(1).GetComponent<Animator>();
        statueScript = currentStatue.GetComponent<Sculpture>();
        sculptorBlocks = statueScript.sculptureOrder;
        activeBlock = sculptorBlocks[0].transform;
        activeBlockSprite = activeBlock.GetComponent<SpriteRenderer>();
        DarkenSprite(activeBlockSprite);
        objectiveInteractables = this.transform.GetComponentsInChildren<BaseInteractable>();
        CurrentHealth = maxHealth;

        foreach (SculptorInteractable objective in objectiveInteractables)
        {
            objective.minigameManager = this;
            objective.vignetteSculptor = this;
        }
    }

    public void Update()
    {
        if (activeBlock)
        {
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 blockPosition = new Vector2(activeBlock.position.x, activeBlock.position.y);
            float distanceValue = maxCursorDistance - Vector2.Distance(cursorPosition, blockPosition);

            if (distanceValue < 0)
                distanceValue = 0;

            float newRotationZ = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

            // activeBlock.localRotation = new Quaternion(activeBlock.localRotation.x, activeBlock.localRotation.y, newRotationZ, activeBlock.localRotation.w);

            activeBlock.Rotate(Vector3.forward, newRotationZ * distanceValue);
        }
    }

    public void ResetSculpture ()
    {
        spawnerAnim.SetTrigger("DestroyStatue");
    }

    public bool CheckBlock(SculptorInteractable block)
    {
        if (block == sculptorBlocks[currentIndex])
        {
            return true;
        }
        return false;
    }

    public void UpdateActiveBlock()
    {
        currentIndex += 1;

        if (currentIndex < sculptorBlocks.Count)
        {
            activeBlock = sculptorBlocks[currentIndex].transform;
            activeBlockSprite = activeBlock.GetComponent<SpriteRenderer>();
            DarkenSprite(activeBlockSprite);
        }
    }

    public void UpdateBlockSprite()
    {
        statueAnimator.SetInteger("StatueHealth", CurrentHealth);
    }

    public void CheckProgress()
    {
        if (currentIndex >= sculptorBlocks.Count - 1)
        {
            base.ObjectiveComplete();
        }
    }

    public void DestroySculpture()
    {
        Destroy(currentStatue);
        StartMinigame();
    }

    public void DarkenSprite(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.color = new Color(0.8f, 0.8f, 0.8f);
    }
}
