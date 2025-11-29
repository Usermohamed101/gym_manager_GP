using GymManagmentSystem.DataBase;

namespace GymManagmentSystem.Repos
{







    public interface IMembershipRepo
    {



            IEnumerable<Membership> GetAll();
            Membership? GetById(int id);
            bool Add(Membership membership);
            bool Update(Membership membership);
            bool Delete(int id);
        





    }
    public class MembershipRepo : IMembershipRepo
    {
        private readonly GymDb contxt;

        public MembershipRepo(GymDb contxt)
        {
            this.contxt = contxt;
        }
        public bool Add(Membership membership)
        {
            contxt.Memberships.Add(membership);
            contxt.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var ms = GetById(id);
            contxt.Remove(ms);
            contxt.SaveChanges();
            return true;
            
        }

        public IEnumerable<Membership> GetAll()
        {
            return contxt.Memberships.ToList();
        }

        public Membership? GetById(int id)
        {
            return contxt.Memberships.FirstOrDefault(m => m.Id == id);
        }

        public bool Update(Membership membership)
        {
            throw new NotImplementedException();
        }
    }




}
