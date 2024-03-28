using ImGuiNET;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hydra_CS2
{
    public class Config
    {
        public bool show = true;
        #region Menu
        public bool showAimbot = true;
        public bool showVisuals = false;
        public bool showMisc = false;
        public bool showColors = false;
        #endregion

        #region Aimbot
        public bool enableaimbot = true;
        public float horizontalSmoothness = 2;
        public float verticalSmoothness = 2;
        public bool enemyOnly1 = true;
        public bool recoilControl = false;
        public bool visibleOnly = true;
        public bool norecoil = false;
        public bool drawFOV = true;
        public float FOVSize = 90;
        public float CrosshairSize = 15;
        public int selectedAimBone = 0;
        public string[] aimBoneSelection = { "Head", "Neck", "Waist" };
        public bool drawcrosshair = true;
        public bool FOVCircle = true;
        public List<int> selectedAimbotType = new List<int>();
        public string[] aimbotTypeInfoSelection = { "Vectored", "Silent" };
        #endregion

        #region Visuals
        public bool enableVisuals = true;
        public bool enemyOnly2 = true;
        public bool enableSnapLines = false;
        public bool visualsVisibilityCheck = true;
        public bool enableBox = true;
        //public bool enableHealth = true;
        //public bool ESPRectangleEdges = true;
        public bool boxOutline = true;
        public bool enableSkeleton = true;
        public bool enableName = true;
        public int boneThickness = 4;
        public List<int> selectedPlayerInfo = new List<int>();
        public string[] playerInfoSelection = { "Name", "Health", "Distance" };
        #endregion

        #region Misc
        public bool enableFOVOverride = false;
        public float FOVOverride = 25;
        #endregion

        public Vector2 screenSize = new Vector2(1920, 1080);
        public Vector2 overlaySize = new Vector2(1920, 1080);
        public Vector4 FOVColor = new Vector4(1, 1, 1, 1);
        public Vector4 CrosshairColor = new Vector4(1, 1, 1, 1);
    }

    public class Colors
    {
        // ESP Colors
        public Vector4 enemyVisible = new Vector4(1, 0, 0, 1); // Red
        public Vector4 enemyNotVisible = new Vector4(1, 1, 1, 1); // White
        public Vector4 teamVisible = new Vector4(0, 0, 1, 1); // Blue
        public Vector4 teamNotVisible = new Vector4(1, 1, 1, 1); // White
        public Vector4 nameColor = new Vector4(1, 1, 1, 1); // White
        public Vector4 skeletonColor = new Vector4(1, 1, 1, 1); // White
        public Vector4 healthColor = new Vector4(1, 1, 1, 1); // Green
        public Vector4 outlineColor = new Vector4(0, 0, 0, 1); // Black

        // Gui Colors
        public Vector4 borderColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f); // White
        public Vector4 textColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f); // White
        public Vector4 bgColor = new Vector4(0.0f, 0.0f, 0.0f, 1.0f); // Black
    }
}
