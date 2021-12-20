using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.DTOs.City;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.City
{
    public class RequestGetCity : BaseIntegration
    {


        [Fact]
        public async Task CrudCity()
        {
            await AddToken();

            var cityCreateDto = new CityCreateDTO
            {
                Name = Faker.Address.City(),
                IbgeId = 121212,
                StateId = Guid.Parse("43a0f783-a042-4c46-8688-5dd4489d2ec7")
            };


            //Post
            var response = await PostJsonAsync(cityCreateDto, $"{hostApi}/cities", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var cityCreateResult = JsonConvert.DeserializeObject<CityCreateResultDTO>(postResult);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(cityCreateResult);
            Assert.Equal(cityCreateResult.Name, cityCreateDto.Name);
            Assert.Equal(cityCreateResult.IbgeId, cityCreateDto.IbgeId);
            Assert.Equal(cityCreateResult.StateId, cityCreateDto.StateId);
            Assert.True(cityCreateResult.Id != Guid.Empty);
            Assert.True(cityCreateResult.CreateAt != null);
            

            //Put
            var cityUpdateDto = new CityUpdateDTO
            {
                Id = cityCreateResult.Id,
                Name = Faker.Address.City(),
                IbgeId = 1233333,
                StateId = Guid.Parse("542668d1-50ba-4fca-bbc3-4b27af108ea3")
            };
            response = await PutJsonAsync(cityUpdateDto, $"{hostApi}/cities", client);
            var putResult = await response.Content.ReadAsStringAsync();
            var cityUpdateResult = JsonConvert.DeserializeObject<CityUpdateResultDTO>(putResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(cityUpdateResult);
            Assert.Equal(cityUpdateResult.Name, cityUpdateDto.Name);
            Assert.Equal(cityUpdateResult.IbgeId, cityUpdateDto.IbgeId);
            Assert.Equal(cityUpdateResult.StateId, cityUpdateDto.StateId);
            Assert.True(cityUpdateResult.Id != Guid.Empty);
            Assert.True(cityUpdateResult.UpdateAt != null);


            //Get by id            
            response = await client.GetAsync($"{hostApi}/cities/{cityCreateResult.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var objectFromJson = JsonConvert.DeserializeObject<CityDTO>(jsonResult);

            Assert.NotNull(jsonResult);
            Assert.Equal(objectFromJson.Name, cityUpdateResult.Name);
            Assert.Equal(objectFromJson.IbgeId, cityUpdateResult.IbgeId);
            Assert.Equal(objectFromJson.Id, cityUpdateResult.Id);

            //Get by id  complete
            response = await client.GetAsync($"{hostApi}/cities/complete/{cityCreateResult.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var cityCompleteById = JsonConvert.DeserializeObject<CityCompleteDTO>(jsonResult);

            Assert.NotNull(jsonResult);
            Assert.Equal(cityCompleteById.Name, cityUpdateResult.Name);
            Assert.Equal(cityCompleteById.IbgeId, cityUpdateResult.IbgeId);
            Assert.Equal(cityCompleteById.Id, cityUpdateResult.Id);
            Assert.NotNull(cityCompleteById.State);
            Assert.Equal(cityCompleteById.State.Sigla, "RN");
            Assert.Equal(cityCompleteById.State.Name, "Rio Grande do Norte");

            //Get by by-ibge  complete
            response = await client.GetAsync($"{hostApi}/cities/by-ibge/{cityUpdateResult.IbgeId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var cityCompleteByIbge = JsonConvert.DeserializeObject<CityCompleteDTO>(jsonResult);

            Assert.NotNull(jsonResult);
            Assert.Equal(cityCompleteByIbge.Name, cityUpdateResult.Name);
            Assert.Equal(cityCompleteByIbge.IbgeId, cityUpdateResult.IbgeId);
            Assert.Equal(cityCompleteByIbge.Id, cityUpdateResult.Id);
            Assert.NotNull(cityCompleteByIbge.State);
            Assert.Equal(cityCompleteByIbge.State.Sigla, "RN");
            Assert.Equal(cityCompleteByIbge.State.Name, "Rio Grande do Norte");

            //GetAll
            response = await client.GetAsync($"{hostApi}/cities");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var listFromJson = JsonConvert.DeserializeObject<IEnumerable<CityDTO>>(jsonResult);
            Assert.NotNull(listFromJson);
            Assert.True(listFromJson.Any());
            Assert.True(listFromJson.Where(x => x.Id.Equals(cityCreateResult.Id)).Count() == 1);

            //delete
            response = await client.DeleteAsync($"{hostApi}/cities/{cityUpdateResult.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //Get by id after delete
            response = await client.GetAsync($"{hostApi}/cities/{cityUpdateResult.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
