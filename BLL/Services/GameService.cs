using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GameService : ServiceBase, IService<Game,GameModel>
    {
        public GameService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Game record)
        {
            if (_db.Games.Any(g => g.Title.ToUpper() == record.Title.ToUpper().Trim() && g.IsMultiplayer == record.IsMultiplayer &&
            g.ReleaseDate == record.ReleaseDate && g.Price == record.Price))
                return Error("Game with the same Title, release date and multiplayer status and price exist.");
            
            record.Title = record.Title?.Trim();
            _db.Games.Add(record);
            _db.SaveChanges();
            return Success("Game added successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Games.SingleOrDefault(g => g.Id == id);
            if (entity is null)
                return Error("Game can't be found.");
            _db.Games.Remove(entity);
            _db.SaveChanges();
            return Success("Game deleted successfully.");
        }

        public IQueryable<GameModel> Query()
        {
           return  _db.Games.Include(g => g.Publisher).OrderBy(g => g.ReleaseDate).ThenByDescending(g => g.Price).
                Select(g => new GameModel() { Record = g });
        }

        public ServiceBase Update(Game record)
        {
            if (_db.Games.Any(g => g.Id != record.Id && g.Title.ToUpper() == record.Title.ToUpper().Trim() &&
                g.IsMultiplayer == record.IsMultiplayer && g.ReleaseDate == record.ReleaseDate && g.Price == record.Price))
                return Error("Game with the same Title, release date and multiplayer status and price exist.");

            var entity = _db.Games.SingleOrDefault(g => g.Id == record.Id);

            if (entity is null)
                return Error("Game is not found!");
            entity.Title = record.Title?.Trim();
            entity.ReleaseDate = record.ReleaseDate;
            entity.Price = record.Price;
            entity.IsMultiplayer = record.IsMultiplayer;
            entity.PublisherId = record.PublisherId;

            _db.Games.Update(entity);
            _db.SaveChanges();
            return Success("Games updated successfully!");
        }
    }
}
