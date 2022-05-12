//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text.RegularExpressions;

///*
// *  참조: https://etst.tistory.com/31
// *  코드원본: https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/#comment-7111
// *  코드가 수행되고 나면 List안에 2차원배열의 형태로 데이터가 저장된다. 
// *  data[행 번지수]["Header 이름"] = Value
// */

//public class CSVReader
//{
//    string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
//    string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
//    char[] TRIM_CHARS = { '\"' };

//    public List<Dictionary<string, object>> Read(string file)
//    {
//        var list = new List<Dictionary<string, object>>();
//        TextAsset data = Resources.Load(file) as TextAsset;

//        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

//        if (lines.Length <= 1) return list;

//        var header = Regex.Split(lines[0], SPLIT_RE);
//        for (var i = 1; i < lines.Length; i++)
//        {

//            var values = Regex.Split(lines[i], SPLIT_RE);
//            if (values.Length == 0 || values[0] == "") continue;

//            var entry = new Dictionary<string, object>();
//            for (var j = 0; j < header.Length && j < values.Length; j++)
//            {
//                string value = values[j];
//                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
//                object finalvalue = value;
//                int n;
//                float f;
//                if (int.TryParse(value, out n))
//                {
//                    finalvalue = n;
//                }
//                else if (float.TryParse(value, out f))
//                {
//                    finalvalue = f;
//                }
//                entry[header[j]] = finalvalue;
//            }
//            list.Add(entry);
//        }
//        return list;
//    }
//}