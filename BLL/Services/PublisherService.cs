using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Services.Bases;
using BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IPublisherService
    {
        public IQueryable<PublisherModel> Query();

        public ServiceBase Create(Publisher record);
        public ServiceBase Update(Publisher record);
        public ServiceBase Delete(int id);
    }
    public class PublisherService : ServiceBase, IPublisherService
    {
        public PublisherService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Publisher record)
        {
            if (_db.Publishers.Any(p => p.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("This publisher already exists!");

            record.Name = record.Name?.Trim();
            _db.Publishers.Add(record);
            _db.SaveChanges();
            return Success("Publisher is added!");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Publishers.Include(p => p.Games).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return Error("Publisher is not found!");
            if(entity.Games.Count > 0)
                return Error("This publisher has games. You can not delete it!");
            _db.Publishers.Remove(entity);
            _db.SaveChanges();
            return Success("Publisher deleted successfully!");
        }

        public IQueryable<PublisherModel> Query()
        {
            return _db.Publishers.OrderBy(p => p.Name).Select(p => new PublisherModel() { Record = p });
        }

        public ServiceBase Update(Publisher record)
        {
            if(_db.Publishers.Any(p => p.Id != record.Id && p.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("This publisher already exists!");
            
            var entity = _db.Publishers.SingleOrDefault(p => p.Id == record.Id);

            if (entity is null)
                return Error("Publisher is not found!");
            entity.Name = record.Name?.Trim();
            _db.Publishers.Update(entity);
            _db.SaveChanges();
            return Success("Publishers updated successfully!");
        }
    }
}
