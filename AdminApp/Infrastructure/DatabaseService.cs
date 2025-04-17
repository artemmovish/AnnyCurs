using AdminApp.Core.Models;
using AdminApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore; 

namespace AdminApp.Infrastructure
{
    public static class DatabaseService
    {
        private static readonly AppDbContext _db = new();

        // Инициализация БД (вызывать при старте приложения)
        public static void Initialize()
        {
            _db.Database.EnsureCreated();

            // Опционально: добавление тестовых данных, если БД пустая
            if (!_db.Cities.Any())
            {
                _db.Cities.Add(new City { Name = "Москва" });
                _db.Cities.Add(new City { Name = "Санкт-Петербург" });
                _db.SaveChanges();
            }
        }

        #region Attractions Methods
        public static List<Attraction> GetAllAttractions()
            => _db.Attractions
                .Include(a => a.Images)
                .Include(a => a.City)
                .Include(a => a.Reviews)
                .ThenInclude(r => r.Author)
                .ToList();

        public static Attraction GetAttractionById(int id)
            => _db.Attractions
                .Include(a => a.City)
                .Include(a => a.Reviews)
                .FirstOrDefault(a => a.Id == id);

        public static void AddAttraction(Attraction attraction)
        {
            _db.Attractions.Add(attraction);
            _db.SaveChanges();
        }

        public static void UpdateAttraction(Attraction attraction)
        {
            _db.Attractions.Update(attraction);
            _db.SaveChanges();
        }

        public static void DeleteAttraction(int id)
        {
            var attraction = _db.Attractions.FirstOrDefault(a => a.Id == id);
            if (attraction != null)
            {
                _db.Attractions.Remove(attraction);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Attraction Images Methods

        // Получение всех изображений для достопримечательности
        public static List<AttractionImage> GetImagesForAttraction(int attractionId)
            => _db.AttractionImages
                .Where(img => img.Attraction.Id == attractionId)
                .ToList();

        // Добавление изображения
        public static void AddImageToAttraction(int attractionId, string imagePath)
        {
            var attraction = _db.Attractions.Find(attractionId);
            if (attraction != null)
            {
                var image = new AttractionImage
                {
                    ImagePath = imagePath,
                    Attraction = attraction
                };
                _db.AttractionImages.Add(image);
                _db.SaveChanges();
            }
        }

        // Удаление изображения по ID
        public static void DeleteImage(int imageId)
        {
            var image = _db.AttractionImages.Find(imageId);
            if (image != null)
            {
                _db.AttractionImages.Remove(image);
                _db.SaveChanges();
            }
        }

        // Удаление всех изображений для достопримечательности
        public static void DeleteAllImagesForAttraction(int attractionId)
        {
            var images = _db.AttractionImages
                .Where(img => img.Attraction.Id == attractionId)
                .ToList();

            if (images.Any())
            {
                _db.AttractionImages.RemoveRange(images);
                _db.SaveChanges();
            }
        }

        #endregion

        #region User Methods
        public static List<User> GetAllUsers()
            => _db.Users
                .Include(u => u.FavoriteAttractions)
                .ToList();

        public static User GetUserById(int id)
            => _db.Users
                .Include(u => u.FavoriteAttractions)
                .FirstOrDefault(u => u.Id == id);

        public static User GetUserByLogin(string username)
            => _db.Users.FirstOrDefault(u => u.Username == username);

        public static void AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public static void UpdateUser(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        public static void DeleteUser(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
        }

        public static void AddToFavorites(int userId, int attractionId)
        {
            var user = _db.Users.Include(u => u.FavoriteAttractions).FirstOrDefault(u => u.Id == userId);
            var attraction = _db.Attractions.Find(attractionId);

            if (user != null && attraction != null)
            {
                user.FavoriteAttractions.Add(attraction);
                _db.SaveChanges();
            }
        }

        public static void RemoveFromFavorites(int userId, int attractionId)
        {
            var user = _db.Users.Include(u => u.FavoriteAttractions).FirstOrDefault(u => u.Id == userId);
            var attraction = user?.FavoriteAttractions.FirstOrDefault(a => a.Id == attractionId);

            if (attraction != null)
            {
                user.FavoriteAttractions.Remove(attraction);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Review Methods
        public static List<Review> GetReviewsForAttraction(int attractionId)
            => _db.Reviews
                .Where(r => r.Attraction.Id == attractionId)
                .Include(r => r.Author)
                .ToList();

        public static void AddReview(Review review)
        {
            _db.Reviews.Add(review);
            _db.SaveChanges();
        }

        public static void DeleteReview(int id)
        {
            var review = _db.Reviews.FirstOrDefault(r => r.Id == id);
            if (review != null)
            {
                _db.Reviews.Remove(review);
                _db.SaveChanges();
            }
        }
        #endregion

        #region City Methods
        public static List<City> GetAllCities()
            => _db.Cities.ToList();

        public static City GetCityById(int id)
            => _db.Cities.FirstOrDefault(c => c.Id == id);

        public static void AddCity(City city)
        {
            _db.Cities.Add(city);
            _db.SaveChanges();
        }
        public static void UpdateCity(City city)
        {
            _db.Cities.Update(city);
            _db.SaveChanges();
        }

        public static void DeleteCity(int id)
        {
            var city = _db.Cities.FirstOrDefault(c => c.Id == id);
            if (city != null)
            {
                _db.Cities.Remove(city);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}
