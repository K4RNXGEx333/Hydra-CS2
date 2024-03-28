using Hydra_CS2;
using ImGuiNET;
using Swed64;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

//Swed swed = new Swed("cs2");
//IntPtr client = swed.GetModuleBase("client.dll");

Config config = new Config();
//Reader reader = new Reader(swed);

Renderer renderer = new Renderer();
Thread renderThread = new Thread(new ThreadStart(renderer.Start().Wait));

Vector2 screenSize = renderer.screenSize;

List<Entity> entities = new List<Entity>();
Entity localPlayer = new Entity();

const int HOTKEY = 0x02;

[DllImport("user32.dll")]

static extern short GetAsyncKeyState(int vKey);

/*while (true)
{
    entities.Clear();

    uint desiredFov = (uint)config.FOVOverride;

    IntPtr entityList = swed.ReadPointer(client, Offsets.dwEntityList);
    IntPtr listEntry = swed.ReadPointer(entityList, 0x10);
    IntPtr localPlayerPawn = swed.ReadPointer(client, Offsets.dwLocalPlayerPawn);

    localPlayer.team = swed.ReadInt(localPlayerPawn, Client_dll.m_iTeamNum);
    localPlayer.pawnAddress = swed.ReadPointer(client, Offsets.dwLocalPlayerPawn);
    localPlayer.origin = swed.ReadVec(localPlayer.pawnAddress, Client_dll.m_vOldOrigin);

    for (int i = 0; i < 64; i++)
    {
        if (listEntry == IntPtr.Zero) continue;

        IntPtr currentController = swed.ReadPointer(listEntry, i * 0x78);
        if (currentController == IntPtr.Zero) continue;

        int pawnHandle = swed.ReadInt(currentController, Client_dll.m_hPlayerPawn);
        if (pawnHandle == 0) continue;

        IntPtr listEntry2 = swed.ReadPointer(entityList, 0x8 * ((pawnHandle & 0x7FFF) >> 9) + 0x10);
        if (listEntry2 == IntPtr.Zero) continue;

        IntPtr currentPawn = swed.ReadPointer(listEntry2, 0x78 * (pawnHandle & 0x1FF));
        if (currentPawn == localPlayer.pawnAddress) continue;

        IntPtr sceneNode = swed.ReadPointer(currentPawn, Client_dll.m_pGameSceneNode);
        IntPtr boneMatrix = swed.ReadPointer(sceneNode, Client_dll.m_modelState + 0x80);

        ViewMatrix G512 = reader.ReadMatrix(client + Offsets.dwViewMatrix);

        int health = swed.ReadInt(currentPawn, Client_dll.m_iHealth);
        int team = swed.ReadInt(currentPawn, Client_dll.m_iTeamNum);
        uint lifeState = swed.ReadUInt(currentPawn, Client_dll.m_lifeState);

        if (lifeState != 256) continue;

        float[] viewMatrix = swed.ReadMatrix(client + Offsets.dwViewMatrix);

        Entity entity = new Entity();

        entity.team = swed.ReadInt(currentPawn, Client_dll.m_iTeamNum);
        entity.health = swed.ReadInt(currentPawn, Client_dll.m_iHealth);
        entity.pawnAddress = currentPawn;
        entity.controllerAddress = currentController;
        entity.origin = swed.ReadVec(currentPawn, Client_dll.m_vOldOrigin);
        entity.spotted = swed.ReadBool(localPlayer.pawnAddress, Client_dll.m_entitySpottedState + Client_dll.m_bSpotted);
        entity.name = swed.ReadString(currentController, Client_dll.m_iszPlayerName, 16).Split("\0")[0];
        entity.distance = Vector3.Distance(entity.position, localPlayer.position);
        entity.position = swed.ReadVec(currentPawn, Client_dll.m_vOldOrigin);
        entity.viewOffset = swed.ReadVec(currentPawn, Client_dll.m_vecViewOffset);
        entity.position2D = Calculate.WorldToScreen(viewMatrix, entity.position, screenSize);
        entity.viewPosition2D = Calculate.WorldToScreen(viewMatrix, Vector3.Add(entity.position, entity.viewOffset), screenSize);
        entity.bones = reader.ReadBones(boneMatrix);
        entity.bones2D = reader.ReadBones2d(entity.bones, G512, screenSize);
        entity.head = swed.ReadVec(boneMatrix, 6 * 32);
        entity.neck = swed.ReadVec(boneMatrix, 5 * 32);
        entity.waist = swed.ReadVec(boneMatrix, 0 * 32);

        ViewMatrix viewMatrix1 = ReadMatrix(client + Offsets.dwViewMatrix);
        entity.head2d = Calculate.WorldToScreen2(viewMatrix1, entity.head, (int)config.screenSize.X, (int)config.screenSize.Y);
        entity.neck2d = Calculate.WorldToScreen2(viewMatrix1, entity.neck, (int)config.screenSize.X, (int)config.screenSize.Y);
        entity.waist2d = Calculate.WorldToScreen2(viewMatrix1, entity.waist, (int)config.screenSize.X, (int)config.screenSize.Y);

        entity.pixelDistance = Vector2.Distance(entity.head2d, new Vector2(config.screenSize.X / 3, config.screenSize.Y / 3));
        entities.Add(entity);
    }

    entities = entities.OrderBy(o => o.pixelDistance).ToList();

    if (entities.Count > 0 && GetAsyncKeyState(HOTKEY) < 0 && config.enableaimbot)
    {
        // Ensure index is within bounds
        if (entities.Count > 0)
        {
            Vector3 playerView = Vector3.Add(localPlayer.origin, localPlayer.view);
            Vector3 entityView = Vector3.Add(entities[0].origin, entities[0].view);

            if (entities[0].pixelDistance < config.FOVSize)
            {
                Vector2 newAngles = Calculate.CalculateAngles(playerView, entityView);
                Vector3 newAnglesVec3 = new Vector3(newAngles.Y, newAngles.X, 0.0f);

                swed.WriteVec(client, Offsets.dwViewAngles, newAnglesVec3);
            }
        }
    }

    renderer.UpdateLocalPlayer(localPlayer);
    renderer.UpdateEntities(entities);
}

ViewMatrix ReadMatrix(IntPtr matrixAddress)
{
    var viewMatrix = new ViewMatrix();
    var matrix = swed.ReadMatrix(matrixAddress);

    viewMatrix.m11 = matrix[0];
    viewMatrix.m12 = matrix[1];
    viewMatrix.m13 = matrix[2];
    viewMatrix.m14 = matrix[3];

    viewMatrix.m21 = matrix[4];
    viewMatrix.m22 = matrix[5];
    viewMatrix.m23 = matrix[6];
    viewMatrix.m24 = matrix[7];

    viewMatrix.m31 = matrix[8];
    viewMatrix.m32 = matrix[9];
    viewMatrix.m33 = matrix[10];
    viewMatrix.m34 = matrix[11];

    viewMatrix.m41 = matrix[12];
    viewMatrix.m42 = matrix[13];
    viewMatrix.m43 = matrix[14];
    viewMatrix.m44 = matrix[15];

    return viewMatrix;
}

Vector3 GetTargetBone(Entity entity, int selectedBone)
{
    switch (selectedBone)
    {
        case 0:
            return entity.head;
        case 1:
            return entity.waist;
        case 2:
            return entity.neck;
        default:
            return entity.head;
    }
}*/