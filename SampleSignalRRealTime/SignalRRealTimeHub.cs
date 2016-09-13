using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SampleSignalRRealTime.Models;

namespace SampleSignalRRealTime
{
    [HubName("SignalRRealTimeHub")]
    public class SignalRRealTimeHub : Hub
    {
        static int _hitCounter = 0;
        static List<LocationDataModels> _clients = new List<LocationDataModels>();
        static LocationPosition _position = new LocationPosition() { Left = 5, Top = 5 };
        static ZoomLevel _zoomValue = new ZoomLevel() { ZoomValue = 6 };

        public override Task OnConnected()
        {
            _hitCounter += 1;
            base.Clients.All.hitReceived(_hitCounter);//function referenced on client i.e javascript
            base.Clients.Client(Context.ConnectionId).pinMoved(_position.Left, _position.Top);
            base.Clients.Client(Context.ConnectionId).ZoomReceived(_zoomValue.ZoomValue);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _hitCounter -= 1;
            base.Clients.All.hitReceived(_hitCounter);
            _clients.RemoveAll(t => t.BrowserClientId == Context.ConnectionId);
            base.Clients.All.MarkerReceived(_clients);
            return base.OnDisconnected(stopCalled);
        }

        public static List<LocationDataModels> AddClient(LocationDataModels Obj)
        {
            var ObjNew = new LocationDataModels();
            ObjNew.BrowserClientId = Obj.BrowserClientId;
            ObjNew.MarkerId = (_clients.Count == 0 ? 0 : _clients.Max(t => t.MarkerId)) + 1;
            ObjNew.Latitude = Obj.Latitude;
            ObjNew.Longitude = Obj.Longitude;
            ObjNew.Username = Obj.Username; 
            _clients.Add(ObjNew);
            return _clients;
        }

        public static List<LocationDataModels> RemoveClient(LocationDataModels Obj)
        {
            var ObjTemp = _clients.Where(x => x.BrowserClientId == Obj.BrowserClientId && x.MarkerId == Obj.MarkerId).Single();
            if(ObjTemp != null)
                _clients.Remove(ObjTemp);
            return _clients;
        }

        public void UpdateOtherClientsZoom(ZoomLevel ObjZoom)
        {
            _zoomValue.ZoomValue = ObjZoom.ZoomValue;
            Clients.Others.ZoomReceived(_zoomValue);
        }
        public static bool CheckUser(string User)
        {
            return _clients.Exists(x => x.Username.ToLower().Trim() == User.ToLower().Trim());
        }
        public void MovePin(double x, double y)
        {
            _position.Left = x;
            _position.Top = y; 
            Clients.Others.pinMoved(_position.Left, _position.Top);//function referenced on client i.e javascript
        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}