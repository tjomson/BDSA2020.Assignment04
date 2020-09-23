

using System.Linq;

namespace BDSA2020.Assignment04.Models{



    class TagRepository : ITagRepository
    {
        public (Response response, int taskId) Create(TagCreateDTO tag)
        {
            throw new System.NotImplementedException();
        }

        public Response Delete(int tagId, bool force = false)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<TagDTO> Read()
        {
            throw new System.NotImplementedException();
        }

        public TagDTO Read(int tagId)
        {
            throw new System.NotImplementedException();
        }

        public Response Update(TagUpdateDTO tag)
        {
            throw new System.NotImplementedException();
        }
    }
}