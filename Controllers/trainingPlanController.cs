using GymManagmentSystem.DataBase;
using GymManagmentSystem.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class trainingPlanController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly GymDb _context;

        public trainingPlanController(UserManager<User> userManager, GymDb context)
        {
            _userManager = userManager;
            _context = context;
        }

        // Trainer creates plan
        [HttpPost]
        [Authorize(Roles = "Trainer")]
        public async Task<IActionResult> CreatePlan(CreatePlanDto dto)
        {
            var trainer = await _userManager.GetUserAsync(User);

            var plan = new WorkOutPlan()
            {
              
                TrainerId = trainer.Id,
                MemberId = dto.MemberId,
                Name = dto.Title,
               
            };

            _context.TrainingPlans.Add(plan);
            await _context.SaveChangesAsync();

            return Ok(plan);
        }

        // Trainer adds workout to plan
        [HttpPost("{planId}/workouts")]
        [Authorize(Roles = "Trainer")]
        public async Task<IActionResult> AddWorkout(int planId, AddWorkoutDto dto)
        {
            var workout = new WorkOut
            {
                
                WorkOutPlanId = planId,
                ExerciseName = dto.Name,
                Reps = dto.Reps,
                Sets = dto.Sets,
                
            };

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            return Ok(workout);
        }

        // Member or Trainer views plan
        [HttpGet("member/{memberId}")]
        [Authorize]
        public IActionResult GetMemberPlan(string memberId)
        {
            var plan = _context.TrainingPlans
                .Where(p => p.MemberId == memberId)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    Workouts = p.Workouts.Select(w => new {
                        w.Id,
                        w.ExerciseName,
                        w.Reps,
                        w.Sets,
                       
                    })
                })
                .ToList();

            return Ok(plan);
        }
    }



}
