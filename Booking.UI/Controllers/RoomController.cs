using Booking.Core.DTO;
using Booking.Core.Services;
using Booking.Core.ServicesContract;
using Booking.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Booking.UI.Controllers
{
    public class RoomController : Controller
    {
        public List<Guid> SelectedRoomIds { get; set; }

        public IRoomService RoomService { get; set; }
        public RoomController(IRoomService _roomService)
        { 
            RoomService = _roomService;
        }
        // GET: RoomController
        public async Task<IActionResult> Index()
        {
            var rooms = await RoomService.GetAllRooms();
            return View(rooms);
        }
        //public IActionResult AddRoomToList(string id)
        //{
        //    // Retrieving existing or initializing a new list from session
        //    List<Guid> roomList = HttpContext.Session.Get<List<Guid>>("RoomOrderList") ?? new List<Guid>();

        //    // Adding the new Guid to the list
        //    roomList.Add(Guid.Parse(id));

        //    // Saving the updated list back to session
        //    HttpContext.Session.Set("RoomOrderList", roomList);

        //    // Optionally, you can redirect to another action or just return a view.
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public IActionResult SubmitRoomOrderList()
        {
            // Process the roomOrderList (e.g., save to database or perform other operations)
            // ...

            // Get the list from session
            List<Guid> orderedRoomIds = HttpContext.Session.Get<List<Guid>>("RoomList") ?? new List<Guid>();

            // Clear the session variable after processing the list
            HttpContext.Session.Remove("RoomList");

            // Return the list values as JSON
            return Json(new { OrderedRoomIds = orderedRoomIds });
        }
        [HttpPost]
        public IActionResult AddRoomToSession(string roomId)
        {
            // Add the room ID to the session
            var roomList2 = HttpContext.Session.Get<List<Guid>>("RoomList") ?? new List<Guid>();
            roomList2.Add(Guid.Parse(roomId));
            HttpContext.Session.Set("RoomList", roomList2);

            return Json(new { success = true });
        }
        // Extension methods for session operations

        [HttpPost]
   
        // GET: RoomController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            return View(await RoomService.GetRoomById(Guid.Parse(id)));
        }

        // GET: RoomController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomDTO roomDTO)
        {
            ServiceResult result = await RoomService.AddRoom(roomDTO);

            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("ImageFiles", result.ErrorMessage);
                return View(roomDTO);
            }
        }


        // GET: RoomController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            return View(await RoomService.GetRoomById(Guid.Parse(id)));
        }

        // POST: RoomController/Edit/5
     
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(string id,RoomDTO editedRoom)
            {
                if (ModelState.IsValid)
                {
                    // Handle logic to update room in the service or repository
                    var result = await RoomService.UpdateRoom(Guid.Parse(id), editedRoom);
                    if (result.Success)
                    {
                        return RedirectToAction(nameof(Index)); // Redirect to the list after successful update
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    }
                }

                // If the model state is not valid, return to the edit view with validation errors
                return View("Edit", editedRoom);
            }

        

        // GET: RoomController/Delete/5
        public async Task<IActionResult> Delete(string  id)
        {
                return View(await RoomService.GetRoomById(Guid.Parse(id)));
        }

        // POST: RoomController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, RoomDTO room)
        {
            ServiceResult result = await RoomService.DeleteRoom(Guid.Parse(id));

            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("RoomNum", result.ErrorMessage);
                return View(room);
            }
        }
    }
    public static class SessionExtensions
    {
        public static T Get<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            return data == null ? default : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
        }

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
    }
}
