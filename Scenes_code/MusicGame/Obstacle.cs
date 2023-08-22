using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public GameObject alignmentObject;
    [SerializeField] public List<Sprite> obstacles;
    [SerializeField] public float maxInterval;
    [SerializeField] public float minInterval;

    private void Awake ()
    {
        StartCoroutine(GenerateObstacles());
    }

    private IEnumerator GenerateObstacles ()
    {
        while (true)
        {
            if (!MiniGameRunStruct_Jump.GameState)
            {
                yield return new WaitForSeconds(0.1f);
                continue;
            }
            // ��ȡ alignRectTransform �� speed
            RectTransform alignRectTransform = alignmentObject.GetComponent<RectTransform>();
            //float speed = GetRandomSpeed();

            Sprite obstacle = obstacles[Random.Range(0, obstacles.Count)];

            float interval = Random.Range(minInterval, maxInterval);

            // �����µ��ϰ��� GameObject
            GameObject obstacleObj = new GameObject("Obstacle");
            obstacleObj.tag = "Obstacle";

            obstacleObj.transform.SetParent(GameObject.Find("MainCanvas").transform, false);

            // ��� RectTransform ���
            RectTransform obstacleRectTransform = obstacleObj.AddComponent<RectTransform>();
            obstacleRectTransform.anchorMin = new Vector2(1, 0);
            obstacleRectTransform.anchorMax = new Vector2(1, 0);
            obstacleRectTransform.pivot = new Vector2(0.5f, 0.5f);

            // �����ϰ���λ�úʹ�С
            Image obstacleImage = obstacleObj.AddComponent<Image>();
            obstacleImage.sprite = obstacle;
            float sizeRatio = 0.5f;
            obstacleRectTransform.sizeDelta = new Vector2(obstacle.rect.width * sizeRatio, obstacle.rect.height * sizeRatio);

            // ���ϰ���ײ����뵽 alignRectTransform �ĵײ�
            float groundY = alignRectTransform.anchoredPosition.y - alignRectTransform.rect.height / 2f;
            float obstacleY = groundY + obstacleRectTransform.rect.height / 2f;
            obstacleRectTransform.anchoredPosition = new Vector2(-obstacleRectTransform.rect.width / 2f, obstacleY);

            // ��� ObstacleMovement ���
            obstacleObj.AddComponent<ObstacleMovement>();
            obstacleObj.AddComponent<MeshRenderer>();

            var tosize = obstacleRectTransform.sizeDelta; tosize.x *= 0.8f;
            obstacleObj.AddComponent<BoxCollider2D>().size = tosize;

            RectTransform obstacleRect = obstacleObj.GetComponent<RectTransform>();

            // �ȴ���һ������
            yield return new WaitForSeconds(interval);
        }
    }

    private int GetRandomSpeed ()
    {
        return NewRandom.GetRandomInAB(300, 400);
    }
}