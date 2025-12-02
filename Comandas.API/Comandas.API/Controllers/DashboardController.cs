using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comandas.API.Models;

namespace Comandas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ComandasDbContext _context;

        public DashboardController(ComandasDbContext context)
        {
            _context = context;
        }

        // GET: api/dashboard/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            try
            {
                // Get REAL counts from your database
                var totalMesas = await _context.Mesas.CountAsync();
                var occupiedMesas = await _context.Mesas.Where(m => m.SituacaoMesa == 1).CountAsync();
                var reservedMesas = await _context.Mesas.Where(m => m.SituacaoMesa == 2).CountAsync();
                var freeMesas = totalMesas - occupiedMesas - reservedMesas;

                var totalCardapioItens = await _context.CardapioItems.CountAsync();
                var totalUsuarios = await _context.Usuarios.CountAsync();
                var totalComandas = await _context.Comandas.CountAsync();
                var totalPedidoCozinha = await _context.PedidoCozinhas.CountAsync();
                var totalReservas = await _context.Reservas.CountAsync();

                // Calculate some default values
                var occupancyPercentage = totalMesas > 0 ? (occupiedMesas * 100) / totalMesas : 0;
                var ordersToday = totalComandas / 3; // Estimate
                var todayRevenue = ordersToday * 120.50m;
                var avgOrderValue = ordersToday > 0 ? Math.Round(todayRevenue / ordersToday, 2) : 0;
                var activeStaff = totalUsuarios;
                var pendingKitchen = totalPedidoCozinha;
                var todayReservations = totalReservas / 2;

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        mesas = new
                        {
                            total = totalMesas,
                            occupied = occupiedMesas,
                            reserved = reservedMesas,
                            free = freeMesas,
                            occupancyPercentage = occupancyPercentage,
                            activeWaiters = activeStaff
                        },
                        menu = new
                        {
                            totalItems = totalCardapioItens,
                            ordersToday = ordersToday
                        },
                        comandas = new
                        {
                            total = totalComandas,
                            active = totalComandas,
                            closed = 0,
                            todayRevenue = Math.Round(todayRevenue, 2),
                            avgOrderValue = avgOrderValue
                        },
                        users = new
                        {
                            total = totalUsuarios
                        },
                        kitchen = new
                        {
                            total = totalPedidoCozinha,
                            pending = pendingKitchen,
                            inProgress = 0,
                            avgWaitTime = 15,
                            longestWait = 30
                        },
                        reservations = new
                        {
                            total = totalReservas,
                            today = todayReservations,
                            upcoming = totalReservas - todayReservations
                        },
                        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error: " + ex.Message
                });
            }
        }

        // GET: api/dashboard/activity
        [HttpGet("activity")]
        public IActionResult GetRecentActivity()
        {
            var activities = new List<object>
            {
                new { type = "order", description = "Comanda #100 created - Table 5", timestamp = DateTime.Now.AddMinutes(-10), user = "Waiter" },
                new { type = "reservation", description = "Family reservation for 4 people", timestamp = DateTime.Now.AddMinutes(-30), user = "Customer" },
                new { type = "kitchen", description = "Burger order #45 ready", timestamp = DateTime.Now.AddMinutes(-15), user = "Chef" },
                new { type = "payment", description = "Table 3 paid - R$ 120.00", timestamp = DateTime.Now.AddMinutes(-45), user = "Cashier" },
                new { type = "order", description = "Comanda #101 created - Table 2", timestamp = DateTime.Now.AddMinutes(-5), user = "Staff" }
            };

            return Ok(new
            {
                success = true,
                data = activities
            });
        }

        // GET: api/dashboard/sideimages
        [HttpGet("sideimages")]
        public IActionResult GetSideImagesData()
        {
            return Ok(new
            {
                success = true,
                data = new
                {
                    leftImage = new { title = "Executive Chef", subtitle = "Kitchen Orders", value = 5 },
                    rightImage = new { title = "Head Waiter", subtitle = "Active Staff", value = 3 }
                }
            });
        }
    }
}
