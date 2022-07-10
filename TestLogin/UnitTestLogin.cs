using Challenge.Controllers;
using Challenge.Interfaces;
using Challenge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestLogin
{
    [TestClass]
    public class UnitTestLogin
    {
        User.LoginSet loginSetOK = new User.LoginSet()
        {
            User = "TestingUser",
            Pass = "TestingPass"
        };

        private User.LoginResult loginResultOK => new User.LoginResult()
        {
            IdResponse = StatusCodes.Status200OK,
            Response = "OK"
        };

        User.LoginSet loginSetERROR = new User.LoginSet()
        {
            User = "WrongUser",
            Pass = "WrongPass"
        };
        private User.LoginResult loginBadRequest => new User.LoginResult()
        {
            IdResponse = StatusCodes.Status400BadRequest,
            Response = "Bad Request"
        };


        [TestMethod]
        public void UnitTestLogin_OK()
        {
            var mockUserLogin = new Mock<IUserLogin>();
            var mockLog = new Mock<Ilog4net>();
            
            mockUserLogin.Setup(testOK => testOK.Login(loginSetOK)).Returns(loginResultOK);
            var controlller = new UserController(mockUserLogin.Object, mockLog.Object);
            var resultado = controlller.Login(loginSetOK) as OkObjectResult;

            Assert.IsInstanceOfType(resultado, typeof(OkObjectResult));
            Assert.IsTrue(resultado.StatusCode == 200);

            var resTestOK = resultado.Value as User.LoginResult;

            Assert.IsTrue(resTestOK.IdResponse == StatusCodes.Status200OK);
        }

        [TestMethod]
        public void UnitTestLogin_ERROR()
        {
            var mockUserLogin = new Mock<IUserLogin>();
            var mockLog = new Mock<Ilog4net>();

            mockUserLogin.Setup(testERROR => testERROR.Login(loginSetERROR)).Returns(loginBadRequest);
            var controlller = new UserController(mockUserLogin.Object, mockLog.Object);
            var resultado = controlller.Login(loginSetERROR) as OkObjectResult;

            Assert.IsInstanceOfType(resultado, typeof(OkObjectResult));
            Assert.IsTrue(resultado.StatusCode == 200);

            var resTestError = resultado.Value as User.LoginResult;

            Assert.IsTrue(resTestError.IdResponse == StatusCodes.Status200OK);
        }
    }
}
