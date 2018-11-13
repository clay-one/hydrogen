using System;

namespace Hydrogen.General.Geo
{
	public static class GoogleMapsUtil
	{
		public const int DefaultWorldPx = 256;

		public static int GetBoundsZoomLevel(LatLngBounds bounds, int widthPx, int heightPx)
		{
			var latFraction = (LatRad(bounds.NorthLat) - LatRad(bounds.SouthLat)) / Math.PI;

			var lngDiff = bounds.EastLng - bounds.WestLng;
			var lngFraction = ((lngDiff < 0) ? (lngDiff + 360) : lngDiff) / 360;

			var latZoom = GetFractionZoomLevel(heightPx, DefaultWorldPx, latFraction);
			var lngZoom = GetFractionZoomLevel(widthPx, DefaultWorldPx, lngFraction);

			return Math.Min(latZoom, Math.Min(lngZoom, 21));
		}

		public static int GetFractionZoomLevel(int mapPx, int worldPx, double fraction)
		{
			return (int)Math.Floor(Math.Log((double)mapPx / worldPx / fraction) / Math.Log(2));
		}

		#region Private helpers

		private static double LatRad(double lat)
		{
			var sin = Math.Sin(lat*Math.PI/180);
			var radX2 = Math.Log((1 + sin)/(1 - sin))/2;
			return Math.Max(Math.Min(radX2, Math.PI), -Math.PI)/2;
		}

		#endregion
	}
}