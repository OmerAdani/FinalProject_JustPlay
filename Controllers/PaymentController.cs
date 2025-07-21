using Final_project1.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_project1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        /// Method to create new payment
        [HttpPost("postCreatPayment")]
        public ActionResult insertPaymentcon([FromBody] Payment newPayment)
        {
            int temp = newPayment.addPayment(newPayment);
            if (temp == 1)
            {
                return Ok("Payment added successfully");
            }
            return BadRequest("Error");
        }

        [HttpGet("GetPaymentList")]
        public List<Payment> getPaymentListCon()
        {
            return Payment.ReadPayments();
        }

        [HttpPut("updatepayment")]
        public IActionResult UpdatePayment([FromBody] Payment payment)
        {
            Payment updatedPayment = payment.UpdatePayment(payment);

            if (updatedPayment == null)
            {
                return Conflict(new { message = "Payment not found or update failed." });
            }

            return Ok(updatedPayment);
        }

        [HttpDelete("DeletePayment")]
        public IActionResult DeletePayment(int PaymentId)
        {
            Payment payment = new Payment();
            bool result = payment.DeletePayment(PaymentId);

            if (result)
                return Ok(new { message = "Payment deleted successfully." });
            else
                return StatusCode(500, new { message = "Failed to delete payment." });
        }
    }
}
