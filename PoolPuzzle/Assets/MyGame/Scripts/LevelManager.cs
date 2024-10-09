using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float playerSpeed;

    // Điểm giới hạn di chuyển
    [SerializeField] private Transform bottomLeftLimit;
    [SerializeField] private Transform topRightLimit;

    private Vector3 targetPosition;
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
        if (!isMoving)
        {
            switch (swipe)
            {
                case "Left":
                    targetPosition = new Vector3(bottomLeftLimit.localPosition.x, playerTransform.position.y, playerTransform.position.z);
                    break;
                case "Right":
                    targetPosition = new Vector3(topRightLimit.localPosition.x, playerTransform.position.y, playerTransform.position.z);

                    break;
                case "Up":
                    targetPosition = new Vector3(playerTransform.position.x, topRightLimit.localPosition.y, playerTransform.position.z);
                    break;
                case "Down":
                    targetPosition = new Vector3(playerTransform.position.x, bottomLeftLimit.localPosition.y, playerTransform.position.z);
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
            // Di chuyển nhân vật tới targetPosition
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPosition, playerSpeed * Time.deltaTime);

            // Khi đã tới vị trí mục tiêu thì mở khóa vuốt
            if (playerTransform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }


    // Gọi hàm này khi một bóng được đưa vào lỗ
    public void OnBallEnteredHole(GameObject ball)
    {
        if (ballsInPlay.Contains(ball))
        {
            ballsInPlay.Remove(ball); // Xóa bóng khỏi danh sách
            Debug.Log(ball.name + " đã vào lỗ.");
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
    private void CheckWinCondition()
    {
        if (ballsInPlay.Count == 0)
        {
            UiGamePlay.instance.popupWin.gameObject.SetActive(true);
            // Gọi phương thức thắng game, chẳng hạn như hiện thông báo hoặc chuyển cảnh
        }
    }

    public void OnPlayerFellIntoHole()
    {
        UiGamePlay.instance.popupLose.gameObject.SetActive(true);
        // Thực hiện hành động thua, chẳng hạn như hiện thông báo hoặc chuyển cảnh
    }
}
