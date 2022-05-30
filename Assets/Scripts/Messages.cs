using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    public string[] angelMessages_1st = 
        {"I'll save you", "You have to get out of here", "through the gate" };
    public string[] angelMessages_monster =
        {"The monster crushed you, but you will be stronger", "","Did you feel that you were weak?", "", "Don't give up. The monster can be defeated", "I'll share my power." };
    public string[] angelMessages_fire =
        {"You were burnt to ashes, but you will be stronger ", "", "Isn't it hot", "", "You don't seem to be afraid of fire", "I'll share my power" };
    public string[] angelMessages_fall =
        {"Did you stumble on your foot?", "You fell off and broke into pieces", "You like to fall and die, don't you?",  "Don't fall and keep move. You don't have much time", "I'll share my power" };
    public string[] angelMessages_electric =
        {"You are not exist anymore, but you will be stronger", "", "It was quite shocking", "", "","I'll share my power" };
    public string[] angelMessages_boss =
        {"The gatekeeper destroyed you, but you will be stronger", "You became more than that", "Still need power?", "You are so greedy", "" };

    public string[] playerMessage_Death = { };
    public string[] playerMessages_confidence = { "I felt something strange", "Did I.. Nevermind", "Wait a seconds..", "Who's there?","Someone's controlling me", "Who are you!", "I.. I don't want this anymore", "Please give mercy..", "Another way.. another way", "hahahahaha", "..."};
    public string playerMessage_Fly = "";
}
