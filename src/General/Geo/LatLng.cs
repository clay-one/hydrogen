
namespace Hydrogen.General.Geo
{
    public class LatLng
	{
        public static readonly LatLng Zero = new LatLng {Lat = 0, Lng = 0};

		public double Lat { get; set; }
		public double Lng { get; set; }

		public string ToGoogleApi()
		{
			return $"new google.maps.LatLng({Lat}, {Lng})";
		}

		public string ToWkt()
		{
			return $"POINT({Lng} {Lat})";
		}
	}
}