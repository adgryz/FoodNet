using FoodNet.ModelCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNET.WebAPI.Data.Interfaces
{
    public interface ITagsRepository
    {
        IEnumerable<Tag> GetAllTags();
        Tag GetTagById(Guid tagId);
    }
}
