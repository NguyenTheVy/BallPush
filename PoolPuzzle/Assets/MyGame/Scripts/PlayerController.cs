using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _collider2D;

    public Animator animator;

    public bool isLimit = false;

    private void Start()
    {
        _rb = transform.GetComponent<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();

        CircleCollider2D circleCollider2D = transform.GetComponent<CircleCollider2D>();
        if(circleCollider2D != null )
        {
            Destroy( circleCollider2D );
            transform.AddComponent<BoxCollider2D>();
        }

        _collider2D = transform.GetComponent<BoxCollider2D>();
        _collider2D.offset = new Vector3(-0.04f, 0.04f);
        _collider2D.size = new Vector3(0.8f, 0.8f);

        float x = RoundToNearestFive(transform.localPosition.x);
        float y = RoundToNearestFive(transform.localPosition.y);
        transform.localPosition = new Vector2(x, y);
    }

    private float RoundToNearestFive(float value)
    {
        float decimalPart = Mathf.Abs(value * 100) % 10;

        if (decimalPart != 0 && decimalPart != 5)
        {
            return Mathf.Floor(value * 10) / 10 + 0.05f;
        }
        return value;
    }

    private void Update()
    {
        if (IsAtLimits(transform.position) && !isLimit)
        {
            isLimit = true;
            transform.localScale = Vector3.one * 0.45f;
            SoundManager.Instance.PlayFxSound(SoundManager.Instance.HitBoard);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BallRed"))
        {
            RoundPosXYPlayer();
            DOTween.Kill(transform);
            transform.localScale = Vector3.one * 0.45f;
            _rb.velocity = Vector2.zero;

            //Vector2 hitDirection = collision.transform.position - transform.position;
            Vector2 hitDirection = (collision.transform.position - transform.position).normalized;

            BackPush(hitDirection, collision);

            ObstacleBall ballController = collision.gameObject.GetComponent<ObstacleBall>();
            if (ballController != null)
            {
                SoundManager.Instance.PlayFxSound(SoundManager.Instance.Ballhit);
                ballController.MoveBall(hitDirection);
            }

            GameManager.instance.CurrentLevel.IsMoving = false;
        }
    }

    public void RoundPosXYPlayer()
    {
        float x = RoundToNearestFive(transform.localPosition.x);
        float y = RoundToNearestFive(transform.localPosition.y);
        transform.localPosition = new Vector2(x, y);
    }


    private void BackPush(Vector2 hitDirection, Collision2D collision)
    {
        if (Mathf.Abs(hitDirection.x) > Mathf.Abs(hitDirection.y))
        {
            hitDirection = new Vector2(hitDirection.x > 0 ? 1 : -1, 0);
        }
        else
        {
            hitDirection = new Vector2(0, hitDirection.y > 0 ? 1 : -1);
        }

        if (hitDirection.x > 0)
        {
            transform.position = new Vector3(collision.transform.position.x - 0.4f, collision.transform.position.y, 0);
        }
        else if (hitDirection.x < 0)
        {
            transform.position = new Vector3(collision.transform.position.x + 0.4f, collision.transform.position.y, 0);
        }
        else if (hitDirection.y > 0)
        {
            transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y - 0.4f, 0);
        }
        else if (hitDirection.y < 0)
        {
            transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 0.4f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hole"))
        {
            // Kiểm tra vị trí của BallPlayer
            if (!IsAtLimits(transform.position))
            {
                // Nếu không ở giới hạn, gọi hàm DisablePlayer
                DisablePlayer();
            }
            else
            {
                //Debug.Log("BallPlayer không bị tắt vì đã chạm vào giới hạn.");
            }
        }
    }


    private void DisablePlayer()
    {
        transform.localScale = Vector3.one * 0.45f;
        // Thực hiện tween scale về 0 cho BallPlayer
        transform.DOScale(0, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            GameManager.instance.CurrentLevel.OnPlayerFellIntoHole(); // Gọi hàm khi BallPlayer rơi vào lỗ
            gameObject.SetActive(false); // Vô hiệu hóa BallPlayer sau khi scale về 0
           // Debug.Log("BallPlayer đã bị tắt do vào vùng lỗ.");
        });
    }


    private bool IsAtLimits(Vector2 position)
    {
        // Kiểm tra xem BallPlayer có chạm vào giới hạn không
        return position.x <= GameManager.instance.CurrentLevel.BottomLeftLimit.position.x || position.x >= GameManager.instance.CurrentLevel.TopRightLimit.position.x ||
               position.y <= GameManager.instance.CurrentLevel.BottomLeftLimit.position.y || position.y >= GameManager.instance.CurrentLevel.TopRightLimit.position.y;
    }
}
