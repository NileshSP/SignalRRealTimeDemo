# SignalRRealTimeCollaboration

Project showing [SignalR](http://www.asp.net/signalr) capabilities being leveraged to provide collaboration between connected browser clients

Try http://samplesignalrrealtime.azurewebsites.net in mutiple tabs (in IE Edge as chrome requires https for some features to work)

Features supported for auto synchronization between connected browser clients:

1. adding and removing google map marker pins

2. zooming levels between connected maps

3. Client information window overlay panel(in gradient black) is synchronized between connected clients as well


![alt text](https://github.com/NileshSP/SignalRRealTimeDemo/blob/master/screenshot.gif "Working example..")


For privacy concerns, user details are not stored in any files or database for the website to work as everything is in virtual memory and therefore when the browser is closed respective objects from the memory are also removed/destroyed
