// See https://aka.ms/new-console-template for more information
using YCore.Data;
using YCore.Data.OS;

Console.WriteLine("Hello, World!");

HttpClient client = new HttpClient();
var resp = client.Send(new HttpRequestMessage(HttpMethod.Get, "https://sun9-44.userapi.com/impf/pb6oT9u_cmpXSBIiWkvdzIJYtMS_mMEtlXyNEQ/3T_JPo7ocqI.jpg?size=160x160&quality=96&sign=b4aab2de20de098ea3cacc64c5472b99&type=audio"));

var imagesOperator = new ImagesOperator();

bool res = imagesOperator.SaveImage("3T_JPo7ocqI.jpg", resp.Content.ReadAsByteArrayAsync().Result);

Console.WriteLine(res);