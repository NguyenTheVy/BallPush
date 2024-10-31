using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float playerSpeed;
    [SerializeField] private PlayerController playerController;

    public bool isEndGame = false;
    public bool isSpawnTrailfx = false;


    // Điểm giới hạn di chuyển
    [SerializeField] private Transform bottomLeftLimit;
    [SerializeField] private Transform topRightLimit;

    public Vector3 targetPosition;
    [SerializeField] private bool isMoving = false;  // Kiểm soát trạng thái di chuyển

    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public Transform BottomLeftLimit { get => bottomLeftLimit; set => bottomLeftLimit = value; }
    public Transform TopRightLimit { get => topRightLimit; set => topRightLimit = value; }

    [SerializeField] private List<GameObject> ballsInPlay = new List<GameObject>(); // Danh sách các bóng đang chơi

    private void Awake()
    {
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }
    private void OnEnable()
    {
        targetPosition = playerTransform.position;  // Khởi tạo vị trí ban đầu
    }


    private void OnSwipe(string swipe)
    {
        // Chỉ xử lý vuốt khi nhân vật không di chuyển
        if (!isMoving && !isEndGame)
        {
            switch (swipe)
            {
                case "Left":
                    targetPosition = new Vector3(bottomLeftLimit.localPosition.x, playerTransform.localPosition.y, playerTransform.localPosition.z);
                    if (playerTransform.localPosition.x > bottomLeftLimit.localPosition.x)
                    {
                        playerTransform.DOScaleY(0.25f, 0.01f);
                    };
                    break;
                case "Right":
                    targetPosition = new Vector3(topRightLimit.localPosition.x, playerTransform.localPosition.y, playerTransform.localPosition.z);
                    if (playerTransform.localPosition.x < topRightLimit.localPosition.x)
                    {
                        playerTransform.DOScaleY(0.25f, 0.01f);
                    };
                    break;
                case "Up":
                    targetPosition = new Vector3(playerTransform.localPosition.x, topRightLimit.localPosition.y, playerTransform.localPosition.z);

                    if (Mathf.Abs(playerTransform.localPosition.y - topRightLimit.localPosition.y) > 0.001f)
                    {
                        playerTransform.DOScaleX(0.25f, 0.01f);
                    }

                    break;
                case "Down":
                    targetPosition = new Vector3(playerTransform.localPosition.x, bottomLeftLimit.localPosition.y, playerTransform.localPosition.z);
                    if (playerTransform.localPosition.y > bottomLeftLimit.localPosition.y)
                    {
                        playerTransform.DOScaleX(0.25f, 0.01f);
                    };
                    break;
            }

            // Kiểm tra nếu điểm đến khác với vị trí hiện tại thì bắt đầu di chuyển
            if (targetPosition != playerTransform.position)
            {
                isMoving = true;  // Khóa vuốt khi bắt đầu di chuyển
            }
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            if (!isSpawnTrailfx)
            {
                SpawnTrail();
                isSpawnTrailfx = true;
            }

            // Di chuyển nhân vật tới targetPosition
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPosition, playerSpeed * Time.deltaTime);

            // Khi đã tới vị trí mục tiêu thì mở khóa vuốt
            if (playerTransform.position == targetPosition)
            {
                playerTransform.localScale = Vector3.one * 0.45f;
                isMoving = false;
                isSpawnTrailfx = false;
                playerTransform.GetComponent<PlayerController>().isLimit = false;
            }
        }
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }

    public void SpawnTrail()
    {
        if (GameManager.instance.Traifx == null) return;

        ParticleSystem Trail = Instantiate(GameManager.instance.Traifx);

        Trail.transform.SetParent(playerTransform);
        Trail.transform.localPosition = Vector3.zero;
        Trail.transform.localScale = Vector3.one;

    }

    // Gọi hàm này khi một bóng được đưa vào lỗ
    public void OnBallEnteredHole(GameObject ball)
    {
        if (ballsInPlay.Contains(ball))
        {
            ballsInPlay.Remove(ball); // Xóa bóng khỏi danh sách
        }

        CheckWinCondition(); // Kiểm tra điều kiện thắng
    }

    // Thêm bóng vào danh sách khi bắt đầu game
    public void AddBall(GameObject ball)
    {
        if (!ballsInPlay.Contains(ball))
        {
            ballsInPlay.Add(ball);
        }
    }

    // Kiểm tra điều kiện thắng
    public void CheckWinCondition()
    {
        if (ballsInPlay.Count == 0)
        {
            if (isEndGame) return;

            UiGamePlay.instance.popupWin.gameObject.SetActive(true);
            isEndGame = true;
            // Gọi phương thức thắng game, chẳng hạn như hiện thông báo hoặc chuyển cảnh
        }
    }

    public void OnPlayerFellIntoHole()
    {
        if (isEndGame) return;
        UiGamePlay.instance.popupLose.gameObject.SetActive(true);
        isEndGame = true;
        // Thực hiện hành động thua, chẳng hạn như hiện thông báo hoặc chuyển cảnh
    }
}
