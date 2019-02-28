using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrainTrain.Dal.Entities;

namespace TrainTrain.Dal
{
    public class RepositoryEntity : IRepositoryEntity<TrainEntity>
    {
        public TrainEntity Get(string id, TrainTrainContext context)
        {
            using (var db = context)
            {
                return db.Trains.Include(train => train.Seats).SingleOrDefault(t => t.TrainId == id);
            }
        }

        public List<TrainEntity> GetAll(TrainTrainContext options)
        {
            using (var db = options)
            {
                return db.Trains.Include(train => train.Seats).ToList();
            }
        }

        public void Save(TrainEntity entity, TrainTrainContext context)
        {
            if (entity == null) return;

            using (var db = context)
            {
                db.Trains.Add(entity);
                db.SaveChanges();
            }
        }

        public void SaveAll(TrainEntity[] entities, TrainTrainContext context)
        {
            if (entities == null) return;

            using (var db = context)
            {
                db.Trains.AddRange(entities);
                db.SaveChanges();
            }
        }

        public void Remove(string trainId, TrainTrainContext context)
        {
            using (var db = context)
            {
                var train = db.Trains.Include(t => t.Seats).SingleOrDefault(t => t.TrainId == trainId);
                if (train != null)
                {
                    train.Seats.RemoveAll(t => true);
                    RemoveSeats(db);
                    db.Trains.Remove(train);
                }
                db.SaveChanges();
            }
        }

        private static void RemoveSeats(TrainTrainContext db)
        {
            var seats = from s in db.Seats
                select s;
            foreach (var s in seats)
            {
                db.Seats.Remove(s);
            }
        }

        public void RemoveAll(TrainTrainContext context)
        {
            using (var db = context)
            {
               
                var trains = db.Trains.Include(t => t.Seats).ToList();
                if (trains.Any())
                {
                    foreach (var trainEntity in trains)
                    {
                        trainEntity.Seats.RemoveAll(t => true);
                        RemoveSeats(db);
                        db.Trains.Remove(trainEntity);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}