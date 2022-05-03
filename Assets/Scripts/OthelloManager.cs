using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class OthelloManager : MonoBehaviour
{
    LogicOthello lg = new LogicOthello(8, 8);

    GameObject b_stone, w_stone; // 흑돌, 백돌

    Vector2 origin; // 마우스 좌표

    bool GameEnds = true; // true: 게임종료상태, false: 게임진행중

    GameObject[,] ObjArr; // 소환한 stone clone들 저장할 배열.

    GameObject TurnStone;
    public Sprite[] StoneColor;

    GameObject rb;
    public Text res_text; // 결과창

    private void Awake()
    {
        GameEnds = false; // 게임 시작.
    }

    private void Start()
    {
        b_stone = Resources.Load("stone_black_p") as GameObject;
        w_stone = Resources.Load("stone_white_p") as GameObject;

        TurnStone = GameObject.Find("TurnStone");

        rb = GameObject.Find("ResultParent").transform.Find("Result").gameObject;

        ObjArr = new GameObject[8, 8];
        Render(); // 첫 4개돌 렌더링.


    }

    private void Update()
    {

        if (!GameEnds)
        {
            lg.CurrentTurn(TurnStone, StoneColor);
            if (Input.GetMouseButtonDown(0))
            {

                Put(); // put stone

                Render(); // 돌 놓을때마다 렌더링.
                if (CheckEnd()) // 
                {
                    lg.nextTurn(); // 현재 차례인 돌이 놓을곳이 없음 -> Turn 넘김
                    if (CheckEnd()) GameEnds = true; // 다음 차례의 돌도 놓을곳 없으면 게임끝

                }

                //StartCoroutine(Flap());
            }
        }
        else // GameEnds가 true, 즉 게임 끝
        {
            ResultBoard();
        }
    }

    private void Render()
    {

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Destroy(ObjArr[i, j]);
            }
        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (lg.getData(i, j) == 1) // White
                {
                    ObjArr[i, j] = Instantiate(w_stone,
                        arr_to_pos(i, j), Quaternion.identity);
                }
                else if (lg.getData(i, j) == 2) // Black
                {
                    ObjArr[i, j] = Instantiate(b_stone,
                        arr_to_pos(i, j), Quaternion.identity);
                }

            }
        }
    }

    // 배열좌표 -> 보드마우스좌표
    Vector2 arr_to_pos(int x, int y)
    {
        int _x, _y;
        _x = -3 + y;
        _y = 3 - x;

        return new Vector2(_x, _y);
    }

    // 보드마우스좌표 -> 배열좌표
    Vector2 pos_to_arr(Vector2 origin)
    {
        int x, y;
        x = (-3 - (int)origin.x) * (-1);
        y = 3 - (int)origin.y;

        return new Vector2(y, x);
    }

    void Put()
    {
        origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lg.checkpos(ref origin); // 소수점 좌표 수정
        Vector2 changed = pos_to_arr(origin);

        lg.setData((int)changed.x, (int)changed.y);
    }

    private void ResultBoard()
    {
        // 0 : Draw, 1: White Wins, 2: Black Wins
        int Winner = lg.WhoWins();

        if (Winner == 1)
        {
            res_text.GetComponent<Text>().text = "WHITE WIN";
        }
        else if (Winner == 2)
        {
            res_text.GetComponent<Text>().text = "BLACK WIN";
        }
        else
        {
            res_text.GetComponent<Text>().text = "DRAW";
        }

        rb.SetActive(true);
    }

    // return true면 게임끝
    bool CheckEnd()
    {
        int mLengthCopy;
        int mTurnCopy;
        int[,] mDatCopy;

        mLengthCopy = lg.getLength(); // 초기 mLength 저장
        mTurnCopy = lg.getTurn(); // 초기 mTurn 저장

        // 초기 mDat 저장
        mDatCopy = new int[8, 8];

        for (int i = 0; i < 8; i++) // mDatCopy에 lg.mDat 복사
        {
            for (int j = 0; j < 8; j++)
            {
                mDatCopy[i, j] = lg.getData(i, j);
            }
        }

        // 빈자리에 돌 넣어봄(시뮬레이션)
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // 이전으로 데이터 복원
                lg.RecoverData(mLengthCopy, mTurnCopy, mDatCopy);

                // ChecksetData가 하나라도 true 리턴하면 아직 놓을자리가 있다.
                if (lg.ChecksetData(i, j))
                {
                    lg.RecoverData(mLengthCopy, mTurnCopy, mDatCopy);
                    return false;
                }
            }
        }

        // 하나도 true 리턴하지 못하고 여기까지오면 이제 놓을자리가 없다 = GameEnd
        return true;

    }


    IEnumerator Flap()
    {


        for (int j = 0; j < lg.getLength(); j++)
        {
            for (int i = 0; i < 12; i++)
            {
                Vector2 res = lg.getmPoints(j);

                ObjArr[(int)res.x, (int)res.y].transform.localScale
                    -= new Vector3(0.05f, 0, 0);

                yield return null;
            }

        }
    }
}