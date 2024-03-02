using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    private Vector2 vector2_BeginPos;
    private Vector2 vector2_EndPos;
    private List<GameObject> list_GameObject_Apples;

    public GameManager gameManager;

    public void InitDragHandler()
    {
        list_GameObject_Apples = new List<GameObject>();
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                vector2_BeginPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                vector2_EndPos = touch.position;
                gameObject.transform.position = vector2_BeginPos + (vector2_EndPos - vector2_BeginPos) / 2;
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Abs(vector2_EndPos.x - vector2_BeginPos.x) / 2, Mathf.Abs(vector2_EndPos.y - vector2_BeginPos.y) / 2);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Mathf.Abs(vector2_EndPos.x - vector2_BeginPos.x) / 2, Mathf.Abs(vector2_EndPos.y - vector2_BeginPos.y) / 2);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                gameManager.SumApples(list_GameObject_Apples);
                list_GameObject_Apples.Clear();
                gameObject.transform.position = new Vector2(0, 0);
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Apple")
        {
            list_GameObject_Apples.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Apple")
        {
            list_GameObject_Apples.Remove(collision.gameObject);
        }
    }
}
