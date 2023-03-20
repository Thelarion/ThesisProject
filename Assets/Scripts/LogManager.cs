using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogManager : MonoBehaviour
{
    private static float startTime;
    private static float endTime;
    private static int totalPoints;
    private static int triggerCount;
    private static int missedTaps;
    private static int melodyCount;
    private static int north;
    private static int east;
    private static int south;
    private static int west;
    private static float voiceVolume = 5f;
    private static float musicVolume = 5f;
    private static float effectsVolume = 5f;




    public static float StartTime { get => startTime; set => startTime = value; }
    public static float EndTime { get => endTime; set => endTime = value; }
    public static int TotalPoints { get => totalPoints; set => totalPoints = value; }
    public static int TriggerCount { get => triggerCount; set => triggerCount = value; }
    public static int MissedTaps { get => missedTaps; set => missedTaps = value; }
    public static int MelodyCount { get => melodyCount; set => melodyCount = value; }
    public static int North { get => north; set => north = value; }
    public static int East { get => east; set => east = value; }
    public static int South { get => south; set => south = value; }
    public static int West { get => west; set => west = value; }
    public static float VoiceVolume { get => voiceVolume; set => voiceVolume = value; }
    public static float MusicVolume { get => musicVolume; set => musicVolume = value; }
    public static float EffectsVolume { get => effectsVolume; set => effectsVolume = value; }

    public static void ResetLogging()
    {
        TotalPoints = 0;
        TriggerCount = 0;
        MissedTaps = 0;
        MelodyCount = 0;
        North = 0;
        South = 0;
        East = 0;
        West = 0;
    }


    // Update is called once per frame
    public static void PrintToTxt()
    {
        StreamWriter writer = new StreamWriter(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\TapToneStats.txt", true);
        writer.WriteLine("Data Log file: Tap Tone");
        writer.WriteLine("Time of study: " + System.DateTime.Now.ToString());
        writer.WriteLine("-------- Time Measurement --------");
        writer.WriteLine("Start: " + startTime.ToString());
        writer.WriteLine("End: " + endTime.ToString());
        writer.WriteLine("Total Time: " + (endTime - startTime).ToString());
        writer.WriteLine("-------- Statistics --------");
        writer.WriteLine("Points/Score: " + totalPoints.ToString());
        writer.WriteLine("Trigger count: " + triggerCount.ToString());
        writer.WriteLine("Taps on wrong note: " + missedTaps.ToString());
        writer.WriteLine("North: " + north.ToString());
        writer.WriteLine("East: " + east.ToString());
        writer.WriteLine("South: " + south.ToString());
        writer.WriteLine("West: " + west.ToString());
        writer.WriteLine("-------- Volume Level --------");
        writer.WriteLine("MusicVolume: " + musicVolume.ToString());
        writer.WriteLine("VoiceVolume: " + voiceVolume.ToString());
        writer.WriteLine("EffectsVolume: " + effectsVolume.ToString());

        writer.Flush();
        writer.Close();
    }
}
