using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicOmok : Logic2D
{
    public LogicOmok(int r, int c) : base(r, c) { }

    // true: 오목완성
    public bool setData(int r, int c)
    {
        if (!isEmpty(r, c)) return false;

        bool res = analyze(r, c);
        setValue(mTurn, r, c);
        nextTurn();

        return res;
    }

    // true : 오목완성
    // false : 미완성
    public override bool analyze(int r, int c)
    {
        int checkvalue = mTurn;

        for(Direction dir = Direction.U; dir <= Direction.DR; dir++)
        {
            // U, UR, R, DR 
            analyzeDirection(checkvalue, (int)dir, r, c);
            // D, DL, L, UL 
            analyzeDirection(checkvalue, (int)dir + 4, r, c);

            if (mLength >= 4) return true;
            resetLength();
        }
        return false;
    }
}
