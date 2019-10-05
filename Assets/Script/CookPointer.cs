using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPointer : MonoBehaviour
{
    CookingManager cookingManager;

    Camera mainCamera;

    [SerializeField]
    private bool isHold, isInput, isGive;

    [SerializeField]
    Transform target;

    [SerializeField]
    GameObject foodPrefab;

    Vector3 mouseScreenPosition, mouseWorldPosition;

    private void Awake()
    {
        cookingManager = FindObjectOfType<CookingManager>();
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!isInput)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            isHold = true;

            SoundManager.Instance.PlaySFX(SoundManager.SFX_SOUND.CATCH);

            UpdateMousePosition();
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(mouseScreenPosition)
                , Vector3.forward
                , Mathf.Infinity);

            if (hit)
            {
                if (isGive && hit.collider.gameObject.CompareTag("NPC"))
                {
                    //TODO :: NPC에게 줌
                    SetInput(false);
                    SetGive(false);
                    cookingManager.GiveNpc();
                }
                else if (!isGive && hit.collider.gameObject.CompareTag("FoodSlot"))
                {
                    FoodSlot slot = hit.collider.gameObject.GetComponent<FoodSlot>();

                    GameObject g = Instantiate(foodPrefab, transform);
                    g.transform.localPosition = Vector3.zero;

                    SetTarget(g.transform);
                    g.GetComponent<Collider2D>().enabled = false;
                    target.GetComponent<Rigidbody2D>().simulated = false;
                    FoodBehaviour food = g.GetComponent<FoodBehaviour>();
                    food.SetFood(slot.FoodID);
                    food.startMoveStick += RemoveTarget;
                }
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            isHold = false;

            if (target == null)
                return;

            target.GetComponent<Collider2D>().enabled = true;
            target.GetComponent<Rigidbody2D>().simulated = true;
            target.SetParent(null);
            RemoveTarget();

        }

        if (isHold)
        {
            UpdateMousePosition();
            transform.position = mouseWorldPosition;
        }
    }

    void UpdateMousePosition()
    {
        mouseScreenPosition = Input.mousePosition;

        mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0;
    }

    void SetTarget(Transform target)
    {
        this.target = target;
        target.SetParent(transform);
    }

    void RemoveTarget()
    {
        this.target = null;
    }

    public void SetInput(bool isInput)
    {
        this.isInput = isInput;
    }

    public void SetGive(bool isGive)
    {
        this.isGive = isGive;
    }


}
