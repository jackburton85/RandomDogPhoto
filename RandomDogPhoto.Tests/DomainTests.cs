// FILEPATH: /c:/PlayGround/SPPTest.Domain.Tests/Models/DogPhotoTests.cs
using Xunit;
using SPPTest.Domain.Models;

namespace SPPTest.Domain.Tests.Models
{
    public class DogPhotoTests
    {
        [Fact]
        public void CanSetAndGetId()
        {
            var dogPhoto = new DogPhoto();
            dogPhoto.Id = 1;
            Assert.Equal(1, dogPhoto.Id);
        }

        [Fact]
        public void CanSetAndGetBreed()
        {
            var dogPhoto = new DogPhoto();
            dogPhoto.Breed = "Labrador";
            Assert.Equal("Labrador", dogPhoto.Breed);
        }

        [Fact]
        public void CanSetAndGetPhotoUrl()
        {
            var dogPhoto = new DogPhoto();
            dogPhoto.PhotoUrl = "http://example.com/dog.jpg";
            Assert.Equal("http://example.com/dog.jpg", dogPhoto.PhotoUrl);
        }
    }
}