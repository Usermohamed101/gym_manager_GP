using GymManagmentSystem.DataBase;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem.Repos
{
    public interface ITrainingPlanRepo
    {
        IEnumerable<WorkOutPlan> GetPlansForMember(string memberId);
        WorkOutPlan? GetById(int id);
        bool Add(WorkOutPlan plan);
        bool Update(WorkOutPlan plan,int id);
        bool Delete(int id);
    }




    public class TrainingPlanRepo : ITrainingPlanRepo
    {
        private readonly GymDb contxt;

        public TrainingPlanRepo(GymDb contxt)
        {
            this.contxt = contxt;
        }

        public bool Add(WorkOutPlan plan)
        {
            contxt.TrainingPlans.Add(plan);
            contxt.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var TP = GetById(id);
            contxt.Remove(TP);
            contxt.SaveChanges();
            return true;
        }

        public WorkOutPlan? GetById(int id)
        {
            return contxt.TrainingPlans.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<WorkOutPlan> GetPlansForMember(string memberId)
        {
           return contxt.Members.Include(m=>m.TrainingPlans).FirstOrDefault(m=>m.Id==memberId).TrainingPlans.ToList();
        }

        public bool Update(WorkOutPlan plan,int id)
        {

            return true;
        }
    }
}
