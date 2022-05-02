using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicBase
{
    public const int EMPTY = 0;
    public const int ERROR = -1;

    int mCheckValue = ERROR; // 현재 탐색중인 돌의 색 

    public void setCheckValue(int _newcheckvalue)
    {
        mCheckValue = _newcheckvalue;
    }

    public bool isSequential(int _data, ref int _length)
    {
        if(_data == mCheckValue)
        {
            _length++;
            return true;
        }
        else
        {
            return false;
        }
    }
}