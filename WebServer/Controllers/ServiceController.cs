using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServer.Models;

namespace WebServer.Controllers {
    [RoutePrefix("services")]
    public class ServiceController : ApiController {
        [Route("getnewid")]
        [HttpGet]
        public int GetNewId() {
            int highestId = 1;

            foreach (Client currClient in GetListOfClients()) {
                if (currClient.Id >= highestId) {
                    highestId = currClient.Id + 1;
                }
            }
            return highestId;
        }

        private List<Client> GetListOfClients() {
            clientdbEntities db = new clientdbEntities();
            return db.Clients.ToList();
        }
    }
}
