using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomName {
    
    public static string Generate()
    {
        var FirstNames = FileSystem.FromJson<string>("/EntityData/Names/firstnames.json").ToList();
        var Surnames = FileSystem.FromJson<string>("/EntityData/Names/surnames.json").ToList();

        var firstn = FirstNames[Random.Range(0, FirstNames.Count - 1)];
        var lastn = Surnames[Random.Range(0, Surnames.Count - 1)];

        return firstn + " " + lastn;
    }

}
