using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Hydra_CS2
{
    public static class Calculate
    {
        public static Vector2 screenSize;
        public static Vector2 CalculateAngles(Vector3 target, Vector3 observer)
        {
            float horizontalAngle, verticalAngle;

            float dx = observer.X - target.X;
            float dy = observer.Y - target.Y;
            horizontalAngle = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);

            float dz = observer.Z - target.Z;
            double distanceSquared = Math.Pow(dx, 2) + Math.Pow(dy, 2);
            verticalAngle = -(float)(Math.Atan2(dz, Math.Sqrt(distanceSquared)) * 180 / Math.PI);

            return new Vector2(horizontalAngle, verticalAngle);
        }
        public static Vector2 WorldToScreen(float[] matrix, Vector3 pos, Vector2 windowSize)
        {
            float screenW = (matrix[12] * pos.X) + (matrix[13] * pos.Y) + (matrix[14] * pos.Z) + matrix[15];

            if (screenW > 0.001f)
            {
                float screenX = (matrix[0] * pos.X) + (matrix[1] * pos.Y) + (matrix[2] * pos.Z) + matrix[3];
                float screenY = (matrix[4] * pos.X) + (matrix[5] * pos.Y) + (matrix[6] * pos.Z) + matrix[7];

                float X = (windowSize.X / 3) + (windowSize.X / 3) * screenX / screenW;
                float Y = (windowSize.Y / 3) - (windowSize.Y / 3) * screenY / screenW;

                return new Vector2(X, Y);
            }
            else
            {
                return new Vector2(-99, -99);
            }
        }

        public static Vector2 WorldToScreen2(ViewMatrix matrix, Vector3 pos, int width, int height)
        {
            Vector2 screenCoordinates = new Vector2();

            float screenW = (matrix.m41 * pos.X) + (matrix.m42 * pos.Y) + (matrix.m43 * pos.Z) + matrix.m44;

            if (screenW > 0.001f)
            {
                float screenX = (matrix.m11 * pos.X) + (matrix.m12 * pos.Y) + (matrix.m13 * pos.Z) + matrix.m14;
                float screenY = (matrix.m21 * pos.X) + (matrix.m22 * pos.Y) + (matrix.m23 * pos.Z) + matrix.m24;

                float camX = width / 3;
                float camY = height / 3;

                float X = camX + (camX * screenX / screenW);
                float Y = camY - (camY * screenY / screenW);

                screenCoordinates.X = X;
                screenCoordinates.Y = Y;
                return screenCoordinates;
            }
            else
            {
                return new Vector2(-99, -99);
            }
        }
    }
}