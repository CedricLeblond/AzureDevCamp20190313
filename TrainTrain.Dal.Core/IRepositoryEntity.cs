using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TrainTrain.Dal.Entities;

namespace TrainTrain.Dal
{
    public interface IRepositoryEntity<T>
    {
        TrainEntity Get(string id, TrainTrainContext context);
        List<TrainEntity> GetAll(TrainTrainContext options);
        void Save(TrainEntity entity, TrainTrainContext options);
        void RemoveAll(TrainTrainContext context);
        void Remove(string trainId, TrainTrainContext context);
        void SaveAll(TrainEntity[] entities, TrainTrainContext context);
    }
}