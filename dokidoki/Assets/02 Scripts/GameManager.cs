using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    // ���Ͱ� ������ ��ġ�� ������ List Ÿ�� ����
    public List<Transform> points = new List<Transform>();
    // ���͸� �̸� ������ ������ ����Ʈ �ڷ���
    public List<GameObject> monsterPool = new List<GameObject>();
    // ������Ʈ Ǯ(Object Pool)�� ������ ������ �ִ� ����
    public int maxMonsters = 10;
    // ���� �������� ������ ����
    public GameObject monster;
    // ������ ���� ����
    public float createTime = 3.0f;
    // ������ ���� ���θ� ������ ��� ����
    private bool isGameOver;
    // ������ ���� ���θ� ������ ������Ƽ
    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if (isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }
    // �̱��� �ν��Ͻ� ����
    public static GameManager instance = null;
    // ��ũ��Ʈ�� ����Ǹ� ���� ���� ȣ��Ǵ� ����Ƽ �̺�Ʈ �Լ�
    void Awake()
    {
        // instance�� �Ҵ���� �ʾ��� ���
        if (instance == null)
        {
            instance = this;
        }
        // instance�� �Ҵ�� Ŭ������ �ν��Ͻ��� �ٸ� ��� ���� ������ Ŭ������ �ǹ���
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        // �ٸ� ������ �Ѿ���� �������� �ʰ� ������
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        // ���� ������Ʈ Ǯ ����
        CreateMonsterPool();
        // SpawnPointGroup ���ӿ�����Ʈ�� Transform ������Ʈ ����
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;
        // SpawnPointGroup ������ �ִ� ��� ���ϵ� ���ӿ�����Ʈ�� Transform ������Ʈ ����
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
        }
        // ������ �ð� �������� �Լ��� ȣ��
        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }
    void CreateMonster()
    {
        // ������ �ұ�Ģ�� ���� ��ġ ����
        int idx = Random.Range(0, points.Count);
        // ���� ������ ����
        Instantiate(monster, points[idx].position, points[idx].rotation);
    }
    // ������Ʈ Ǯ�� ���� ����
    void CreateMonsterPool()
    {
        for (int i = 0; i < maxMonsters; i++)
        {
            // ���� ����
            var _monster = Instantiate<GameObject>(monster);
            // ������ �̸��� ����
            _monster.name = $"Monster_{i:00}";
            // ���� ��Ȱ��ȭ
            _monster.SetActive(false);
            // ������ ���͸� ������Ʈ Ǯ�� �߰�
            monsterPool.Add(_monster);
        }
    }
}