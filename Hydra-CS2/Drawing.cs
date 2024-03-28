using ClickableTransparentOverlay;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hydra_CS2
{
    public partial class Renderer : Overlay
    {
        private float dynamicValue = 180f; // Start with a bluish hue
        private float colorChangeSpeed = 0.25f;
        private float circleRadius = 1.25f;

        void DrawCircle(float FOVSize)
        {
            ImGui.SetNextWindowSize(screenSize);
            ImGui.SetNextWindowPos(new Vector2(0, 0));
            ImGui.Begin("FOVCircle", ImGuiWindowFlags.NoDecoration
                | ImGuiWindowFlags.NoBackground
                | ImGuiWindowFlags.NoBringToFrontOnFocus
                | ImGuiWindowFlags.NoMove
                | ImGuiWindowFlags.NoInputs
                | ImGuiWindowFlags.NoCollapse
                | ImGuiWindowFlags.NoScrollbar
                | ImGuiWindowFlags.NoScrollWithMouse);

            // Get window width and height
            float width = screenSize.X;
            float height = screenSize.Y;

            // Calculate center point
            Vector2 center = new Vector2(width / 2, height / 2);

            // Calculate radius based on FOV size
            float radius = Math.Min(width, height) * config.FOVSize / 725;

            // Draw dynamically changing colored circle
            float radiansIncrement = MathF.PI / 180f;
            for (int i = 0; i < 360; i++)
            {
                float radians = i * radiansIncrement;
                float x = center.X + radius * MathF.Cos(radians);
                float y = center.Y + radius * MathF.Sin(radians);

                float[] rgb = ColorFromHSV(dynamicValue, 1, 1);

                ImGui.GetWindowDrawList().AddCircleFilled(new Vector2(x, y), circleRadius, ImGui.GetColorU32(new Vector4(rgb[0], rgb[1], rgb[2], 1)));
            }

            // Increment dynamic value for color variation
            dynamicValue += colorChangeSpeed;
            dynamicValue %= 360f;
        }

        // Function to convert HSV to RGB
        private float[] ColorFromHSV(float hue, float saturation, float value)
        {
            // Adjust hue to start from blue
            hue += 240; // Shift hue by 240 degrees (blue-green range)
            hue %= 360; // Wrap around if hue exceeds 360

            int hi = (int)MathF.Floor(hue / 60) % 6;
            float f = hue / 60f - (float)MathF.Floor(hue / 60);

            value *= 255f;
            int v = (int)value;
            int p = (int)(value * (1 - saturation));
            int q = (int)(value * (1 - f * saturation));
            int t = (int)(value * (1 - (1 - f) * saturation));

            int r, g, b;

            switch (hi)
            {
                case 0:
                    r = v; g = t; b = p; break;
                case 1:
                    r = q; g = v; b = p; break;
                case 2:
                    r = p; g = v; b = t; break;
                case 3:
                    r = p; g = q; b = v; break;
                case 4:
                    r = t; g = p; b = v; break;
                default:
                    r = v; g = p; b = q; break;
            }

            return new float[] { r / 255f, g / 255f, b / 255f };
        }

        void DrawCrosshair()
        {
            ImGui.SetNextWindowSize(screenSize);
            ImGui.SetNextWindowPos(new Vector2(0, 0));
            ImGui.Begin("Crosshair", ImGuiWindowFlags.NoDecoration
                | ImGuiWindowFlags.NoBackground
                | ImGuiWindowFlags.NoBringToFrontOnFocus
                | ImGuiWindowFlags.NoMove
                | ImGuiWindowFlags.NoInputs
                | ImGuiWindowFlags.NoCollapse
                | ImGuiWindowFlags.NoScrollbar
                | ImGuiWindowFlags.NoScrollWithMouse
                );
        }

        void DrawESPOverlay(Vector2 screenSize)
        {
            ImGui.SetNextWindowSize(screenSize);
            ImGui.SetNextWindowPos(new Vector2(0, 0));
            ImGui.Begin("ESP Box", ImGuiWindowFlags.NoDecoration
                | ImGuiWindowFlags.NoBackground
                | ImGuiWindowFlags.NoBringToFrontOnFocus
                | ImGuiWindowFlags.NoMove
                | ImGuiWindowFlags.NoInputs
                | ImGuiWindowFlags.NoCollapse
                | ImGuiWindowFlags.NoScrollbar
                | ImGuiWindowFlags.NoScrollWithMouse
            );
        }
    }
}
