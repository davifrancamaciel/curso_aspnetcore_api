using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.DTOs.State;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.State
{
    public class RequestGetState : BaseIntegration
    {
        private string _name { get; set; }

        private string _sigla { get; set; }

        [Fact]
        public async Task CrudState()
        {
            await AddToken();

            //GetAll
            response = await client.GetAsync($"{hostApi}/states");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var listFromJson = JsonConvert.DeserializeObject<IEnumerable<StateDTO>>(jsonResult);
            Assert.NotNull(listFromJson);
            Assert.True(listFromJson.Any());
            Assert.True(listFromJson.Count() == 27);
            Assert.True(listFromJson.Where(x => x.Sigla.Equals("RJ")).Count() == 1);



            //Get by id
            var state = listFromJson.FirstOrDefault(x => x.Sigla.Equals("RJ"));
            response = await client.GetAsync($"{hostApi}/states/{state.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var objectFromJson = JsonConvert.DeserializeObject<StateDTO>(jsonResult);

            Assert.NotNull(jsonResult);
            Assert.Equal(objectFromJson.Name, state.Name);
            Assert.Equal(objectFromJson.Sigla, state.Sigla);
            Assert.Equal(objectFromJson.Id, state.Id);
        }
    }
}
