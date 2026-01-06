using UnityEngine;
using System.Collections.Generic;
using System;


public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    private List<Transform> _segments;

    public Transform segmentPrefab;

    [SerializeField] private int initialSize = 0;

    private void Awake()
    {
        _segments = new List<Transform> ();
        _segments.Add(transform);
        for(int i = 0;i<initialSize;i++)
        {
            Grow();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) _direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S)) _direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A)) _direction = Vector2.left;
        else if(Input.GetKeyDown(KeyCode.D)) _direction = Vector2.right;
    }

    private void FixedUpdate()
    {
        for(int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i-1].position;
        }  

        transform.position = new Vector3(
            Mathf.Round(transform.position.x + _direction.x),
            Mathf.Round(transform.position.y + _direction.y),
            0f
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Food")) Grow();
        else if (collision.tag.Equals("Walls") || collision.tag.Equals("SnakeSegment")) ResetState();
    }

    private void ResetState()
    {
        for(int i=1;i< _segments.Count;i++)Destroy(_segments[i].gameObject);
        _segments.Clear();
        _segments.Add(transform);
        transform.position = Vector3.zero;
        Time.timeScale = 1f;
    }

    private void Grow()
    {
        Transform newSegment = Instantiate(segmentPrefab);
        newSegment.position = _segments[^1].position;
        _segments.Add(newSegment);
        Time.timeScale += 0.05f;
    }


}
