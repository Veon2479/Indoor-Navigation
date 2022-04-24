using System;
using System.Drawing;

namespace Server
{
    public static class MapInfo
    {
        /// <summary>
        /// Coordinate X of point 1
        /// </summary>
        public static int PointX1;
        /// <summary>
        /// Coordinate Y of point 1
        /// </summary>
        public static int PointY1;
        /// <summary>
        /// Coordinate X of point 2
        /// </summary>
        public static int PointX2;
        /// <summary>
        /// Coordinate Y of point 2
        /// </summary>
        public static int PointY2;
        /// <summary>
        /// The ratio of the size in meters to the size in pixels
        /// </summary>
        public static double SizeCoefficient = 1;
        /// <summary>
        /// Real length of marked area
        /// </summary>
        public static double RealLength;
        internal static void SetRealLength(double value)
        {
            isMapChanged = true;
            RealLength = value;
            SizeCoefficient = RealLength / Math.Abs(PointX2 - PointX1);
            RealWidth = SizeCoefficient * Math.Abs(PointY2 - PointY1);
        }
        /// <summary>
        /// Real width of marked area
        /// </summary>
        public static double RealWidth;
        /// <summary>
        /// Azimuth
        /// </summary>
        public static double Azimuth = 0;

        /// <summary>
        /// Bitmap with room map
        /// </summary>
        public static Bitmap bitmap = null;
        internal static bool isDownloadMap = false;
        internal static bool isMapChanged = false;

        internal static void SetX1(int value)
        {
            PointX1 = value;
            RealLength = SizeCoefficient * Math.Abs(PointX2 - PointX1);
        }
        internal static void SetY1(int value)
        {
            PointY1 = value;
            RealWidth = SizeCoefficient * Math.Abs(PointY2 - PointY1);
        }
        internal static void SetX2(int value)
        {
            PointX2 = value;
            RealLength = SizeCoefficient * Math.Abs(PointX2 - PointX1);
        }
        internal static void SetY2(int value)
        {
            PointY2 = value;
            RealWidth = SizeCoefficient * Math.Abs(PointY2 - PointY1);
        }

        internal static void SetDefoult()
        {
            SizeCoefficient = 1;
            RealLength = SizeCoefficient * Math.Abs(PointX2 - PointX1);
            RealWidth = SizeCoefficient * Math.Abs(PointY2 - PointY1);
        }
    }
}
