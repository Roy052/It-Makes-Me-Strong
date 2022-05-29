using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    public string[] angelMessages_1st = 
        {"I'll save you", "You have to get out of here", "through the gate" };
    public string[] angelMessages_monster =
        {"The monster crushed you", "Did you feel that you were weak?", "You have enough power to defeat it", "Don't give up. The monster can be defeated", "I'll share my power." };
    public string[] angelMessages_fire =
        {"You were burnt to ashes", "You don't seem to be afraid of fire", "" };
    public string[] angelMessages_fall =
        {"Did you stumble on your foot?", "You fell off and broke into pieces", "You like to fall and die, don't you?", "Don't fall and keep move. You don't have much time", "I'll share my power" };
    public string[] angelMessages_electric =
        {"I'll save you", "You have to get out of here", "through the gate" };
    public string[] angelMessages_boss =
        {"I'll save you", "You have to get out of here", "through the gate", "", "" };

    public string[,] playerMessages = {
        { },
        { },
        { }
    };
}
