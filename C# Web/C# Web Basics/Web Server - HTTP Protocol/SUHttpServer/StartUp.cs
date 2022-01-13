using SUHttpServer;
using System.Net;
using System.Net.Sockets;
using System.Text;

var ipAddress = "127.0.0.1";

var port = 8080;

HttpServer httpServer = new HttpServer(ipAddress, port);

httpServer.Start();