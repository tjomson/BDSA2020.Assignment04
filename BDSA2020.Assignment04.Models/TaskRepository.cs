using System.Linq;
using BDSA2020.Assignment04.Entities;
using static BDSA2020.Assignment04.Models.Response;
using BDSA2020.Assignment04.Models;
using System.Collections.Generic;


namespace BDSA2020.Assignment04.Models
{
    public class TaskRepository : ITaskRepository
    {

        private readonly KanbanContext _context;
        public TaskRepository(KanbanContext context){
            _context = context;
        }
        public (Response response, int createdId) Create(TaskCreateDTO task)
        {
             IEnumerable<TaskTag> tags = from tasktag in _context.TaskTags
                                        where task.Tags.Contains(tasktag.Tag.Name)
                                        select tasktag;

            Task t = new Task() {
                Title = task.Title,
                AssignedTo = _context.Users.Find(task.AssignedToId),
                Description = task.Description,
                State = State.New,
                Tags = tags.ToList() 
            };
            _context.Add<Task>(t);
            _context.SaveChanges();
            
            return (Created,t.Id);
        }

        public Response Delete(int taskId)
        {
            var Entity = _context.Tasks.Find(taskId);
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
            throw new System.NotImplementedException();
        }

        public TaskDetailsDTO Read(int taskId)
        {
            throw new System.NotImplementedException();
        }

        public Response Update(TaskUpdateDTO task)
        {
            throw new System.NotImplementedException();
        }
    }
}