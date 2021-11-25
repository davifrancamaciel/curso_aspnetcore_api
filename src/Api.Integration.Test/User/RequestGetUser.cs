using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.User
{
    public class RequestGetUser : BaseIntegration
    {
        private string _name { get; set; }

        private string _email { get; set; }

        [Fact]
        public async Task CrudUser()
        {
            await AddToken();
            _name = Faker.Name.First();
            _email = Faker.Internet.Email();

            var userDto = new UserDTO { Name = _name, Email = _email };

            //Post
            var response = await PostJsonAsync(userDto, $"{hostApi}/users", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var userCreateResult = JsonConvert.DeserializeObject<UserCreateResultDTO>(postResult);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(userCreateResult);
            Assert.Equal(userCreateResult.Name, _name);
            Assert.Equal(userCreateResult.Email, _email);
            Assert.True(userCreateResult.Id != Guid.Empty);

            //GetAll
            response = await client.GetAsync($"{hostApi}/users");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var listFromJson = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(jsonResult);
            Assert.NotNull(listFromJson);
            Assert.True(listFromJson.Any());
            Assert.NotNull(listFromJson.FirstOrDefault(u => u.Id.Equals(userCreateResult.Id)));

            //Put
            var updateUserDto = new UserUpdateDTO
            {
                Id = userCreateResult.Id,
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(updateUserDto), Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}/users", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var userUpdateResult = JsonConvert.DeserializeObject<UserUpdateResultDTO>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(userCreateResult.Name, userUpdateResult.Name);
            Assert.NotEqual(userCreateResult.Email, userUpdateResult.Email);
            Assert.Equal(userCreateResult.Id, userUpdateResult.Id);

            //Get by id
            response = await client.GetAsync($"{hostApi}/users/{userUpdateResult.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var objectFromJson = JsonConvert.DeserializeObject<UserDTO>(jsonResult);

            Assert.NotNull(jsonResult);
            Assert.Equal(objectFromJson.Name, userUpdateResult.Name);
            Assert.Equal(objectFromJson.Email, userUpdateResult.Email);
            Assert.Equal(objectFromJson.Id, userUpdateResult.Id);

            //delete
            response = await client.DeleteAsync($"{hostApi}/users/{userUpdateResult.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //Get by id after delete
            response = await client.GetAsync($"{hostApi}/users/{userUpdateResult.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
