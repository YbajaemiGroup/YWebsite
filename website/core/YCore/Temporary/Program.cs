using YApi;

var client = new YClient("token");

await client.DeleteLinksAsync(5);