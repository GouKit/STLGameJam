using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoodBehaviour : MonoBehaviour
{
    FoodDB db;

    [SerializeField]
    Food food;

    [SerializeField]
    float moveSpeed;

    SpriteRenderer spriteRenderer;

    private Collider2D coll;

    Coroutine updateMovement;

    public UnityAction startMoveStick, finishMoveStick;


    private void Awake()
    {
        db = FindObjectOfType<FoodDB>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    public void SetFood(int id)
    {
        this.food = new Food(db.FindFoodWithID(id));

        SetSprite();
    }

    public void SetFood(Food f)
    {
        food = new Food(f);
        SetSprite();
    }

    public Food GetFood()
    {
        return food;
    }

    public void MoveStick(Vector3 position)
    {
        if (updateMovement != null)
            StopCoroutine(updateMovement);

        updateMovement = StartCoroutine(UpdateMovement(position));
    }

    IEnumerator UpdateMovement(Vector3 position)
    {
        if (startMoveStick != null)
        {
            startMoveStick.Invoke();
        }

        GetComponent<Collider2D>().enabled = false;

        while ((position - transform.position).sqrMagnitude > 0.001f)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, position, Time.deltaTime * moveSpeed);
            yield return null;
        }

        transform.position = position;

        if (finishMoveStick != null)
        {
            finishMoveStick.Invoke();
        }

    }

    void SetSprite()
    {
        spriteRenderer.sprite = food.GetSprite();
    }


}
