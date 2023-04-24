using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Details: PlayNoteOnTone
// Logic to play the correct Wwise event on each note

public class PlayNoteOnTone : MonoBehaviour
{
    public AK.Wwise.Event C2;
    public AK.Wwise.Event C2s;
    public AK.Wwise.Event D2b;
    public AK.Wwise.Event D2;
    public AK.Wwise.Event D2s;
    public AK.Wwise.Event E2b;
    public AK.Wwise.Event E2;
    public AK.Wwise.Event F2;
    public AK.Wwise.Event F2s;
    public AK.Wwise.Event G2b;
    public AK.Wwise.Event G2;
    public AK.Wwise.Event G2s;
    public AK.Wwise.Event A2b;
    public AK.Wwise.Event A2;
    public AK.Wwise.Event A2s;
    public AK.Wwise.Event B2b;
    public AK.Wwise.Event B2;
    public AK.Wwise.Event C3;
    public AK.Wwise.Event C3s;
    public AK.Wwise.Event D3b;
    public AK.Wwise.Event D3;
    public AK.Wwise.Event D3s;
    public AK.Wwise.Event E3b;
    public AK.Wwise.Event E3;
    public AK.Wwise.Event F3;
    public AK.Wwise.Event F3s;
    public AK.Wwise.Event G3b;
    public AK.Wwise.Event G3;
    public AK.Wwise.Event G3s;
    public AK.Wwise.Event A3b;
    public AK.Wwise.Event A3;
    public AK.Wwise.Event A3s;
    public AK.Wwise.Event B3b;
    public AK.Wwise.Event B3;
    public AK.Wwise.Event C4;
    public AK.Wwise.Event C4s;
    public AK.Wwise.Event D4b;
    public AK.Wwise.Event D4;
    public AK.Wwise.Event D4s;
    public AK.Wwise.Event E4b;
    public AK.Wwise.Event E4;
    public AK.Wwise.Event F4;
    public AK.Wwise.Event F4s;
    public AK.Wwise.Event G4b;
    public AK.Wwise.Event G4;
    public AK.Wwise.Event G4s;
    public AK.Wwise.Event A4b;
    public AK.Wwise.Event A4;
    public AK.Wwise.Event A4s;
    public AK.Wwise.Event B4b;
    public AK.Wwise.Event B4;

    private AK.Wwise.Event currentNote = new AK.Wwise.Event();


    // Classify and filter down each information passed through the method
    public void PlayNote(string materialToNote, string _nameGOOctave, string _nameGOSharpFlat, GameObject tone)
    {
        switch (materialToNote)
        {
            case "C":
                switch (_nameGOOctave)
                {
                    case "2":
                        switch (_nameGOSharpFlat)
                        {
                            case "s":
                                currentNote = C2s;
                                break;
                            default:
                                currentNote = C2;
                                break;
                        }
                        break;
                    case "3":
                        switch (_nameGOSharpFlat)
                        {
                            case "s":
                                currentNote = C3s;
                                break;
                            default:
                                currentNote = C3;
                                break;
                        }
                        break;
                    case "4":
                        switch (_nameGOSharpFlat)
                        {
                            case "s":
                                currentNote = C3s;
                                break;
                            default:
                                currentNote = C3;
                                break;
                        }
                        break;
                }
                break;
            case "D":
                switch (_nameGOOctave)
                {
                    case "2":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = D2b;
                                break;
                            case "s":
                                currentNote = D2s;
                                break;
                            default:
                                currentNote = D2;
                                break;
                        }
                        break;
                    case "3":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = D3b;
                                break;
                            case "s":
                                currentNote = D3s;
                                break;
                            default:
                                currentNote = D3;
                                break;
                        }
                        break;
                    case "4":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = D4b;
                                break;
                            case "s":
                                currentNote = D4s;
                                break;
                            default:
                                currentNote = D4;
                                break;
                        }
                        break;
                }
                break;
            case "E":
                switch (_nameGOOctave)
                {
                    case "2":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = E2b;
                                break;
                            default:
                                currentNote = E2;
                                break;
                        }
                        break;
                    case "3":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = E3b;
                                break;
                            default:
                                currentNote = E3;
                                break;
                        }
                        break;
                    case "4":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = E4b;
                                break;
                            default:
                                currentNote = E4;
                                break;
                        }
                        break;
                }
                break;
            case "F":
                switch (_nameGOOctave)
                {
                    case "2":
                        switch (_nameGOSharpFlat)
                        {
                            case "s":
                                currentNote = F2s;
                                break;
                            default:
                                currentNote = F2;
                                break;
                        }
                        break;
                    case "3":
                        switch (_nameGOSharpFlat)
                        {
                            case "s":
                                currentNote = F3s;
                                break;
                            default:
                                currentNote = F3;
                                break;
                        }
                        break;
                    case "4":
                        switch (_nameGOSharpFlat)
                        {
                            case "s":
                                currentNote = F4s;
                                break;
                            default:
                                currentNote = F4;
                                break;
                        }
                        break;
                }
                break;
            case "G":
                switch (_nameGOOctave)
                {
                    case "2":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = G2b;
                                break;
                            case "s":
                                currentNote = G2s;
                                break;
                            default:
                                currentNote = G2;
                                break;
                        }
                        break;
                    case "3":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = G3b;
                                break;
                            case "s":
                                currentNote = G3s;
                                break;
                            default:
                                currentNote = G3;
                                break;
                        }
                        break;
                    case "4":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = G4b;
                                break;
                            case "s":
                                currentNote = G4s;
                                break;
                            default:
                                currentNote = G4;
                                break;
                        }
                        break;
                }
                break;
            case "A":
                switch (_nameGOOctave)
                {
                    case "2":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = A2b;
                                break;
                            case "s":
                                currentNote = A2s;
                                break;
                            default:
                                currentNote = A2;
                                break;
                        }
                        break;
                    case "3":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = A3b;
                                break;
                            case "s":
                                currentNote = A3s;
                                break;
                            default:
                                currentNote = A3;
                                break;
                        }
                        break;
                    case "4":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = A3b;
                                break;
                            case "s":
                                currentNote = A3s;
                                break;
                            default:
                                currentNote = A3;
                                break;
                        }
                        break;
                }
                break;
            case "B":
                switch (_nameGOOctave)
                {
                    case "2":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = B2b;
                                break;
                            default:
                                currentNote = B2;
                                break;
                        }
                        break;
                    case "3":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = B3b;
                                break;
                            default:
                                currentNote = B3;
                                break;
                        }
                        break;
                    case "4":
                        switch (_nameGOSharpFlat)
                        {
                            case "b":
                                currentNote = B4b;
                                break;
                            default:
                                currentNote = B4;
                                break;
                        }
                        break;
                }
                break;
        }


        // Play the note accordingly
        // Without accidentals
        if (_nameGOSharpFlat != "b")
        {
            AkSoundEngine.SetSwitch("Horns", materialToNote + _nameGOOctave, tone);
            AkSoundEngine.PostEvent("Play_Horn", tone);
        }
        // For flats:
        else
        {
            AkSoundEngine.SetSwitch("Horns", materialToNote + _nameGOOctave + _nameGOSharpFlat, tone);
            AkSoundEngine.PostEvent("Play_Horn", tone);
        }

    }
}
