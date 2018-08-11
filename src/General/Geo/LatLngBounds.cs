using System;
using System.Collections.Generic;

namespace hydrogen.General.Geo
{
    public class LatLngBounds
	{
		public LatLng NorthEast { get; set; }
		public LatLng SouthWest { get; set; }

		public double NorthLat
		{
			get { return NorthEast.Lat; }
			set { NorthEast.Lat = value; }
		}

		public double SouthLat
		{
			get { return SouthWest.Lat; }
			set { SouthWest.Lat = value; }
		}

		public double EastLng
		{
			get { return NorthEast.Lng; }
			set { NorthEast.Lng = value; }
		}

		public double WestLng
		{
			get { return SouthWest.Lng; }
			set { SouthWest.Lng = value; }
		}

	    public LatLngBounds()
	    {
	        NorthEast = new LatLng();
            SouthWest = new LatLng();
	    }

	    public void ExtentToContain(LatLng point)
		{
			if (point.Lat > NorthLat)
				NorthLat = point.Lat;

			if (point.Lat < SouthLat)
				SouthLat = point.Lat;

			if (point.Lng > EastLng)
				EastLng = point.Lng;

			if (point.Lng < WestLng)
				WestLng = point.Lng;
		}

		public LatLng GetCenter()
		{
			return new LatLng
			       {
				       Lat = (NorthLat + SouthLat)/2,
				       Lng = (EastLng + WestLng)/2
			       };
		}

		public LatLngBounds GetSmallestContainingRect()
		{
			var center = GetCenter();
			var latSpan = Math.Abs(NorthLat - SouthLat);
			var lngSpan = Math.Abs(EastLng - WestLng);
			var largerSpan = Math.Max(latSpan, lngSpan);

			return new LatLngBounds
			{
				NorthEast = new LatLng { Lat = center.Lat + largerSpan/2, Lng = center.Lng + largerSpan/2 },
				SouthWest = new LatLng { Lat = center.Lat - largerSpan/2, Lng = center.Lng - largerSpan/2 }
			};
		}

		public LatLngBounds GetLargestContainedRect()
		{
			var center = GetCenter();
			var latSpan = Math.Abs(NorthLat - SouthLat);
			var lngSpan = Math.Abs(EastLng - WestLng);
			var smallerSpan = Math.Min(latSpan, lngSpan);

			return new LatLngBounds
			{
				NorthEast = new LatLng { Lat = center.Lat + smallerSpan/2, Lng = center.Lng + smallerSpan/2 },
				SouthWest = new LatLng { Lat = center.Lat - smallerSpan/2, Lng = center.Lng - smallerSpan/2 }
			};
		}

		public string ToGoogleApi()
		{
			return $"new google.maps.LatLngBounds({SouthWest.ToGoogleApi()}, {NorthEast.ToGoogleApi()})";
		}

		public static LatLngBounds BoundsOf(LatLng point)
		{
			return new LatLngBounds {NorthEast = point, SouthWest = point};
		}

		public static LatLngBounds BoundsOf(IEnumerable<LatLng> points)
		{
			LatLngBounds result = null;

			foreach (var point in points)
			{
				if (point == null)
					continue;
				
				if (result == null)
					result = BoundsOf(point);
				else
					result.ExtentToContain(point);
			}

			return result;
		}
	}
}