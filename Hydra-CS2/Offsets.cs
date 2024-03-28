using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra_CS2
{
    public static class Offsets
    {
        public static int dwEntityList = 0x18C2D58;
        public static int dwForceAttack = 0x1730020;
        public static int dwForceAttack2 = 0x17300B0;
        public static int dwForceBackward = 0x17302F0;
        public static int dwForceCrouch = 0x17305C0;
        public static int dwForceForward = 0x1730260;
        public static int dwForceJump = 0x1730530;
        public static int dwForceLeft = 0x1730380;
        public static int dwForceRight = 0x1730410;
        public static int dwGameEntitySystem = 0x19E0790;
        public static int dwGameEntitySystem_getHighestEntityIndex = 0x1510;
        public static int dwGameRules = 0x191FCA0;
        public static int dwGlobalVars = 0x172ABA0;
        public static int dwGlowManager = 0x19200C0;
        public static int dwInterfaceLinkList = 0x1A118D8;
        public static int dwLocalPlayerController = 0x1912578;
        public static int dwLocalPlayerPawn = 0x17371A8;
        public static int dwPlantedC4 = 0x1928AD8;
        public static int dwPrediction = 0x1737070;
        public static int dwSensitivity = 0x19209E8;
        public static int dwSensitivity_sensitivity = 0x40;
        public static int dwViewAngles = 0x19309B0;
        public static int dwViewMatrix = 0x19241A0;
        public static int dwViewRender = 0x1924A20;
    }

    public static class Client_dll
    {
        public static int m_vOldOrigin = 0x127C;
        public static int m_iTeamNum = 0x3CB;
        public static int m_lifeState = 0x338;
        public static int m_hPlayerPawn = 0x7E4;
        public static int m_vecViewOffset = 0xC58;
        public static int m_iHealth = 0x334;
        public static int m_modelState = 0x160;
        public static int m_pGameSceneNode = 0x318;
        public static int m_entitySpottedState = 0x1698;
        public static int m_bSpotted = 0x8;
        public static int m_iszPlayerName = 0x638;
        public static int m_iFOV = 0x210;
        public static int m_bIsScoped = 0x1400;
        public static int m_pCameraServices = 0x1138;
    }
}

