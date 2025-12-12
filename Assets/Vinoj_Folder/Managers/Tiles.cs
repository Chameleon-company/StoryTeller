using UnityEngine;  // MUST be at the very top

public enum TileType 
{ 
    Normal, 
    MessageStick, 
    GoneWalkabout, 
    Artifact, 
    Waterhole, 
    MeetingPlace 
}

public enum FootprintColor 
{ 
    White, 
    Black 
}

public class Tile : MonoBehaviour
{
    public TileType tileType = TileType.Normal;
    public FootprintColor footprintColor = FootprintColor.White;
    public int artifactRequiredRoll = 3;
    public int tileIndex;
}
