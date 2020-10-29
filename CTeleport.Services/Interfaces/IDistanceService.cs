namespace CTeleport.Services.Interfaces
{
    public interface IDistanceService
    {
        double GetDistance(LatLon pos1, LatLon pos2);
    }
}
