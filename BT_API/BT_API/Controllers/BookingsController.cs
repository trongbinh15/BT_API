using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Domain;
using Repository;

namespace BT_API.Controllers
{
    public class BookingsController : ApiController
    {
        private HotelDBContext db = new HotelDBContext();

        RoomsController roomCtrl;
        BookingsController bookingCtrl;

        // GET: api/Bookings
        public IQueryable<Booking> GetBookings()
        {
            return db.Bookings;
        }

        // GET: api/Bookings/5
        [ResponseType(typeof(Booking))]
        public IHttpActionResult GetBooking(string id)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // PUT: api/Bookings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBooking(string id, Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != booking.BookingID)
            {
                return BadRequest();
            }

            db.Entry(booking).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Bookings
        [ResponseType(typeof(Booking))]
        public IHttpActionResult PostBooking(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bookings.Add(booking);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BookingExists(booking.BookingID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = booking.BookingID }, booking);
        }

        // DELETE: api/Bookings/5
        [ResponseType(typeof(Booking))]
        public IHttpActionResult DeleteBooking(string id)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            db.Bookings.Remove(booking);
            db.SaveChanges();

            return Ok(booking);
        }


        public bool Reservation(DateTime ngaydat, DateTime ngaytra, string hoten, string cmnd, int slPhong, int slNguoi)
        {
            var listRoom = db.Rooms.Where(s => s.isEnable == true && s.capacity > slNguoi).ToList();
            if (listRoom.Count() < slPhong)
            {
                return false;
            }

            var listBooking = bookingCtrl.GetBookings().ToList();
            var lastBooking = listBooking.Last();
            Booking newBooking = new Booking();
            newBooking.CheckIn = ngaydat;
            newBooking.CheckOut = ngaytra;
            newBooking.CMND = cmnd;
            newBooking.CustomerName = hoten;

            if (listBooking != null)
            {
                newBooking.BookingID = (int.Parse(lastBooking.BookingID) + 1).ToString();
            }else
            {
                newBooking.BookingID = "0001";
            }

            try
            {
                db.Bookings.Add(newBooking);                

                for (int i = 0; i < slPhong; i++)
                {
                    var room = listRoom[i];
                    room.isEnable = false;
                }

                
                db.SaveChanges();
            }
            catch( Exception ex)
            {
                //Xử lý lỗi
            }
            

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookingExists(string id)
        {
            return db.Bookings.Count(e => e.BookingID == id) > 0;
        }
    }
}