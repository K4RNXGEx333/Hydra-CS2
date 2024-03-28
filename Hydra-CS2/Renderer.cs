using ClickableTransparentOverlay;
using ImGuiNET;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using System.Net.Mime;
using HydraNet;
using System.Runtime.Intrinsics;
using System.Text.RegularExpressions;
using Vortice.Mathematics;
using System;


namespace Hydra_CS2
{
    public partial class Renderer : Overlay
    {
        private const string CursorFilePath = @"C:\Users\k4rnx\source\repos\K4RNXGEx333\Hydra-CS2\Hydra-CS2\Resources\Normal Select.cur";
        private static IntPtr customCursor;

        public Vector2 screenSize;

        Config config = new Config();
        Colors colors = new Colors();

        private ConcurrentQueue<Entity> entities = new ConcurrentQueue<Entity>();
        private Entity localPlayer = new Entity();
        private readonly object entityLock = new object();

        ImDrawListPtr drawList;

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int key);


        protected override void Render()
        {
            ImGuiIOPtr io = ImGui.GetIO();
            screenSize.X = io.DisplaySize.X;
            screenSize.Y = io.DisplaySize.Y;

            ReplaceFont(@"D:\Hydra Projects\Hydra-Menu\Hydra-Menu\calibri.ttf", 18, FontGlyphRangeType.English);

            DrawESPOverlay(screenSize);
            //DrawSkeletonOverlay();
            drawList = ImGui.GetWindowDrawList();

            if (config.enableaimbot)
            {
                if (config.drawFOV)
                {
                    if (config.FOVCircle)
                    {
                        DrawCircle(config.FOVSize);
                    }
                }
            }

            if (config.enableVisuals)
            {
                foreach (var entity in entities)
                {
                    if (config.enemyOnly2 && entity.team == localPlayer.team)
                    {
                        continue;
                    }

                    if (config.enableBox)
                    {
                        DrawBox(entity);
                    }
                    if (config.enableSnapLines)
                    {
                        DrawLine(entity);
                    }
                    /*if (config.enableSkeleton)
                    {
                        DrawSkeleton(entity);
                    }*/

                    foreach (var info in config.selectedPlayerInfo)
                    {
                        switch (config.playerInfoSelection[info])
                        {
                            case "Name":
                                    DrawName(entity);
                                break;
                            case "Health":
                                    DrawHealth(entity);
                                break;
                            /*case "Distance":
                                DrawDistance(entity);
                                break;*/
                            default:
                                break;
                        }
                    }
                }
            }
            if (GetAsyncKeyState(0x2D) < 0)
            {
                config.show = !config.show;
                Thread.Sleep(100);
            }

            if (config.show)
            {
                var style = ImGuiNET.ImGui.GetStyle();

                style.Alpha = 1f;
                style.DisabledAlpha = 0.6000000238418579f;
                style.WindowPadding = new Vector2(8.0f, 8.0f);
                style.WindowRounding = 0.0f;
                style.WindowBorderSize = 0.0f;
                style.WindowMinSize = new Vector2(32.0f, 32.0f);
                style.WindowTitleAlign = new Vector2(0.5f, 0.5f);
                style.WindowMenuButtonPosition = ImGuiDir.Left;
                style.ChildRounding = 0.0f;
                style.ChildBorderSize = 0.1f;
                style.PopupRounding = 10.0f;
                style.PopupBorderSize = 1.0f;
                style.FramePadding = new Vector2(14.0f, 4.0f);
                style.FrameRounding = 0.0f;
                style.FrameBorderSize = 0.0f;
                style.ItemSpacing = new Vector2(5.5f, 10.0f);
                style.ItemInnerSpacing = new Vector2(4.0f, 4.0f);
                style.CellPadding = new Vector2(4.0f, 2.0f);
                style.IndentSpacing = 0.0f;
                style.ColumnsMinSpacing = 6.0f;
                style.ScrollbarSize = 15.0f;
                style.ScrollbarRounding = 20.0f;
                style.GrabMinSize = 20.0f;
                style.GrabRounding = 20.0f;
                style.TabRounding = 5.0f;
                style.TabBorderSize = 1.0f;
                style.TabMinWidthForCloseButton = 0.0f;
                style.ColorButtonPosition = ImGuiDir.Right;
                style.ButtonTextAlign = new Vector2(0.5f, 0.5f);
                style.SelectableTextAlign = new Vector2(0.0f, 0.0f);

                style.Colors[(int)ImGuiCol.Text] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(9.999899930335232e-07f, 9.999999974752427e-07f, 9.999944268201943e-07f, 0.1030042767524719f);
                style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(9.999999974752427e-07f, 9.999899930335232e-07f, 9.999899930335232e-07f, 0.5f);
                style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
                style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.0784313753247261f, 0.0784313753247261f, 0.0784313753247261f, 0.9399999976158142f);
                style.Colors[(int)ImGuiCol.Border] = new Vector4(1.0f, 0.9999899864196777f, 0.9999899864196777f, 1.0f);
                style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.1372549086809158f, 0.1372549086809158f, 0.1372549086809158f, 1.0f);
                style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.995708167552948f);
                style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.1764705926179886f, 0.1764705926179886f, 0.1764705926179886f, 0.4000000059604645f);
                style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.2156862765550613f, 0.2156862765550613f, 0.2156862765550613f, 0.6700000166893005f);
                style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(9.999999974752427e-07f, 9.999899930335232e-07f, 9.999899930335232e-07f, 1.0f);
                style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(9.999999974752427e-07f, 9.999899930335232e-07f, 9.999899930335232e-07f, 1.0f);
                style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.1372549086809158f, 0.1372549086809158f, 0.1372549086809158f, 0.6700000166893005f);
                style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.4978540539741516f, 0.4978490769863129f, 0.4978490769863129f, 0.1845493316650391f);
                style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.407843142747879f, 0.407843142747879f, 0.407843142747879f, 1.0f);
                style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(1.0f, 0.9999899864196777f, 0.9999899864196777f, 1.0f);
                style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(1.0f, 0.3803921639919281f, 0.3803921639919281f, 1.0f);
                style.Colors[(int)ImGuiCol.Button] = new Vector4(9.999999974752427e-07f, 9.999899930335232e-07f, 9.999899930335232e-07f, 0.0f);
                style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.1764705926179886f, 0.1764705926179886f, 0.1764705926179886f, 0.4000000059604645f);
                style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.2156862765550613f, 0.2156862765550613f, 0.2156862765550613f, 0.6705882549285889f);
                style.Colors[(int)ImGuiCol.Header] = new Vector4(0.2705882489681244f, 0.2705882489681244f, 0.2705882489681244f, 1.0f);
                style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.2705882489681244f, 0.2705882489681244f, 0.2705882489681244f, 1.0f);
                style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.3529411852359772f, 0.3529411852359772f, 0.3529411852359772f, 1.0f);
                style.Colors[(int)ImGuiCol.Separator] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                style.Colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                style.Colors[(int)ImGuiCol.SeparatorActive] = new Vector4(1.0f, 0.3294117748737335f, 0.3294117748737335f, 1.0f);
                style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(1.0f, 0.4862745106220245f, 0.4862745106220245f, 1.0f);
                style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(1.0f, 0.4862745106220245f, 0.4862745106220245f, 1.0f);
                style.Colors[(int)ImGuiCol.Tab] = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.2901960909366608f, 0.2901960909366608f, 0.2901960909366608f, 1.0f);
                style.Colors[(int)ImGuiCol.TabActive] = new Vector4(9.999999974752427e-07f, 9.999899930335232e-07f, 9.999899930335232e-07f, 1.0f);
                style.Colors[(int)ImGuiCol.TabUnfocused] = new Vector4(0.1716738343238831f, 0.04347105696797371f, 0.04347105696797371f, 0.0f);
                style.Colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4(0.4039215743541718f, 0.1529411822557449f, 0.1529411822557449f, 1.0f);
                style.Colors[(int)ImGuiCol.PlotLines] = new Vector4(0.6078431606292725f, 0.6078431606292725f, 0.6078431606292725f, 1.0f);
                style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                style.Colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.8980392217636108f, 0.0f, 0.0f, 1.0f);
                style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(0.364705890417099f, 0.0f, 0.0f, 1.0f);
                style.Colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(0.2705882489681244f, 0.2705882489681244f, 0.2705882489681244f, 1.0f);
                style.Colors[(int)ImGuiCol.TableBorderStrong] = new Vector4(0.1372549086809158f, 0.1372549086809158f, 0.1372549086809158f, 1.0f);
                style.Colors[(int)ImGuiCol.TableBorderLight] = new Vector4(0.1372549086809158f, 0.1372549086809158f, 0.1372549086809158f, 1.0f);
                style.Colors[(int)ImGuiCol.TableRowBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
                style.Colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1.0f, 1.0f, 1.0f, 0.05999999865889549f);
                style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.2627451121807098f, 0.6352941393852234f, 0.8784313797950745f, 0.4377682209014893f);
                style.Colors[(int)ImGuiCol.DragDropTarget] = new Vector4(0.4666666686534882f, 0.1843137294054031f, 0.1843137294054031f, 0.9656652212142944f);
                style.Colors[(int)ImGuiCol.NavHighlight] = new Vector4(0.2705882489681244f, 0.2705882489681244f, 0.2705882489681244f, 1.0f);
                style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.0f, 1.0f, 1.0f, 0.699999988079071f);
                style.Colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.800000011920929f, 0.800000011920929f, 0.800000011920929f, 0.2000000029802322f);
                style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.800000011920929f, 0.800000011920929f, 0.800000011920929f, 0.3499999940395355f);


                ImGui.PushStyleColor(ImGuiCol.Text, colors.textColor);
                ImGui.PushStyleColor(ImGuiCol.Border, colors.borderColor);
                ImGui.PushStyleColor(ImGuiCol.WindowBg, colors.bgColor);
                ImGui.PushStyleColor(ImGuiCol.Border, colors.borderColor);
                if (ImGui.Begin("Hydra", ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize))
                {
                    ImGui.SetWindowSize(new Vector2(1000, 605));
                    ImGui.PushStyleVar(ImGuiStyleVar.ChildBorderSize, 0.1f);
                    ImGui.BeginChild("Menu Buttons", new Vector2(157, 583), ImGuiChildFlags.Border);

                    if (ImGui.Button("Aimbot", new Vector2(140, 75)))
                    {
                        config.showAimbot = true;
                        config.showVisuals = false;
                        config.showMisc = false;
                        config.showColors = false;
                    }

                    if (ImGui.Button("Visuals", new Vector2(140, 75)))
                    {
                        config.showVisuals = true;
                        config.showAimbot = false;
                        config.showMisc = false;
                        config.showColors = false;
                    }

                    if (ImGui.Button("Misc", new Vector2(140, 75)))
                    {
                        config.showMisc = true;
                        config.showVisuals = false;
                        config.showAimbot = false;
                        config.showColors = false;
                    }

                    if (ImGui.Button("Colors", new Vector2(140, 75)))
                    {
                        config.showColors = true;
                        config.showAimbot = false;
                        config.showVisuals = false;
                        config.showMisc = false;
                    }

                    ImGui.EndChild();

                    if (config.showAimbot)
                    {
                        ImGui.SetCursorPos(new Vector2(180, 8));
                        ImGui.BeginChild("General Aimbot", new Vector2(230, 583), ImGuiChildFlags.Border);

                        ImGui.PushStyleColor(ImGuiCol.Text, colors.textColor);
                        HydraGui.TextDisabled("General");
                        HydraGui.Switch("Enable Aimbot", ref config.enableaimbot);

                        ImGui.Spacing();
                        ImGui.Spacing();
                        HydraGui.Switch("Visible Only", ref config.visibleOnly);

                        ImGui.Spacing();
                        ImGui.Spacing();
                        ImGui.Text("Aimbot Type");
                        ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, 1.0f);
                        ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 10.0f);
                        if (ImGui.BeginCombo("##AimbotType", GetSelectedAimbotType(), ImGuiComboFlags.NoArrowButton))
                        {
                            for (int i = 0; i < config.aimbotTypeInfoSelection.Length; i++)
                            {
                                bool isSelected = config.selectedAimbotType.Contains(i);
                                if (ImGui.Selectable(config.aimbotTypeInfoSelection[i], isSelected))
                                {
                                    if (isSelected)
                                    {
                                        config.selectedAimbotType.Remove(i);
                                    }
                                    else
                                    {
                                        config.selectedAimbotType.Add(i);
                                    }
                                }

                                if (isSelected)
                                    ImGui.SetItemDefaultFocus();
                            }
                            ImGui.EndCombo();
                        }
                        ImGui.Spacing();
                        HydraGui.Switch("Enemy Only", ref config.enemyOnly1);

                        ImGui.Spacing();
                        ImGui.Spacing();
                        HydraGui.Switch("Recoil Control", ref config.recoilControl);

                        ImGui.Spacing();
                        ImGui.Spacing();
                        HydraGui.Switch("Draw FOV", ref config.drawFOV);

                        ImGui.Spacing();
                        ImGui.Spacing();
                        HydraGui.Switch("Draw Crosshair", ref config.drawcrosshair);

                        HydraGui.SliderFloat2("Horizontal Speed", ref config.horizontalSmoothness, 1, 100);
                        ImGui.Spacing();
                        HydraGui.SliderFloat2("Vertical Speed", ref config.verticalSmoothness, 1, 100);


                        ImGui.EndChild();

                        ImGui.SetCursorPos(new Vector2(425, 8));
                        ImGui.BeginChild("Aimbot Settings", new Vector2(230, 583), ImGuiChildFlags.Border);
                        HydraGui.SliderFloat("FOV", ref config.FOVSize, 10, 200);
                        ImGui.Spacing();
                        ImGui.Spacing();
                        ImGui.Spacing();
                        ImGui.Spacing();
                        HydraGui.SliderFloat("Crosshair", ref config.CrosshairSize, 10, 50);
                        ImGui.EndChild();
                    }

                    string GetSelectedAimbotType()
                    {
                        return string.Join(", ", config.selectedAimbotType.Select(index => config.aimbotTypeInfoSelection[index]));
                    }

                    if (config.showVisuals)
                    {
                        ;
                        ImGui.SetCursorPos(new Vector2(180, 8));
                        ImGui.BeginChild("Visuals Settings", new Vector2(230, 583), ImGuiChildFlags.Border);

                        HydraGui.TextDisabled("General");
                        HydraGui.Switch("Enable Visuals", ref config.enableVisuals);
                        ImGui.Spacing();
                        ImGui.Spacing();
                        HydraGui.Switch("Enemy Only", ref config.enemyOnly2);
                        ImGui.Spacing();
                        ImGui.Spacing();
                        ImGui.Spacing();
                        ImGui.Spacing();
                        HydraGui.TextDisabled("Character");
                        ImGui.Spacing();
                        ImGui.Spacing();
                        HydraGui.Switch("Box", ref config.enableBox);
                        ImGui.Spacing();
                        ImGui.Spacing();
                        HydraGui.Switch("Box Outline", ref config.boxOutline);
                        ImGui.Spacing();
                        ImGui.Spacing();
                        HydraGui.Switch("Skeleton", ref config.enableSkeleton);
                        ImGui.Text("Player Info");
                        ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, 1.0f);
                        ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 10.0f);
                        if (ImGui.BeginCombo("##PlyaerInfo", GetSelectedPlayerInfo(), ImGuiComboFlags.NoArrowButton))
                        {
                            for (int i = 0; i < config.playerInfoSelection.Length; i++)
                            {
                                bool isSelected = config.selectedPlayerInfo.Contains(i);
                                if (ImGui.Selectable(config.playerInfoSelection[i], isSelected))
                                {
                                    if (isSelected)
                                    {
                                        config.selectedPlayerInfo.Remove(i);
                                    }
                                    else
                                    {
                                        config.selectedPlayerInfo.Add(i);
                                    }
                                }

                                if (isSelected)
                                    ImGui.SetItemDefaultFocus();
                            }
                            ImGui.EndCombo();
                        }
                        ImGui.EndChild();
                    }

                    string GetSelectedPlayerInfo()
                    {
                        return string.Join(", ", config.selectedPlayerInfo.Select(index => config.playerInfoSelection[index]));
                    }
                    if (config.showMisc)
                    {
                        ImGui.SetCursorPos(new Vector2(180, 8));
                        ImGui.BeginChild("Visuals Colors", new Vector2(230, 583), ImGuiChildFlags.Border);
                        HydraGui.TextDisabled("Exploits");

                        HydraGui.Switch("FOV Override", ref config.enableFOVOverride);
                        ImGui.Spacing();
                        ImGui.Spacing();
                        HydraGui.SliderFloat("FOV", ref config.FOVOverride, 10, 200);

                        ImGui.EndChild();
                    }
                    if (config.showColors)
                    {
                        ImGui.SetCursorPos(new Vector2(180, 8));
                        ImGui.BeginChild("Visuals Colors", new Vector2(230, 583), ImGuiChildFlags.Border);
                        HydraGui.TextDisabled("Visuals Colors");

                        ImGui.Text("Enemy Visible");
                        ImGui.SetCursorPos(new Vector2(190, 35));
                        ImGui.ColorEdit4("##Enemy Visible", ref colors.enemyVisible, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.PickerMask | ImGuiColorEditFlags.InputRGB | ImGuiColorEditFlags.NoBorder | ImGuiColorEditFlags.NoTooltip);
                        ImGui.Spacing();
                        ImGui.Spacing();

                        ImGui.Text("Enemy Not Visible");
                        ImGui.SetCursorPos(new Vector2(190, 85));
                        ImGui.ColorEdit4("##Enemy Not Visible", ref colors.enemyNotVisible, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.PickerMask | ImGuiColorEditFlags.InputRGB | ImGuiColorEditFlags.NoBorder | ImGuiColorEditFlags.NoTooltip);
                        ImGui.Spacing();
                        ImGui.Spacing();

                        ImGui.Text("Team Visible");
                        ImGui.SetCursorPos(new Vector2(190, 135));
                        ImGui.ColorEdit4("##Team Visible", ref colors.teamVisible, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.PickerMask | ImGuiColorEditFlags.InputRGB | ImGuiColorEditFlags.NoBorder | ImGuiColorEditFlags.NoTooltip);
                        ImGui.Spacing();
                        ImGui.Spacing();

                        ImGui.Text("Team Not Visible");
                        ImGui.SetCursorPos(new Vector2(190, 185));
                        ImGui.ColorEdit4("##Team Not Visible", ref colors.teamNotVisible, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.PickerMask | ImGuiColorEditFlags.InputRGB | ImGuiColorEditFlags.NoBorder | ImGuiColorEditFlags.NoTooltip);
                        ImGui.EndChild();


                        ImGui.SetCursorPos(new Vector2(425, 8));
                        ImGui.BeginChild("GUI Colors", new Vector2(230, 583), ImGuiChildFlags.Border);
                        HydraGui.TextDisabled("GUI Colors");

                        //ImGui.Text("UI Button");
                        //ImGui.ColorEdit4("UI Button")

                        ImGui.Text("UI Border");
                        ImGui.SetCursorPos(new Vector2(190, 35));
                        ImGui.ColorEdit4("##UI Border", ref colors.borderColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.PickerMask | ImGuiColorEditFlags.InputRGB | ImGuiColorEditFlags.NoBorder | ImGuiColorEditFlags.NoTooltip);
                        ImGui.Spacing();
                        ImGui.Spacing();

                        ImGui.Text("UI Text");
                        ImGui.SetCursorPos(new Vector2(190, 90));
                        ImGui.ColorEdit4("##UI Text", ref colors.textColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.PickerMask | ImGuiColorEditFlags.InputRGB | ImGuiColorEditFlags.NoBorder | ImGuiColorEditFlags.NoTooltip);
                        ImGui.Spacing();
                        ImGui.Spacing();

                        /*ImGui.Text("UI BackGround");
                        //ImGui.SetCursorPos(new Vector2 (45, 45));
                        ImGui.ColorEdit4("##UI BackGround", ref colors.bgColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.PickerMask | ImGuiColorEditFlags.InputRGB | ImGuiColorEditFlags.NoBorder | ImGuiColorEditFlags.NoTooltip);
                        ImGui.Spacing();
                        ImGui.Spacing();*/

                        ImGui.EndChild();
                    }
                    ImGui.End();
                }
            }
        }
        public void DrawHealth(Entity entity)
        {
            if (entity.team == localPlayer.team)
                return;

            float entityHeight = entity.position2D.Y - entity.viewPosition2D.Y;
            float boxLeft = entity.viewPosition2D.X - entityHeight / 3;
            float boxRight = entity.position2D.X + entityHeight / 3;

            float barPercentWidth = 0.05f;
            float barPixelWidth = barPercentWidth * (boxRight - boxLeft);

            float barHeight = entityHeight * (entity.health / 100f);

            Vector2 barTop = new Vector2(boxLeft - barPixelWidth, entity.position2D.Y - barHeight);
            Vector2 barBottom = new Vector2(boxLeft, entity.position2D.Y);

            Vector4 barColor = new Vector4(0, 1, 0, 1);

            // Draw health for enemy team members only
            drawList.AddRectFilled(barTop, barBottom, ImGui.ColorConvertFloat4ToU32(barColor));
        }

        private void DrawBox(Entity entity)
        {
            float entityHeight = entity.position2D.Y - entity.viewPosition2D.Y;

            Vector2 rectTop = new Vector2(entity.viewPosition2D.X - entityHeight / 2, entity.viewPosition2D.Y);
            Vector2 rectBottom = new Vector2(entity.position2D.X + entityHeight / 2, entity.position2D.Y);

            Vector4 boxColor = localPlayer.team == entity.team ? colors.teamVisible : colors.enemyVisible;

            if (config.visualsVisibilityCheck)
            {
                if (entity.spotted)
                    boxColor = localPlayer.team == entity.team ? colors.teamVisible : colors.enemyVisible;
                else
                    boxColor = localPlayer.team == entity.team ? colors.teamNotVisible : colors.enemyNotVisible;
            }

            drawList.AddRect(rectTop, rectBottom, ImGui.ColorConvertFloat4ToU32(boxColor));

            if (config.boxOutline)
            {
                Vector2 rectTopOutline = new Vector2(rectTop.X - 1, rectTop.Y - 1);
                Vector2 rectBottomOutline = new Vector2(rectBottom.X + 1, rectBottom.Y + 1);
                drawList.AddRect(rectTopOutline, rectBottomOutline, ImGui.ColorConvertFloat4ToU32(colors.outlineColor));
            }
        }

        private void ESPRectangleEdges(Entity entity)
        {
            if (entity.team == localPlayer.team)
                return;

            float entityHeight = entity.position2D.Y - entity.viewPosition2D.Y;

            Vector2 rectTop = new Vector2(entity.viewPosition2D.X - entityHeight / 3, entity.viewPosition2D.Y);
            Vector2 rectBottom = new Vector2(entity.position2D.X + entityHeight / 3, entity.position2D.Y);

            Vector4 boxColor = colors.enemyVisible;

            drawList.AddRect(rectTop, rectBottom, ImGui.ColorConvertFloat4ToU32(boxColor));

            uint boxFillColor = ImGui.ColorConvertFloat4ToU32(boxColor);

            // Draw box for enemy team members only
            drawList.AddRect(rectTop, rectBottom, boxFillColor);

            if (config.enableSnapLines)
            {
                Vector2 rectTopOutline = new Vector2(rectTop.X - 2, rectTop.Y - 2);
                Vector2 rectBottomOutline = new Vector2(rectBottom.X + 2, rectBottom.Y + 2);
                drawList.AddRect(rectTopOutline, rectBottomOutline, ImGui.ColorConvertFloat4ToU32(colors.outlineColor));
            }
        }

        private void DrawLine(Entity entity)
        {
            if (entity.team == localPlayer.team)
                return;

            Vector4 lineColor = colors.enemyVisible;

            drawList.AddLine(new Vector2(screenSize.X / 3, screenSize.Y / 3), entity.position2D, ImGui.ColorConvertFloat4ToU32(lineColor));
        }

        private void DrawName(Entity entity)
        {
            Vector2 textLocation = new Vector2(entity.viewPosition2D.X, entity.viewPosition2D.Y - 20);
            drawList.AddText(textLocation, ImGui.ColorConvertFloat4ToU32(colors.nameColor), $"{entity.name}");
        }
        /*void DrawSkeleton(Entity entity)
        {
            Vector4 SkeletonColor = colors.skeletonColor;

            uint uintColor = ImGui.ColorConvertFloat4ToU32(SkeletonColor);

            float config.boneThickness = config.boneThickness / entity.distance;

            drawList.AddLine(entity.bones2D[1], entity.bones2D[2], uintColor, config.boneThickness);
            drawList.AddLine(entity.bones2D[1], entity.bones2D[2], uintColor, config.boneThickness);
            drawList.AddLine(entity.bones2D[1], entity.bones2D[6], uintColor, config.boneThickness);
            drawList.AddLine(entity.bones2D[3], entity.bones2D[4], uintColor, config.boneThickness);
            drawList.AddLine(entity.bones2D[6], entity.bones2D[7], uintColor, config.boneThickness);
            drawList.AddLine(entity.bones2D[4], entity.bones2D[5], uintColor, config.boneThickness);
            drawList.AddLine(entity.bones2D[7], entity.bones2D[8], uintColor, config.boneThickness);
            drawList.AddLine(entity.bones2D[1], entity.bones2D[0], uintColor, config.boneThickness);
            drawList.AddLine(entity.bones2D[0], entity.bones2D[9], uintColor, config.boneThickness);
            drawList.AddLine(entity.bones2D[0], entity.bones2D[11], uintColor, config.boneThickness);
            drawList.AddLine(entity.bones2D[9], entity.bones2D[10], uintColor, config.boneThickness);
            drawList.AddLine(entity.bones2D[11], entity.bones2D[12], uintColor, config.boneThickness);
        }
        private void DrawDistance(Entity entity)
        {
            Vector2 textLocation = new Vector2(entity.viewPosition2D.X, entity.viewPosition2D.Y + 20);
            drawList.AddText(textLocation, ImGui.ColorConvertFloat4ToU32(colors.nameColor), $"{entity.distance}");
        }*/
        bool EntityOnScreen(Entity entity)
        {
            if (entity.position2D.X > 0 && entity.position2D.X < config.screenSize.X && entity.position2D.Y > 0 && entity.position2D.Y < config.screenSize.Y)
            {
                return true;
            }
            return false;
        }
        public void UpdateEntities(IEnumerable<Entity> newEntities)
        {
            entities = new ConcurrentQueue<Entity>(newEntities);
        }

        public void UpdateLocalPlayer(Entity newEntity)
        {
            lock (entityLock)
            {
                localPlayer = newEntity;
            }
        }

        public Entity GetLocalPlayer()
        {
            lock (entityLock)
            {
                return localPlayer;
            }
        }
    }
}
