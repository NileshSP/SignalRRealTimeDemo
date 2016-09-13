using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using SampleSignalRRealTime.Models;
using System.Web.Http;
using System;

namespace SampleSignalRRealTime.Controllers
{
    [RoutePrefix("api/LocationData")]
    public class LocationDataController : ApiController
    {
        // POST: api/LocationData
        [HttpPost]
        public void Post([FromBody]LocationDataModels ObjLocData)
        {
            var mappinghub = GlobalHost.ConnectionManager.GetHubContext<SignalRRealTimeHub>();

            List<LocationDataModels> ObjMarkers = SignalRRealTimeHub.AddClient(ObjLocData);

            mappinghub.Clients.All.MarkerReceived(ObjMarkers);
        }

        //// POST: api/LocationData/Zoom
        //[Route("{ZoomLevel}")]
        //[HttpPost]
        //public void Post([FromBody]ZoomLevel ObjZoomData)
        //{
        //    var mappinghub = GlobalHost.ConnectionManager.GetHubContext<SignalRRealTimeHub>();

        //    SignalRRealTimeHub.UpdateZoom(ObjZoomData);

            
        //}

        // GET: api/LocationData/name
        [Route("{User}")]
        [HttpGet]
        public string Get(string User)
        {
            return SignalRRealTimeHub.CheckUser(User).ToString();
        }

        // DELETE: api/LocationData
        [HttpDelete]
        public void Delete([FromBody]LocationDataModels ObjLocData)
        {
            var mappinghub = GlobalHost.ConnectionManager.GetHubContext<SignalRRealTimeHub>();

            List<LocationDataModels> ObjMarkers = SignalRRealTimeHub.RemoveClient(ObjLocData);

            mappinghub.Clients.All.MarkerReceived(ObjMarkers);
        }
    }
}