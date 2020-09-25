using System.Linq;
using BDSA2020.Assignment04.Entities;
using static BDSA2020.Assignment04.Models.Response;
using BDSA2020.Assignment04.Models;
using System.Collections.Generic;
using System;


namespace BDSA2020.Assignment04.Models
{
    public class TaskRepository : ITaskRepository
    {

        private readonly KanbanContext _context;
        public TaskRepository(KanbanContext context)
        {
            _context = context;
        }
        public (Response response, int createdId) Create(TaskCreateDTO task)
        {
            IEnumerable<TaskTag> tags = from tasktag in _context.TaskTags
                                        where task.Tags.Contains(tasktag.Tag.Name)
                                        select tasktag;

            Task t = new Task()
            {
                Title = task.Title,
                AssignedTo = _context.Users.Find(task.AssignedToId),
                Description = task.Description,
                State = State.New,
                Tags = tags.ToList()
            };

            if(t.AssignedTo == null){
                return (Response.BadRequest,-1);
            }

            _context.Add<Task>(t);
            _context.SaveChanges();

            return (Created, t.Id);
        }

        public Response Delete(int taskId)
        {
            var Entity = _context.Tasks.Find(taskId);
            if (Entity == null)
            {
                return Response.BadRequest;
            }
            switch (Entity.State)
            {
                case State.New:
                    _context.Tasks.Remove(Entity);
                    _context.SaveChanges();
                    return Response.Deleted;
                case State.Active:
                    Entity.State = State.Removed;
                    _context.SaveChanges();
                    return Response.Deleted;
                case State.Resolved:
                case State.Closed:
                case State.Removed:
                    return Response.Conflict;



            }

            return Response.BadRequest;
        }

        public IQueryable<TaskListDTO> Read(bool includeRemoved = false)
        {
            var returnlist = new List<TaskListDTO>(){};
            foreach (var item in _context.Tasks)
            {
                returnlist.Add(Read(item.Id));
                
            }
            return returnlist.AsQueryable();
        }



        public TaskDetailsDTO Read(int taskId)
        {
            var tasks =
                    from task in _context.Tasks
                    where task.Id == taskId
                    select task;

            var tagIds =
                    from tasktag in _context.TaskTags
                    where tasktag.TaskId == taskId
                    select tasktag.TagId;

            var tags =
                    from tag in _context.Tags
                    where tagIds.Contains(tag.Id)
                    select tag;

            var tagsAsPairs = new List<KeyValuePair<int, string>>();

            foreach (var tag in tags)
            {
                tagsAsPairs.Add(new KeyValuePair<int, string>(tag.Id, tag.Name));
            }

            var thisTask = tasks.FirstOrDefault();
            return new TaskDetailsDTO
            {
                Id = thisTask.Id,
                Title = thisTask.Title,
                AssignedToId = thisTask.AssignedToId,
                AssignedToName = thisTask.AssignedTo.Name,
                State = thisTask.State,
                Description = thisTask.Description,
                Tags = tagsAsPairs
            };

        }

        public Response Update(TaskUpdateDTO task)
        {
            var entity = _context.Tasks.Find(task.Id);
            if(entity == null){
                return Response.BadRequest;
            }
            entity.State = task.State;
            _context.SaveChanges();
            return Response.Updated;
        }

    }
}