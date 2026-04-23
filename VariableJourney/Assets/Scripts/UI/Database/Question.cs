using UnityEngine;
using System.Collections.Generic;

public class Question 
{
    public int questionNum;
    public string question;
    public List<(string, bool)> answers = new List<(string, bool)>();
}
