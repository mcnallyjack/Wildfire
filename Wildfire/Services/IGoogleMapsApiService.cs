/* Author:      Jack McNally
 * Page Name:   IGoogleMapsApiService
 * Purpose:     IGoogleMapsApiService for places.
 */
using System.Threading.Tasks;
using Wildfire.Models;

namespace Wildfire.Services
{
    public interface IGoogleMapsApiService
    {
        Task<GooglePlaceAutoCompleteResult> GetPlaces(string text);
        Task<GooglePlace> GetPlaceDetails(string placeId);
    }
}
